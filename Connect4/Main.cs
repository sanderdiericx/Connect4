using Connect4.src.Graphics;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Main : Form
    {
        private Stopwatch _elapsedTime;

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Paint += Main_Paint;
            DoubleBuffered = true;

            GraphicsEngine.Start(Width, Height);

            GameLoop.LoadGame();

            // Set up frame timer
            Timer frameTimer = new Timer();
            frameTimer.Interval = 16; // 60FPS

            frameTimer.Tick += FrameTimer_Tick;
            frameTimer.Start();

            _elapsedTime = new Stopwatch();
            _elapsedTime.Start();
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            GraphicsEngine.SetDeltaTime((float)_elapsedTime.Elapsed.TotalSeconds);

            GameLoop.UpdateGame();
            GameLoop.RenderGame();

            Invalidate(); // Force the form to repaint
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(GraphicsEngine._frame, 0, 0);
        }
    }
}
