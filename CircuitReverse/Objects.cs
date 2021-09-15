using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CircuitReverse
{
	public delegate Point PanelTransform(Point p);

	public enum LayerEnum
	{
		NONE,
		TOP,
		BOTTOM,
		BOTH
	}

	public enum ObjectType
	{
		NONE,
		LINE
	}

	// Return type to show if the active tool needs to be deleted
	public enum ToolAction
	{
		NONE,
		RESET,
		ABORT,
		EXIT
	}

	// Parent class for objects to organize into list
	public abstract class AbstractObject {
		public ObjectType type = ObjectType.NONE;

		public LayerEnum layer = LayerEnum.BOTH;

		public AbstractObject(LayerEnum l)
		{
			layer = l;
		}

		public AbstractObject(AbstractObject o)
		{
			layer = o.layer;
		}

		public abstract void DrawObject(LayerEnum target_layer, PanelTransform tform, Graphics g);
	}

	public class WireObject : AbstractObject
	{
		public Color WireColor = Color.Red;
		public string NetName = "";

		public List<Point> WirePoints = new List<Point>();

		public WireObject(LayerEnum l = LayerEnum.BOTH) : base(l)
		{
			type = ObjectType.LINE;
		}

		public WireObject(WireObject w) : base(w)
		{
			type = ObjectType.LINE;
			WireColor = w.WireColor;
			NetName = w.NetName;
			WirePoints = new List<Point>(w.WirePoints);
		}

		public void RemoveLastPoint()
		{
			if ( WirePoints.Count > 0)
			{
				WirePoints.RemoveAt(WirePoints.Count - 1);
			}
		}

		public override void DrawObject(LayerEnum target_layer, PanelTransform tform, Graphics g)
		{
			if ( layer != LayerEnum.BOTH && target_layer != layer )
			{
				// no drawing on this layer
				return;
			}

			using (var p = new Pen(WireColor, 4))
			{
				for (int i = 0; i < WirePoints.Count - 1; i++)
				{
					g.DrawLine(p, tform(WirePoints[i]), tform(WirePoints[i + 1]));
				}
			}
		}

		public override string ToString()
		{
			return string.Format("LINE : Net {0} : {1}", NetName, WireColor.ToKnownColor().ToString());
		}
	}

	public class PinObject : AbstractObject
	{
		public Color PinColor = Color.Blue;
		public string NetName = "";
		public string Component = "";
		public int Number = -1;
		public Point Location;

		public PinObject(LayerEnum l) : base(l)
		{
			Location = new Point();
		}

		public PinObject(PinObject o) : base(o)
		{
			PinColor = o.PinColor;
			NetName = o.NetName;
			Number = o.Number;
			Location = o.Location;
		}

		// Draw pin on panel
		public override void DrawObject(LayerEnum target_layer, PanelTransform tform, Graphics g)
		{
			if (layer != LayerEnum.BOTH && target_layer != layer)
			{
				// no drawing on this layer
				return;
			}

			using ( var p = new Pen(PinColor, 2) )
			{
				const int hs = 5;
				var loc = tform(new Point(Location.X - hs, Location.Y - hs));
				g.DrawRectangle(p, loc.X, loc.Y, hs * 2, hs * 2);
			}
		}

		public override string ToString()
		{
			return string.Format("PIN : {0} : Net {1} : {2}", "", NetName, PinColor.ToKnownColor().ToString());
		}
	}

	// Parent class for drawing commands to generalize in a variable
	public abstract class AbstractTool
	{
		public LayerEnum ActiveLayer = LayerEnum.NONE;

		public AbstractTool(int layer = 0)
		{
			// interpret layer selection dropdown menu
			if (layer == 0)
			{
				ActiveLayer = LayerEnum.TOP;
			}
			else if (layer == 1)
			{
				ActiveLayer = LayerEnum.BOTTOM;
			}
			else
			{
				ActiveLayer = LayerEnum.BOTH;
			}
		}

		public abstract AbstractObject ResetAndGetObject();
		public abstract ToolAction ClickHandler(MouseEventArgs e);
		public abstract void MoveHandler(Point p);
		public abstract void PaintHandler(LayerEnum target_layer, PanelTransform tform, Graphics g);
	}

	public class WireTool : AbstractTool
	{
		public WireObject wire;

		public WireTool(int layer = 0) : base(layer)
		{
			ResetAndGetObject();
		}

		public override AbstractObject ResetAndGetObject()
		{
			// remove last point (it is not placed just drawn)
			wire?.RemoveLastPoint();

			// return this wire and create a new
			var tmp = wire;
			wire = new WireObject(ActiveLayer);
			wire.WirePoints.Add(new Point()); // last point is always updated to mouse position

			return tmp;
		}

		public override ToolAction ClickHandler(MouseEventArgs e)
		{
			// left click adds node to wire
			if (e.Button == MouseButtons.Left)
			{
				wire.WirePoints.Add(new Point());
			}

			// right click ends wire and begins a new one
			if (e.Button == MouseButtons.Right)
			{
				// save wire if it has at least 2 points (not including last one)
				if (wire.WirePoints.Count > 2)
				{
					return ToolAction.RESET;
				}

				// reset now if not, and return with no action
				ResetAndGetObject();
			}

			return ToolAction.NONE;
		}

		public override void MoveHandler(Point p)
		{
			wire.WirePoints[wire.WirePoints.Count - 1] = p;
		}

		public override void PaintHandler(LayerEnum target_layer, PanelTransform tform, Graphics g)
		{
			wire.DrawObject(target_layer, tform, g);
		}
	}

	// Class for pin drawing tool
	public class PinTool : AbstractTool
	{
		PinObject pin;

		public PinTool(int layer = 0) : base(layer)
		{
			pin = new PinObject(ActiveLayer);
		}

		public override AbstractObject ResetAndGetObject()
		{
			return new PinObject(pin);
		}

		public override ToolAction ClickHandler(MouseEventArgs e)
		{
			// left click places pin
			if (e.Button == MouseButtons.Left)
			{
				return ToolAction.RESET;
			}

			// right click exits tool
			if (e.Button == MouseButtons.Right)
			{
				return ToolAction.ABORT;
			}

			return ToolAction.NONE;
		}

		public override void MoveHandler(Point p)
		{
			pin.Location = p;
		}

		public override void PaintHandler(LayerEnum target_layer, PanelTransform tform, Graphics g)
		{
			pin.DrawObject(target_layer, tform, g);
		}
	}
}
