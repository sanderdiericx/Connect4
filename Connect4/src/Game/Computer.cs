using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.src.Game
{
    internal class Computer
    {
        internal int _difficulty;
        internal bool _currentMiniMaxTurn;

        internal Computer(int difficulty)
        {
            _difficulty = difficulty;
        }

        // Calculates the best possible move on the board using the mini-max algorithm
        private static Vector2 CalculateBestMove(Grid grid)
        {
            CellType[,] startState = grid.ConvertToCellTypeArray();




        }

        // Counts all chains for a given cell in all four directions and returns the score calculated
        private int CountChainSum(CellType[,] state, CellType checkType, int col, int row)
        {
            (int dx, int dy)[] directions = new (int dx, int dy)[]
            {
                (1, 0),  // Horizontal
                (0, 1),  // Vertical
                (1, 1),  // Diagonal
                (1, -1)  // Diagonal
            };

            int totalScore = 0;

            foreach (var (dx, dy) in directions)
            {
                int countPositive = CountMarkers(state, checkType, dx, dy, col, row);
                int countNegative = CountMarkers(state, checkType, -dx, -dy, col, row);

                int chainLength = countPositive + countNegative;

                // Add the score based on chain length
                if (chainLength == 2)
                {
                    totalScore += 2;
                }
                else if (chainLength == 3)
                {
                    totalScore += 5;
                }
                else if (chainLength == 4) // Win
                {
                    totalScore += 1000;
                }
            }

            return totalScore;
        }


        // Returns the length of a chain in a given direction
        private int CountMarkers(CellType[,] state, CellType checkType, int dx, int dy, int col, int row)
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
                if (x >= 0 && x < state.GetLength(0) && y >= 0 && y < state.GetLength(1))
                {
                    if (state[x, y] == checkType)
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
