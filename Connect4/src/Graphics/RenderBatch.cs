using System.Collections.Generic;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Game;

namespace Connect4.src.Graphics
{
    internal class RenderBatch
    {
        private readonly List<Sprite> _sprites;

        internal void Clear()
        {
            _sprites.Clear();
        }

        internal RenderBatch()
        {
            _sprites = new List<Sprite>();
        }

        internal void AddSprite(Sprite sprite)
        {
            _sprites.Add(sprite);
        }

        internal void AddGrid(Grid grid)
        {
            foreach (var rectangle in grid._gameGrid)
            {
                _sprites.Add(rectangle);
            }
        }

        internal void Draw()
        {
            foreach (var sprite in _sprites)
            {
                sprite.RecalculatePixels();
                sprite.Draw();
            }
        }
    }
}
