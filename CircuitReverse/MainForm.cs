using System;
using System.Drawing;
using System.Windows.Forms;

namespace CircuitReverse
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private Point Crosshair;

		private void MainForm_Resize(object sender, EventArgs e)
		{
			LoadTopButton.Location = new Point(12, Size.Height - 74);

			LoadBottomButton.Location = new Point(138, Size.Height - 74);
			
			TopPanel.Size = new Size(Size.Width / 3, Size.Height - 92);

			BottomPanel.Size = new Size(Size.Width / 3, Size.Height - 92);
			BottomPanel.Location = new Point(Size.Width / 3 + 18, BottomPanel.Location.Y);

		}

		private void LoadTopButton_Click(object sender, EventArgs e)
		{
			using (var f = new LoadImageForm())
			{
				if ( f.ShowDialog() == DialogResult.OK )
				{
					TopPanel.img = f.getImage();
				}
			}
		}

		private void LoadBottomButton_Click(object sender, EventArgs e)
		{
			using (var f = new LoadImageForm())
			{
				if (f.ShowDialog() == DialogResult.OK)
				{
					BottomPanel.img = f.getImage();
				}
			}
		}

		private void TopPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			TopPanel.DrawPanelImage(g);

			DrawPanelCrosshair(g, TopPanel.Size, Crosshair);
		}

		private void BottomPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			BottomPanel.DrawPanelImage(g);

			DrawPanelCrosshair(g, BottomPanel.Size, Crosshair);
		}

		private void TopBottomPanel_MouseMove(object sender, MouseEventArgs e)
		{
			Crosshair = e.Location;
		}

		public static void DrawPanelCrosshair(Graphics g, Size size, Point p)
		{
			Pen pn = new Pen(Color.Black, 1);
			g.DrawLine(pn, p.X, 0, p.X, size.Height);
			g.DrawLine(pn, 0, p.Y, size.Width, p.Y);
		}

		private void RefreshTimer_Tick(object sender, EventArgs e)
		{
			TopPanel.Invalidate();
			BottomPanel.Invalidate();
		}
	}
}
