using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CircuitReverse
{
	public delegate Point PanelTransform(Point p);

	// Enum for layers
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

	// Parent class for objects
	public abstract class AbstractObject {
		public ObjectType type = ObjectType.NONE;

		public LayerEnum layer = LayerEnum.BOTH;

		public AbstractObject(LayerEnum l)
		{
			layer = l;
		}

		public abstract void DrawObject(LayerEnum target_layer, PanelTransform tform, Graphics g);
		public abstract string ToString(int idx);
	}

	// Class for wires
	public class WireObject : AbstractObject
	{
		public Color WireColor = Color.Red;
		public string NetName = "";

		public List<Point> WirePoints = new List<Point>();

		public WireObject(LayerEnum l = LayerEnum.BOTH) : base(l)
		{
			type = ObjectType.LINE;
		}

		// Draw object on panel
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

		public override string ToString(int idx)
		{
			return string.Format("Line {0} : Net {1} : {2}", idx, NetName, WireColor.ToKnownColor().ToString());
		}
	}

	// Parent class for drawing commands
	public abstract class AbstractTool
	{
		public LayerEnum ActiveLayer = LayerEnum.NONE;

		public AbstractTool(int layer = 0)
		{
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

		public abstract bool ClickHandler(MouseEventArgs e);
		public abstract void MoveHandler(Point p);
		public abstract void PaintHandler(LayerEnum target_layer, PanelTransform tform, Graphics g);

		public abstract AbstractObject EndTool();
	}

	// Class for wire drawing tool
	public class WireTool : AbstractTool
	{
		public WireObject wire;

		public WireTool(int layer = 0) : base(layer)
		{
			wire = new WireObject(ActiveLayer);
			wire.WirePoints.Add(new Point());
		}

		// Place point or end tool
		public override bool ClickHandler(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				return true;
			}
			if (e.Button == MouseButtons.Left)
			{
				wire.WirePoints.Add(new Point());
			}
			return false;
		}

		// Update last point
		public override void MoveHandler(Point p)
		{
			wire.WirePoints[wire.WirePoints.Count - 1] = p;
		}

		// Draw wire
		public override void PaintHandler(LayerEnum target_layer, PanelTransform tform, Graphics g)
		{
			wire.DrawObject(target_layer, tform, g);
		}

		// End tool, return wire
		public override AbstractObject EndTool()
		{
			wire.WirePoints.RemoveAt(wire.WirePoints.Count - 1);
			return wire;
		}
	}
}
