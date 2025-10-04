using System.Collections.Generic;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Game;
using System.Windows.Forms;

namespace Connect4.src.Graphics
{
    internal class RenderBatch
    {
        internal readonly List<Sprite> _sprites;

        internal void Clear()
        {
            _sprites.Clear();
        }

        internal RenderBatch()
        {
            _sprites = new List<Sprite>();
        }

        internal void Draw()
        {
            // Lock bitmap bits
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, GraphicsEngine._frame.Width, GraphicsEngine._frame.Height);
            var bmpData = GraphicsEngine._frame.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, GraphicsEngine._frame.PixelFormat);

            foreach (var sprite in _sprites)
            {
                sprite.Draw(bmpData);
            }

            GraphicsEngine._frame.UnlockBits(bmpData);
        }
    }
}
