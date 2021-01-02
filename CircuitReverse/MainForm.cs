using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircuitReverse
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private Image TopImage = null;
		private Image BottomImage = null;

		private Point Crosshair;

		public enum LayerEnum { I_TOP, I_BOTTOM };

		private void MainForm_Resize(object sender, EventArgs e)
		{
			LoadTopButton.Location = new Point(12, Size.Height - 74);

			LoadBottomButton.Location = new Point(138, Size.Height - 74);
			
			TopPanel.Size = new Size(Size.Width / 3, Size.Height - 92);
			TopPanel.Invalidate();

			BottomPanel.Size = new Size(Size.Width / 3, Size.Height - 92);
			BottomPanel.Location = new Point(Size.Width / 3 + 18, BottomPanel.Location.Y);
			BottomPanel.Invalidate();

		}

		private void LoadTopButton_Click(object sender, EventArgs e)
		{
			new LoadImageForm(LayerEnum.I_TOP, this);
		}

		private void LoadBottomButton_Click(object sender, EventArgs e)
		{
			new LoadImageForm(LayerEnum.I_BOTTOM, this);
		}

		private void TopPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			if (!(TopImage is null))
			{
				DrawPanelImage(g, TopImage, TopPanel.Size);
			}

			DrawPanelCrosshair(g, TopPanel.Size, Crosshair);
		}

		private void BottomPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			if (!(BottomImage is null))
			{
				DrawPanelImage(g, BottomImage, BottomPanel.Size);
			}

			DrawPanelCrosshair(g, BottomPanel.Size, Crosshair);
		}

		private void TopBottomPanel_MouseMove(object sender, MouseEventArgs e)
		{
			Crosshair = e.Location;
			TopPanel.Invalidate();
			BottomPanel.Invalidate();
		}

		public void SetImage(Image img, LayerEnum layer)
		{
			if (layer == LayerEnum.I_TOP)
			{
				TopImage = img;
				TopPanel.Invalidate();
			}
			else if (layer == LayerEnum.I_BOTTOM)
			{
				BottomImage = img;
				BottomPanel.Invalidate();
			}
		}

		public static void DrawPanelImage(Graphics g, Image img, Size size)
		{
			var scale_x = size.Width / (float)img.Width;
			var scale_y = size.Height / (float)img.Height;

			if (scale_x > scale_y)
			{
				var w = img.Width * scale_y;
				var h = img.Height * scale_y;
				var fw = size.Width;
				g.DrawImage(img, (fw - w) / 2, 0, w, h);
			}
			else
			{
				var w = img.Width * scale_x;
				var h = img.Height * scale_x;
				var fh = size.Height;
				g.DrawImage(img, 0, (fh - h) / 2, w, h);
			}
		}

		public static void DrawPanelCrosshair(Graphics g, Size size, Point p)
		{
			Pen pn = new Pen(Color.Black, 1);
			g.DrawLine(pn, p.X, 0, p.X, size.Height);
			g.DrawLine(pn, 0, p.Y, size.Width, p.Y);
		}
	}
}
