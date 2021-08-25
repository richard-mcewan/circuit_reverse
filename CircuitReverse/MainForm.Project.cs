using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CircuitReverse
{
	public partial class MainForm : Form
	{
		// Absolute path for project file
		public string ProjectFilePath = "";

		// Image panel crosshair
		public Point Crosshair = new Point(0, 0);
		public bool ShowCrosshair = false;

		// Active command
		public AbstractTool ActiveTool = null;

		// Project objects
		public List<AbstractObject> ProjectObjects = new List<AbstractObject>();

		// Begin tools
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
			// TODO set active tool
		}

		// End tool
		public void CancelTool(object s = null, EventArgs e = null)
		{
			ActiveTool = null;
			toolWire.Checked = false;
			toolPin.Checked = false;
		}

		public void EndTool()
		{
			ProjectObjects.Add(ActiveTool.EndTool());
			CancelTool();
		}
	}
}
