using Connect4.src.Graphics;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Connect4.src.Game;

namespace Connect4
{
    public partial class Main : Form
    {
        private Stopwatch _elapsedTime;

        internal static Button _btnNewGame;
        internal static Label _lblWinner;

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

            // Set up form controls
            _btnNewGame = btnNewGame;
            _lblWinner = lblWinner;

            _btnNewGame.Location = new Point(Width - _btnNewGame.Width - 130, _btnNewGame.Location.Y);
            _lblWinner.Location = new Point(130, _lblWinner.Location.Y);

            _btnNewGame.Visible = false;
            _lblWinner.Visible = false;

            // Set up frame timer
            Timer frameTimer = new Timer();
            frameTimer.Interval = 16; // 60FPS

            frameTimer.Tick += FrameTimer_Tick;
            frameTimer.Start(); // Start game

            _elapsedTime = new Stopwatch();
            _elapsedTime.Start();
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            GraphicsEngine.SetDeltaTime((float)_elapsedTime.Elapsed.TotalSeconds);
            GraphicsEngine._windowMousePosition = PointToClient(Cursor.Position);

            GameLoop.UpdateGame();
            GameLoop.RenderGame();

            Invalidate(); // Force the form to repaint
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(GraphicsEngine._frame, 0, 0);
        }

        /*
         * Mouse Events
         */

        private void Main_MouseEnter(object sender, EventArgs e)
        {
            GraphicsEngine._isMouseInside = true;
        }

        private void Main_MouseLeave(object sender, EventArgs e)
        {
            GraphicsEngine._isMouseInside = false;
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            GraphicsEngine._isMouseDown = true;
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            GraphicsEngine._isMouseDown = false;
        }

        private void btnNewGame_MouseDown(object sender, MouseEventArgs e)
        {
            GraphicsEngine._btnNewGameClicked = true;
        }

        private void btnNewGame_MouseUp(object sender, MouseEventArgs e)
        {
            GraphicsEngine._btnNewGameClicked = false;
        }
    }
}
