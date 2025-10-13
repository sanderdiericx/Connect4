using Connect4.src.Graphics;
using Connect4.src.Graphics.Animations;
using Connect4.src.Graphics.Sprites;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;

namespace Connect4.src.Game
{
    internal class Connect
    {
        private const float MARKER_COOLDOWN = 0.15f;
        private const float MARKER_DROP_SPEED = 0.5f;

        internal Grid _grid;
        internal Indicator _indicator;
        internal bool _playerTurn;

        private Stopwatch _markerCooldownTracker;

        internal Connect(GridLayout gridLayout)
        {
            _grid = new Grid(gridLayout);
            _indicator = new Indicator(_grid, gridLayout, 50, Color.Black, Color.Firebrick, 12);
            _playerTurn = true;

            _markerCooldownTracker = new Stopwatch();
            _markerCooldownTracker.Start();
        }

        internal void NextTurn()
        {
            _playerTurn = !_playerTurn;
        }

        // Drops a marker of a certain cellType if mouse is clicked. returns true if a marker was dropped
        internal bool TryDropMarker(CellType cellType)
        {
            bool markerDropped = false;

            int closestCol = _grid.GetClosestIndex();
            int furthestCell = _grid.FindFurthestCell(closestCol);

            if (GraphicsEngine._isMouseDown && _markerCooldownTracker.Elapsed.TotalSeconds > MARKER_COOLDOWN && furthestCell != -1)
            {
                // Drops a marker with an animation
                _grid.SetGridCell(closestCol, furthestCell, cellType, EasingFunctions.GetEaseOutBounce(), MARKER_DROP_SPEED);

                markerDropped = true;

                // Restart cooldown
                _markerCooldownTracker.Restart();
            }

            return markerDropped;
        }

        internal void GameOver(GameCheckResult gameCheckResult)
        {
            _indicator._triangle._isVisible = false;

            // Unhighlight selected cell
            Vector2 lastHighlight = _grid._lastHighlight;
            _grid._gridCells[(int)lastHighlight.X, (int)lastHighlight.Y]._cellRectangle.SetBorderColor(_grid._gridLayout._borderColor);

            if (gameCheckResult._winner != Winner.Draw)
            {
                // Create a rainbow animation for all winning markers
                foreach (var winningPosition in gameCheckResult._winningMarkers)
                {
                    // Create a chainAnimation to endlessly change the markers colors
                    Circle winningSprite = _grid._gridCells[(int)winningPosition.X, (int)winningPosition.Y]._cellMarker;
                    AnimationTarget animationTarget = new AnimationTarget(winningSprite, 1f, x => x);
                    List<ColorAnimation> animations = DefaultChainAnimations.GetRainbowAnimation(animationTarget);

                    GraphicsEngine.StartAnimationChain(animations, true);
                }
            }
        }

        // Scans the last move for 4 markers of the same type in a chain, if 4 are found it returns a new gamestate and the winner
        internal GameCheckResult CheckWinCondition()
        {
            GameState gameState = GameState.Playing;
            Winner winner = Winner.None;
            List<Vector2> winningMarkers = new List<Vector2>();

            Vector2 lastMove = _grid._lastMove;
            CellType lastMoveCellType = _grid._gridCells[(int)lastMove.X, (int)lastMove.Y]._cellType;

            var LongestChainResult = GetLongestChainAtPosition(_grid, lastMoveCellType, (int)lastMove.X, (int)lastMove.Y);
            winningMarkers = LongestChainResult.markerPositions;

            // Check for 4 chained cells at the last move
            if (LongestChainResult.count >= 4)
            {
                gameState = GameState.GameOver;
                winner = lastMoveCellType == CellType.Red ? Winner.Player : Winner.Computer;
            }

            // If no players have won, check for a draw
            if (gameState == GameState.Playing && CheckForDraw(_grid))
            {
                gameState = GameState.GameOver;
                winner = Winner.Draw;
            }

            GameCheckResult gameCheckResult = new GameCheckResult(gameState, winner, winningMarkers);

            return gameCheckResult;
        }

        // Show win label and reset button
        internal void ShowWinUI(GameCheckResult gameCheckResult)
        {
            string winnerText = "";
            Winner winner = gameCheckResult._winner;

            if (winner == Winner.Player)
            {
                winnerText = "Player has won!";
            }
            else if (winner == Winner.Computer)
            {
                winnerText = "Computer has won!";
            }
            else
            {
                winnerText = "Draw!";
            }

            Main._lblWinner.Text = winnerText;

            Main._lblWinner.Visible = true;
            Main._btnNewGame.Visible = true;
        }

        // Hide win label and reset button
        internal void HideWinUI()
        {
            Main._lblWinner.Visible = false;
            Main._btnNewGame.Visible = false;
        }

        // Updates game UI elements
        internal void UpdateUI()
        {
            if (GraphicsEngine._isMouseInside)
            {
                _indicator.UpdatePosition();
            }

            _grid.HighlightSelectedCell(_playerTurn ? Color.Firebrick : Color.Gold);
            _indicator.SetFillColor(_playerTurn ? Color.Firebrick : Color.Gold);
        }

        // Checks a grid for a draw
        private bool CheckForDraw(Grid grid)
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
        private (int count, List<Vector2> markerPositions) GetLongestChainAtPosition(Grid grid, CellType cellType, int col, int row)
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

            foreach (var (dx, dy) in directions)
            {
                int currentCount = 1;

                var countMarkersPositiveResult = CountMarkers(grid, cellType, dx, dy, col, row);
                var countMarkersNegativeResult = CountMarkers(grid, cellType, -dx, -dy, col, row);

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
        private (int count, List<Vector2> markerPositions) CountMarkers(Grid grid, CellType cellType, int dx, int dy, int col, int row)
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
