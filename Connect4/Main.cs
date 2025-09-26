using Connect4.src.Game;
using Connect4.src.Graphics;
using Connect4.src.Logs;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Main : Form
    {
        private Stopwatch _stopwatch;

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _stopwatch = new Stopwatch();

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
            GraphicsEngine.IncrementFrameTick();

            bool tickHit = GraphicsEngine._frameTick == 60;

            if (tickHit)
            {
                _stopwatch.Restart();
            }

            GameLoop.UpdateGame();

            if (tickHit)
            {
                _stopwatch.Stop();
                Logger.LogInfo($"Game updated in {_stopwatch.ElapsedMilliseconds}ms");
                _stopwatch.Restart();
            }

            GameLoop.RenderGame();

            if (tickHit)
            {
                _stopwatch.Stop();
                Logger.LogInfo($"Game rendered in {_stopwatch.ElapsedMilliseconds}ms");
            }

            Invalidate(); // Force the form to repaint
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(GraphicsEngine._frame, 0, 0);
        }
    }
}
