using System.Collections.Generic;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Game;

namespace Connect4.src.Graphics
{
    internal class RenderBatch
    {
        private readonly List<Sprite> sprites;

        internal void Clear()
        {
            sprites.Clear();
        }

        internal RenderBatch()
        {
            sprites = new List<Sprite>();
        }

        internal void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        internal void AddGrid(Grid grid)
        {
            foreach (var rectangle in grid.GameGrid)
            {
                sprites.Add(rectangle);
            }
        }

        internal void Draw()
        {
            foreach (var sprite in sprites)
            {
                sprite.Draw();
            }
        }
    }
}
