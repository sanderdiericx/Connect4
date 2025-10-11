using System;

namespace Connect4.src.Game
{
    internal static class Connect4
    {
        // Scans the last move for 4 markers of the same type in a chain, if 4 are found it returns a new gamestate and the winner
        internal static (GameState gameState, Winner winner) CheckWinCondition(Grid grid)
        {
            GameState gameState = GameState.Playing;
            Winner winner = Winner.None;

            (int col, int row) lastMove = grid._lastMove;
            CellType lastMoveCellType = grid._gridCells[lastMove.col, lastMove.row]._cellType;

            // Check for 4 chained cells at the last move
            if (GetLongestChainAtPosition(grid, lastMoveCellType, lastMove.col, lastMove.row) == 4)
            {
                gameState = GameState.GameOver;
                winner = lastMoveCellType == CellType.Red ? Winner.Player : Winner.Computer;
            }

            return (gameState, winner);
        }

        // Returns the longest chain of cells of a given type at a position in the grid
        private static int GetLongestChainAtPosition(Grid grid, CellType cellType, int col, int row)
        {
            (int dx, int dy)[] directions = new (int dx, int dy)[]
            {
                (1, 0),  // Horizontal
                (0, 1),  // Vertical
                (1, 1),  // Diagonal
                (1, -1)  // Diagonal
            };

            int longestCount = 0;

            foreach ((int dx, int dy) direction in directions)
            {
                int currentCount = 1;

                // Check both sides of a direction
                currentCount += CountMarkers(grid, cellType, direction.dx, direction.dy, col, row);
                currentCount += CountMarkers(grid, cellType, -direction.dx, -direction.dy, col, row);

                if (currentCount > longestCount)
                {
                    longestCount = currentCount;
                }
            }

            return longestCount;
        }

        // Counts the markers on a grid in a given direction
        private static int CountMarkers(Grid grid, CellType cellType, int dx, int dy, int col, int row)
        {
            int x = col;
            int y = row;

            int currentCount = 0;
            bool chainBroken = false;

            for (int i = 0; i < 4 && !chainBroken; i++)
            {
                x += dx;
                y += dy;

                // Check if position remains within bounds
                if (x >= 0 && x < grid._gridLayout._columns && y >= 0 && y < grid._gridLayout._rows)
                {
                    if (grid._gridCells[x, y]._cellType == cellType)
                    {
                        currentCount++;
                    }
                    else
                    {
                        chainBroken = true;
                    }
                }
            }

            return currentCount;
        }
    }
}
