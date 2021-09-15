using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CircuitReverse
{
	public partial class MainForm
	{
		// Absolute path for project file
		public string ProjectFilePath = "";

		// Image panel crosshair
		public Point Crosshair = new Point(0, 0);
		public bool ShowCrosshair = false;

		// Active command
		public AbstractTool ActiveTool = null;

		// Tool functions
		public void BeginWire(object s, EventArgs e)
		{
			CancelTool();
			toolWire.Checked = true;
			ActiveTool = new WireTool(toolLayerSelect.SelectedIndex);
		}

		public void BeginPin(object s, EventArgs e)
		{
			CancelTool();
			toolPin.Checked = true;
			ActiveTool = new PinTool(toolLayerSelect.SelectedIndex);
		}

		public void CancelTool(object s = null, EventArgs e = null)
		{
			ActiveTool = null;
			toolWire.Checked = false;
			toolPin.Checked = false;
		}
	}
}
