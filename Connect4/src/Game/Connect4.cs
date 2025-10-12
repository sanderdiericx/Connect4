using System.Numerics;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Connect4.src.Game
{
    internal static class Connect4
    {
        // Scans the last move for 4 markers of the same type in a chain, if 4 are found it returns a new gamestate and the winner
        internal static GameCheckResult CheckWinCondition(Grid grid)
        {
            GameState gameState = GameState.Playing;
            Winner winner = Winner.None;
            List<Vector2> winningMarkers = new List<Vector2>();

            Vector2 lastMove = grid._lastMove;
            CellType lastMoveCellType = grid._gridCells[(int) lastMove.X, (int) lastMove.Y]._cellType;

            var LongestChainResult = GetLongestChainAtPosition(grid, lastMoveCellType, (int)lastMove.X, (int)lastMove.Y);
            winningMarkers = LongestChainResult.markerPositions;

            // Check for 4 chained cells at the last move
            if (LongestChainResult.count >= 4)
            {
                gameState = GameState.GameOver;
                winner = lastMoveCellType == CellType.Red ? Winner.Player : Winner.Computer;
            }

            // If no players have won, check for a draw
            if (gameState == GameState.Playing && CheckForDraw(grid))
            {
                gameState = GameState.GameOver;
                winner = Winner.Draw;
            }

            GameCheckResult gameCheckResult = new GameCheckResult(gameState, winner, winningMarkers);

            return gameCheckResult;
        }

        // Checks a grid for a draw
        private static bool CheckForDraw(Grid grid)
        {
            bool draw = true;

            // Loop through the grid and check for an empty cell
            for (int i = 0; i < grid._gridLayout._columns; i++)
            {
                for (int j = 0; j < grid._gridLayout._rows; j++)
                {
                    if (grid._gridCells[i, j]._cellType == CellType.Empty)
                    {
                        draw = false;
                    }
                }
            }

            return draw;
        }

        // Returns the longest chain of cells of a given type at a position in the grid aswell as the positions of those cells
        private static (int count, List<Vector2> markerPositions) GetLongestChainAtPosition(Grid grid, CellType cellType, int col, int row)
        {
            (int dx, int dy)[] directions = new (int dx, int dy)[]
            {
                (1, 0),  // Horizontal
                (0, 1),  // Vertical
                (1, 1),  // Diagonal
                (1, -1)  // Diagonal
            };

            int longestCount = 0;

            List<Vector2> longestMarkerPositions = new List<Vector2>(); 

            foreach ((int dx, int dy) direction in directions)
            {
                int currentCount = 1;

                var countMarkersPositiveResult = CountMarkers(grid, cellType, direction.dx, direction.dy, col, row);
                var countMarkersNegativeResult = CountMarkers(grid, cellType, -direction.dx, -direction.dy, col, row);

                // Check both sides of a direction
                currentCount += countMarkersPositiveResult.count;
                currentCount += countMarkersNegativeResult.count;

                if (currentCount > longestCount)
                {
                    longestCount = currentCount;

                    // Save the positions of the markers
                    longestMarkerPositions = new List<Vector2>();

                    // Make sure to also add the original marker
                    longestMarkerPositions.Add(new Vector2(col, row));

                    longestMarkerPositions.AddRange(countMarkersPositiveResult.markerPositions);
                    longestMarkerPositions.AddRange(countMarkersNegativeResult.markerPositions);
                }
            }

            return (longestCount, longestMarkerPositions);
        }

        // Counts the markers on a grid in a given direction
        private static (int count, List<Vector2> markerPositions) CountMarkers(Grid grid, CellType cellType, int dx, int dy, int col, int row)
        {
            int x = col;
            int y = row;

            int currentCount = 0;
            bool chainBroken = false;

            List<Vector2> markerPositions = new List<Vector2>();

            for (int i = 0; i < 4 && !chainBroken; i++)
            {
                x += dx;
                y += dy;

                // Check if position remains within bounds
                if (x >= 0 && x < grid._gridLayout._columns && y >= 0 && y < grid._gridLayout._rows)
                {
                    if (grid._gridCells[x, y]._cellType == cellType)
                    {
                        // Save the position of this marker
                        markerPositions.Add(new Vector2(x, y));

                        currentCount++;
                    }
                    else
                    {
                        chainBroken = true;
                    }
                }
            }

            return (currentCount, markerPositions);
        }
    }
}
