using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CircuitReverse
{
	public struct RelativePoint
	{
		public double X, Y;

		public RelativePoint(double x, double y)
		{
			X = x;
			Y = y;
		}

		public RelativePoint(RelativePoint p)
		{
			X = p.X;
			Y = p.Y;
		}
	}

	public delegate Point PanelTransform(RelativePoint p);

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
	public abstract class AbstractObject
	{
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

		public List<RelativePoint> WirePoints = new List<RelativePoint>();
		public RelativePoint ActivePoint = new RelativePoint(0, 0);
		public bool ShowActivePoint = false;

		public WireObject(LayerEnum l = LayerEnum.BOTH) : base(l)
		{
			type = ObjectType.LINE;
		}

		public WireObject(WireObject w) : base(w)
		{
			type = ObjectType.LINE;
			WireColor = w.WireColor;
			NetName = w.NetName;
			WirePoints = new List<RelativePoint>(w.WirePoints);
		}

		public void AddActivePoint()
		{
			WirePoints.Add(ActivePoint);
		}

		private void DrawLine(RelativePoint p1, RelativePoint p2, PanelTransform tform, Graphics g)
		{
			using (var p = new Pen(WireColor, 4))
			{
				g.DrawLine(p, tform(p1), tform(p2));
			}
		}

		public override void DrawObject(LayerEnum target_layer, PanelTransform tform, Graphics g)
		{
			if (layer != LayerEnum.BOTH && target_layer != layer)
			{
				// no drawing on this layer
				return;
			}

			for (int i = 0; i < WirePoints.Count - 1; i++)
			{
				DrawLine(WirePoints[i], WirePoints[i + 1], tform, g);
			}

			if (ShowActivePoint && WirePoints.Count > 0)
			{
				DrawLine(WirePoints[WirePoints.Count - 1], ActivePoint, tform, g);
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
		public RelativePoint Location;

		public PinObject(LayerEnum l) : base(l)
		{
			Location = new RelativePoint();
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

			using (var p = new Pen(PinColor, 2))
			{
				const int hs = 5;
				var loc = tform(new RelativePoint(Location.X - hs, Location.Y - hs));
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
		public abstract void MoveHandler(RelativePoint p);
		public abstract void PaintHandler(LayerEnum target_layer, PanelTransform tform, Graphics g);
		public abstract void MouseFocusHandler(bool hover);
		public abstract void KeyHandler(Keys key);
	}

	public class WireTool : AbstractTool
	{
		public WireObject wire;

		public WireTool(int layer = 0, bool mouseover = false) : base(layer)
		{
			ResetAndGetObject();
			wire.ShowActivePoint = mouseover;
		}

		public override AbstractObject ResetAndGetObject()
		{
			// return this wire and create a new
			var tmp = wire;
			wire = new WireObject(ActiveLayer);
			return tmp;
		}

		public override ToolAction ClickHandler(MouseEventArgs e)
		{
			// left click adds node to wire
			if (e.Button == MouseButtons.Left)
			{
				wire.AddActivePoint();
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

		public override void MoveHandler(RelativePoint p)
		{
			wire.ActivePoint = p;
		}

		public override void PaintHandler(LayerEnum target_layer, PanelTransform tform, Graphics g)
		{
			wire.DrawObject(target_layer, tform, g);
		}

		public override void MouseFocusHandler(bool hover)
		{
			wire.ShowActivePoint = hover;
		}

		public override void KeyHandler(Keys key)
		{
			if (key == Keys.Back)
			{
				if (wire.WirePoints.Count > 0)
				{
					wire.WirePoints.RemoveAt(wire.WirePoints.Count - 1);
				}
			}
		}
	}

	public class PinTool : AbstractTool
	{
		PinObject pin;
		bool show = false;

		public PinTool(int layer = 0, bool mouseover = false) : base(layer)
		{
			pin = new PinObject(ActiveLayer);
			show = mouseover;
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

		public override void MoveHandler(RelativePoint p)
		{
			pin.Location = p;
		}

		public override void PaintHandler(LayerEnum target_layer, PanelTransform tform, Graphics g)
		{
			if (show)
			{
				pin.DrawObject(target_layer, tform, g);
			}
		}

		public override void MouseFocusHandler(bool hover)
		{
			show = hover;
		}

		public override void KeyHandler(Keys key)
		{
		}
	}
}
