using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.src.Game
{
    internal struct GameCheckResult
    {
        internal GameState _gameState;
        internal Winner _winner;
        internal List<Vector2> _winningMarkers;

        internal GameCheckResult(GameState gameState, Winner winner, List<Vector2> winningMarkers)
        {
            _gameState = gameState;
            _winner = winner;
            _winningMarkers = winningMarkers;
        }
    }
}
