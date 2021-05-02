using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CircuitReverse
{
	public partial class LoadImageForm : Form
	{
		// List for clip reference points
		private List<Point> ClipPoints = new List<Point>();

		public LoadImageForm()
		{
			InitializeComponent();

			// Set tooltips
			tt.SetToolTip(ImageFlipButton, "Mirror image horizontally");
			tt.SetToolTip(ImageRotateLeftButton, "Rotate image 90 degrees left");
			tt.SetToolTip(ImageRotateRightButton, "Rotate image 90 degrees right");
			tt.SetToolTip(ImageScaleInput, "Height/Width ratio");
			tt.SetToolTip(ImageScaleButton, "Set image scale (removes projection points)");
			tt.SetToolTip(ImageClipButton, "Clip image projection");
			tt.SetToolTip(ImageResetButton, "Remove projection points and reset image scale");
		}

		//Return layer image
		public Image getImage()
		{
			return ImagePanel.img;
		}

		// Load image from file
		// Called from Form control
		private void LoadImageForm_Load(object sender, EventArgs e)
		{
			if (OpenImageDialog.ShowDialog() == DialogResult.OK)
			{
				ImagePanel.img = Image.FromFile(OpenImageDialog.FileName);
				ImageScaleInput.Value = (decimal)ImagePanel.img.Height / ImagePanel.img.Width;
			}
			else
			{
				Close();
			}
		}

		// Image preview panel Paint event
		// Called from Form control
		private void ImagePanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (ImagePanel.DrawPanelImage(g, (float)ImageScaleInput.Value))
			{
				foreach (var p in ClipPoints)
				{
					var pp = ImagePanel.ImageToPanel(p);
					g.FillEllipse(Brushes.Red, pp.X - 4, pp.Y - 4, 8, 8);
				}

				ImagePanel.DrawPanelCrosshair(g);
			}
		}

		// Move panel crosshair
		private void ImagePanel_MouseMove(object sender, MouseEventArgs e)
		{
			ImagePanel.Crosshair = e.Location;
		}

		private void ImagePanel_MouseEnter(object sender, EventArgs e)
		{
			ImagePanel.ShowCrosshair = true;
		}

		private void ImagePanel_MouseLeave(object sender, EventArgs e)
		{
			ImagePanel.ShowCrosshair = false;
		}

		// Place clip points, maximum 4
		private void ImagePanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (ClipPoints.Count < 4)
			{
				Point p = ImagePanel.PanelToImage(e.Location);
				if ( p.X < 0 || p.Y < 0 || p.X > ImagePanel.img.Size.Width || p.Y > ImagePanel.img.Size.Height )
				{
					return;
				}

				ClipPoints.Add(p);

				if ( ClipPoints.Count == 4 )
				{
					ImageClipButton.Enabled = true;
				}
			}
		}

		// Transform image
		private void ImageFlipButton_Click(object sender, EventArgs e)
		{
			ImagePanel.img.RotateFlip(RotateFlipType.RotateNoneFlipX);
		}

		private void ImageRotateLeftButton_Click(object sender, EventArgs e)
		{
			ImagePanel.img.RotateFlip(RotateFlipType.Rotate270FlipNone);
			ImageScaleInput.Value = 1 / ImageScaleInput.Value;
		}

		private void ImageRotateRightButton_Click(object sender, EventArgs e)
		{
			ImagePanel.img.RotateFlip(RotateFlipType.Rotate90FlipNone);
			ImageScaleInput.Value = 1 / ImageScaleInput.Value;
		}

		private void ImageScaleButton_Click(object sender, EventArgs e)
		{
			ImagePanel.img = ImageClipper.ScaleImage(ImagePanel.img, (float)ImageScaleInput.Value);
			ImageResetButton_Click(sender, e);
			MessageBox.Show("Image scale set", "Done");
		}

		private void ImageClipButton_Click(object sender, EventArgs e)
		{
			if (ClipPoints.Count == 4)
			{
				ImagePanel.img = ImageClipper.ClipImage(ImagePanel.img, ClipPoints);
				ImageResetButton_Click(sender, e);
			}
		}

		private void ImageResetButton_Click(object sender, EventArgs e)
		{
			ClipPoints.Clear();
			ImageScaleInput.Value = (decimal)ImagePanel.img.Height / ImagePanel.img.Width;
			ImageClipButton.Enabled = false;
		}

		// Resize evnet for responsive form
		private void LoadImageForm_Resize(object sender, EventArgs e)
		{
			ImagePanel.Size = new Size(Size.Width - 40, Size.Height - 92);

			ImageFlipButton.Location = new Point(12, Size.Height - 72);
			ImageRotateLeftButton.Location = new Point(41, Size.Height - 72);
			ImageRotateRightButton.Location = new Point(70, Size.Height - 72);
			ImageScaleInput.Location = new Point(99, Size.Height - 71); // different height
			ImageScaleButton.Location = new Point(200, Size.Height - 72);
			ImageClipButton.Location = new Point(229, Size.Height - 72);
			ImageResetButton.Location = new Point(258, Size.Height - 72);
			ImageOKButton.Location = new Point(Size.Width - 103, Size.Height - 72);
		}

		// Redraw image panel
		private void RefreshTimer_Tick(object sender, EventArgs e)
		{
			ImagePanel.Invalidate();
		}
	}
}
