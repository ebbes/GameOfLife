using System;
using System.Windows.Forms;
using System.Drawing;

namespace gameoflife
{
	public class MainForm : Form
	{
		private GameOfLife game;
		private PictureBox gameBox;
		private Button advance;
		private Timer timer;
		private Button startstop;
		private Button clear;
		private Button reinit;
		private NumericUpDown width;
		private NumericUpDown height;
		private TextBox rule;
		private Point gameBoxOffset;
		
		#region Manual generated code
		private void InitializeComponent ()
		{
			gameBox = new PictureBox ();
			gameBox.Parent = this;
			gameBox.Location = new Point (5, 5);
			gameBox.Size = new Size (ClientSize.Width - 10, ClientSize.Height - 40);
			gameBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			gameBox.Paint += game_Paint;
			gameBox.MouseUp += game_MouseUp;
			Controls.Add (gameBox);
			
			advance = new Button ();
			advance.Parent = this;
			advance.Location = new Point (5, ClientSize.Height - 35);
			advance.Size = new Size (120, 30);
			advance.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			advance.Text = "Next generation";
			advance.Click += advance_Click;
			Controls.Add (advance);
			
			startstop = new Button ();
			startstop.Parent = this;
			startstop.Location = new Point (advance.Location.X + advance.Width + 5, advance.Location.Y);
			startstop.Size = new Size (100, 30);
			startstop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			startstop.Text = "Start Timer";
			startstop.Click += startstop_Click;
			Controls.Add (startstop);
			
			clear = new Button ();
			clear.Parent = this;
			clear.Location = new Point (startstop.Location.X + startstop.Width + 5, startstop.Location.Y);
			clear.Size = new Size (80, 30);
			clear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			clear.Text = "Clear";
			clear.Click += (sender, e) => ReinitBoard (0);
			Controls.Add (clear);
			
			width = new NumericUpDown ();
			width.Width = 50;
			width.Location = new Point (clear.Location.X + clear.Width + 5, clear.Location.Y + (clear.Height - width.Height) / 2);
			width.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			width.Minimum = 10;
			width.Maximum = 128;
			width.Value = 48;
			Controls.Add (width);
			
			height = new NumericUpDown ();
			height.Width = 50;
			height.Location = new Point (width.Location.X + width.Width + 5, width.Location.Y);
			height.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			height.Minimum = 10;
			height.Maximum = 128;
			height.Value = 48;
			Controls.Add (height);
			
			rule = new TextBox ();
			rule.Width = 80;
			rule.Location = new Point (height.Location.X + height.Width + 5, height.Location.Y);
			rule.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			rule.Text = "23/3";
			Controls.Add (rule);
			
			reinit = new Button ();
			reinit.Parent = this;
			reinit.Location = new Point (rule.Location.X + rule.Width + 5, clear.Location.Y);
			reinit.Size = new Size (80, 30);
			reinit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			reinit.Text = "New";
			reinit.Click += (sender, e) => ReinitBoard (30);
			Controls.Add (reinit);

			timer = new Timer ();
			timer.Enabled = false;
			timer.Interval = 500;
			timer.Tick += (sender, e) => advance_Click (sender, e);
			
			Resize += (sender, e) => gameBox.Invalidate ();
		}
		#endregion
		
		public MainForm ()
		{			
			InitializeComponent ();
			
			Width = reinit.Left + reinit.Width + 5 + (Width - ClientSize.Width);
			Height = Height - gameBox.Height + gameBox.Width;
			
			ReinitBoard (30);
		}
		
		private void advance_Click (object sender, EventArgs e)
		{
			game.AdvanceGeneration ();
			
			Text = "Game of Life - " + game.ToString ();
			
			gameBox.Invalidate ();
		}
		
		private void ReinitBoard (int percentage)
		{
			game = new GameOfLife ((int)width.Value, (int)height.Value, percentage, new GameRule (rule.Text));
			Text = "Game of Life - " + game.ToString ();
			
			gameBox.Invalidate ();
		}
		
		private void startstop_Click (object sender, EventArgs e)
		{
			timer.Enabled = !timer.Enabled;
			startstop.Text = timer.Enabled ? "Stop timer" : "Start timer";
		}
		
		private void game_MouseUp (object sender, MouseEventArgs e)
		{
			int width = gameBox.Width / game.Width;
			int height = gameBox.Height / game.Height;
			
			int size = Math.Min (width, height);
			
			Point p = gameBox.PointToClient (this.PointToScreen (e.Location));
			
			int x = (p.X - gameBoxOffset.X) / size;
			int y = (p.Y - gameBoxOffset.Y) / size;
			
			if (0 <= x && x < game.Width && 0 <= y && y < game.Height) {
				game.ToggleCellState (y, x);
				gameBox.Invalidate ();
			}
		}
		
		private void game_Paint (object sender, PaintEventArgs e)
		{
			e.Graphics.Clear (this.BackColor);
			
			int width = gameBox.Width / game.Width;
			int height = gameBox.Height / game.Height;
			
			int size = Math.Min (width, height);
			
			gameBoxOffset = new Point (gameBox.Width / 2 - game.Width * size / 2, gameBox.Height / 2 - game.Height * size / 2);
			
			for (int y = 0; y < game.Height; y++) {
				for (int x = 0; x < game.Width; x++) {
					Brush b;
					if (game.IsCellAlive (y, x))
						b = Brushes.Black;
					else
						b = Brushes.White;
					e.Graphics.FillRectangle (b, gameBoxOffset.X + x * size, gameBoxOffset.Y + y * size, size, size);
				}
			}
		}
	}
}

