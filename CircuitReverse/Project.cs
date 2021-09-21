using System;
using System.Drawing;

namespace CircuitReverse
{
	public struct Crosshair
	{
		public Point location;
		public bool show;
	}
	public partial class MainForm
	{
		// Absolute path for project file
		public string ProjectFilePath = "";

		public Crosshair crosshair = new Crosshair() { location = new Point(0, 0), show = false };

		// Active command
		public AbstractTool ActiveTool = null;

		// Tool functions
		public void BeginWire(object s, EventArgs e)
		{
			CancelTool();
			toolWire.Checked = true;
			ActiveTool = new WireTool(toolLayerSelect.SelectedIndex, IsMouseOverImgPanel());
		}

		public void BeginPin(object s, EventArgs e)
		{
			CancelTool();
			toolPin.Checked = true;
			ActiveTool = new PinTool(toolLayerSelect.SelectedIndex, IsMouseOverImgPanel());
		}

		public void CancelTool(object s = null, EventArgs e = null)
		{
			ActiveTool = null;
			toolWire.Checked = false;
			toolPin.Checked = false;
		}
	}
}
