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
	public partial class LoadImageForm : Form
	{
		private Image img = null;
		private Point Crosshair;
		private bool ShowCrosshair = false;
		private List<Point> ClipPoints = new List<Point>();

		private readonly MainForm.LayerEnum layer;
		private readonly MainForm parent;

		public LoadImageForm(MainForm.LayerEnum l, MainForm p)
		{
			InitializeComponent();
			layer = l;
			parent = p;
			ShowDialog();
		}

		private void LoadImageForm_Load(object sender, EventArgs e)
		{
			if (OpenImageDialog.ShowDialog() == DialogResult.OK)
			{
				img = Image.FromFile(OpenImageDialog.FileName);
				ImagePanel.Invalidate();
			}
			else
			{
				Close();
			}
		}

		private void ImagePanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (!(img is null))
			{
				MainForm.DrawPanelImage(g, img, ImagePanel.Size);
				if (ShowCrosshair)
				{
					MainForm.DrawPanelCrosshair(g, ImagePanel.Size, Crosshair);
				}

				foreach ( var p in ClipPoints )
				{
					g.FillEllipse(Brushes.Red, p.X - 4, p.Y - 4, 8, 8);
				}
			}
		}

		private void ImagePanel_MouseMove(object sender, MouseEventArgs e)
		{
			Crosshair = e.Location;
			ImagePanel.Invalidate();
		}

		private void ImagePanel_MouseEnter(object sender, EventArgs e)
		{
			ShowCrosshair = true;
			ImagePanel.Invalidate();
		}

		private void ImagePanel_MouseLeave(object sender, EventArgs e)
		{
			ShowCrosshair = false;
			ImagePanel.Invalidate();
		}

		private void ImagePanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (ClipPoints.Count < 4)
			{
				ClipPoints.Add(e.Location);
				ImagePanel.Invalidate();
				if ( ClipPoints.Count == 4 )
				{
					ImageClipButton.Enabled = true;
				}
			}
		}

		private void ImageOKButton_Click(object sender, EventArgs e)
		{
			parent.SetImage(img, layer);
			Close();
		}

		private void ImageFlipButton_Click(object sender, EventArgs e)
		{
			img.RotateFlip(RotateFlipType.RotateNoneFlipX);
			ImagePanel.Invalidate();
		}

		private void ImageRotateLeftButton_Click(object sender, EventArgs e)
		{
			img.RotateFlip(RotateFlipType.Rotate270FlipNone);
			ImagePanel.Invalidate();
		}

		private void ImageRotateRightButton_Click(object sender, EventArgs e)
		{
			img.RotateFlip(RotateFlipType.Rotate90FlipNone);
			ImagePanel.Invalidate();
		}

		private void ImageClearClipButton_Click(object sender, EventArgs e)
		{
			ClipPoints.Clear();
			ImagePanel.Invalidate();
			ImageClipButton.Enabled = false;
		}

		private void ImageClipButton_Click(object sender, EventArgs e)
		{
			if ( ClipPoints.Count == 4 )
			{
				//
			}
		}
	}
}
