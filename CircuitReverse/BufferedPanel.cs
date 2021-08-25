using System;
using System.Drawing;
using System.Windows.Forms;

namespace CircuitReverse
{
	// This is a double buffered Panel for visualizing the PCB layers
	// Used on MainForm and LoadImageForm
	public class BufferedPanel : Panel
	{
		// Reference to MainForm
		public MainForm project = null;

		// PCB image to show
		public Image img = null;
		public double ImageScale = 1.0;
		public float ImageWidthScale = 1; // for load image scaling preview

		// Layer
		public LayerEnum Layer = LayerEnum.NONE;

		public BufferedPanel()
		{
			DoubleBuffered = true;
			ResizeRedraw = true;

			// set panel event handlers
			Paint += new PaintEventHandler(PaintEvent);
			MouseMove += new MouseEventHandler(MouseMoveEvent);
			MouseEnter += new EventHandler(MouseEnterEvent);
			MouseLeave += new EventHandler(MouseLeaveEvent);
			MouseClick += new MouseEventHandler(MouseClickEvent);
		}

		// Paint event handler
		private void PaintEvent(object s, PaintEventArgs e)
		{
			if (project is null)
			{
				// mitigate design-time exception
				return;
			}

			var g = e.Graphics;
			DrawPanelImage(g);
			project.ActiveTool?.PaintHandler(Layer, ImageToPanel, g);

			foreach (var obj in project.ProjectObjects)
			{
				obj.DrawObject(Layer, ImageToPanel, g);
			}

			DrawPanelCrosshair(g);
		}

		// Mouse movement event handlers (set crosshair)
		public void MouseMoveEvent(object s, MouseEventArgs e)
		{
			if (!(img is null))
			{
				project.Crosshair = PanelToImage(e.Location);
			}
			project.ActiveTool?.MoveHandler(project.Crosshair);
		}

		public void MouseEnterEvent(object s, EventArgs e)
		{
			project.ShowCrosshair = true;
		}

		public void MouseLeaveEvent(object s, EventArgs e)
		{
			project.ShowCrosshair = false;
		}

		// Mouse click event handler
		private void MouseClickEvent(object s, MouseEventArgs e)
		{
			if (!(project.ActiveTool is null))
			{
				if (project.ActiveTool.ClickHandler(e))
				{
					project.EndTool();
				}
			}
		}

		// Draw the image on the panel
		// Called from the Paint event
		public bool DrawPanelImage(Graphics g, float whscale = 0)
		{
			if ( img is null )
			{
				// nothing to draw
				return false;
			}

			if ( whscale != 0 )
			{
				// scaling widht for load preview
				ImageWidthScale = (float)img.Size.Height / img.Size.Width / whscale;
			}

			// determine scale and draw image to best fit
			float imgw = img.Width * ImageWidthScale;
			float imgh = img.Height;

			var scale_x = Size.Width / imgw;
			var scale_y = Size.Height / imgh;

			if (scale_x > scale_y)
			{
				// center horizontally
				var w = imgw * scale_y;
				var h = imgh * scale_y;
				var fw = Size.Width;
				g.DrawImage(img, (fw - w) / 2, 0, w, h);
				ImageScale = scale_y;
			}
			else
			{
				// center vertically
				var w = imgw * scale_x;
				var h = imgh * scale_x;
				var fh = Size.Height;
				g.DrawImage(img, 0, (fh - h) / 2, w, h);
				ImageScale = scale_x;
			}

			return true;
		}

		// Draw crosshair on panel
		// Called from Paint event
		public void DrawPanelCrosshair(Graphics g)
		{
			if (project.ShowCrosshair && !(img is null))
			{
				Pen pn = new Pen(Color.Black, 1);
				Point p = ImageToPanel(project.Crosshair);
				g.DrawLine(pn, p.X, 0, p.X, Size.Height);
				g.DrawLine(pn, 0, p.Y, Size.Width, p.Y);
			}
		}

		// Get image corrdinates from panel coordinates
		public Point PanelToImage(Point p)
		{
			var x = (p.X - Size.Width / 2.0) / ImageScale + img.Size.Width / 2.0;
			var y = (p.Y - Size.Height / 2.0) / ImageScale + img.Size.Height / 2.0;
			return new Point(ImageClipper.dtoi(x), ImageClipper.dtoi(y));
		}

		// Get panel coordinates from image coordinates
		public Point ImageToPanel(Point p)
		{
			var x = (p.X - img.Size.Width / 2.0) * ImageScale + Size.Width / 2.0;
			var y = (p.Y - img.Size.Height / 2.0) * ImageScale + Size.Height / 2.0;
			return new Point(ImageClipper.dtoi(x), ImageClipper.dtoi(y));
		}
	}
}
