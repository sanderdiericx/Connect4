using System.Collections.Generic;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Game;

namespace Connect4.src.Graphics
{
    internal class RenderBatch
    {
        private readonly List<Sprite> sprites;

        public void Clear()
        {
            sprites.Clear();
        }


        public RenderBatch()
        {
            sprites = new List<Sprite>();
        }

        public void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        public void AddGrid(Grid grid)
        {
            foreach (var rectangle in grid.GameGrid)
            {
                sprites.Add(rectangle);
            }
        }

        public void Draw()
        {
            foreach (var sprite in sprites)
            {
                sprite.Draw();
            }
        }
    }
}
