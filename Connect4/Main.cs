using Connect4.src.Game;
using Connect4.src.Graphics;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Paint += Main_Paint;
            DoubleBuffered = true;

            GraphicsEngine.Start(Width, Height);

            // Set up frame timer
            Timer frameTimer = new Timer();
            frameTimer.Interval = 16; // 60FPS

            frameTimer.Tick += FrameTimer_Tick;
            frameTimer.Start();

            GameLoop.LoadGame();
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            GameLoop.UpdateGame();
            GameLoop.RenderGame();

            Invalidate(); // Force the form to repaint
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(GraphicsEngine.Frame, 0, 0);
        }
    }
}
