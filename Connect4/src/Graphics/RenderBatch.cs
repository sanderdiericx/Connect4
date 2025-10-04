using Connect4.src.Graphics.Sprites;
using System.Collections.Generic;

namespace Connect4.src.Graphics
{
    // Renderbatch holds all sprites that need to be drawn each frame
    internal class RenderBatch
    {
        internal readonly List<Sprite> _sprites;

        internal RenderBatch()
        {
            _sprites = new List<Sprite>();
        }

        internal void Clear()
        {
            _sprites.Clear();
        }

        internal void Draw()
        {
            // Lock bitmap bits
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, GraphicsEngine._frame.Width, GraphicsEngine._frame.Height);
            var bmpData = GraphicsEngine._frame.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, GraphicsEngine._frame.PixelFormat);

            foreach (var sprite in _sprites)
            {
                if (sprite != null)
                {
                    sprite.Draw(bmpData);
                }
            }

            GraphicsEngine._frame.UnlockBits(bmpData);
        }
    }
}
