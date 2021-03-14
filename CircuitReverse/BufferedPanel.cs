using System.Drawing;
using System.Windows.Forms;

namespace CircuitReverse
{
	public class BufferedPanel : Panel
	{
		public BufferedPanel()
		{
			DoubleBuffered = true;
			ResizeRedraw = true;
		}

		public Image img;
		public double ImageScale;
		public float ImageWidthScale = 1;

		public Point Crosshair;
		public bool ShowCrosshair = false;

		public bool DrawPanelImage(Graphics g, float whscale = 0)
		{
			if ( img is null )
			{
				return false;
			}

			if ( whscale != 0 )
			{
				ImageWidthScale = (float)img.Size.Height / img.Size.Width / whscale;
			}

			float imgw = img.Width * ImageWidthScale;
			float imgh = img.Height;

			var scale_x = Size.Width / imgw;
			var scale_y = Size.Height / imgh;

			if (scale_x > scale_y)
			{
				var w = imgw * scale_y;
				var h = imgh * scale_y;
				var fw = Size.Width;
				g.DrawImage(img, (fw - w) / 2, 0, w, h);
				ImageScale = scale_y;
			}
			else
			{
				var w = imgw * scale_x;
				var h = imgh * scale_x;
				var fh = Size.Height;
				g.DrawImage(img, 0, (fh - h) / 2, w, h);
				ImageScale = scale_x;
			}

			return true;
		}

		public void DrawPanelCrosshair(Graphics g)
		{
			if (ShowCrosshair)
			{
				Pen pn = new Pen(Color.Black, 1);
				g.DrawLine(pn, Crosshair.X, 0, Crosshair.X, Size.Height);
				g.DrawLine(pn, 0, Crosshair.Y, Size.Width, Crosshair.Y);
			}
		}

		public Point PanelToImage(Point p)
		{
			var x = (p.X - Size.Width / 2.0) / ImageScale + img.Size.Width / 2.0;
			var y = (p.Y - Size.Height / 2.0) / ImageScale + img.Size.Height / 2.0;
			return new Point(ImageClipper.dtoi(x), ImageClipper.dtoi(y));
		}

		public Point ImageToPanel(Point p)
		{
			var x = (p.X - img.Size.Width / 2.0) * ImageScale + Size.Width / 2.0;
			var y = (p.Y - img.Size.Height / 2.0) * ImageScale + Size.Height / 2.0;
			return new Point(ImageClipper.dtoi(x), ImageClipper.dtoi(y));
		}
	}
}
