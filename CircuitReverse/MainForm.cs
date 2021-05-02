using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace CircuitReverse
{
	public enum ActiveTool
	{
		TOOL_NONE,
		TOOL_LINE,
		TOOL_PIN
	}

	public partial class MainForm : Form
	{
		private Project project = new Project();

		private bool NetActive = false;
		private int ActiveLine = 0;

		public MainForm()
		{
			InitializeComponent();
			
			// set panel layer numbers
			TopPanel.LayerNumber = 0;
			BottomPanel.LayerNumber = 1;
		}

		// Resize event for responsive form
		private void MainForm_Resize(object sender, EventArgs e)
		{
			TopPanel.Size = new Size((Size.Width - 52) / 3, Size.Height - 116);

			BottomPanel.Size = new Size((Size.Width - 52) / 3, Size.Height - 116);
			BottomPanel.Location = new Point((Size.Width - 52) / 3 + 18, BottomPanel.Location.Y);

			objectPropertyGrid.Size = new Size((Size.Width - 52) / 3, Size.Height - 116);
			objectPropertyGrid.Location = new Point((Size.Width - 52) * 2 / 3 + 24, objectPropertyGrid.Location.Y);
		}

		// Load images with LoadImageForm
		private void LoadTopMenu_Click(object sender, EventArgs e)
		{
			using (var f = new LoadImageForm())
			{
				if (f.ShowDialog() == DialogResult.OK)
				{
					TopPanel.img = f.getImage();
				}
			}
		}

		private void LoadBottomMenu_Click(object sender, EventArgs e)
		{
			using (var f = new LoadImageForm())
			{
				if (f.ShowDialog() == DialogResult.OK)
				{
					BottomPanel.img = f.getImage();
				}
			}
		}

		// Panel Paint events
		// Called from Form Control
		private void TopPanel_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			if (TopPanel.DrawPanelImage(g))
			{
				DrawLayer(sender, g);
			}
			TopPanel.DrawPanelCrosshair(g);
		}

		private void BottomPanel_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			if (BottomPanel.DrawPanelImage(g))
			{
				DrawLayer(sender, g);
			}
			BottomPanel.DrawPanelCrosshair(g);
		}

		// Draw panel layers
		// Called by Paint events
		private void DrawLayer(object sender, Graphics g)
		{
			var layer = sender as BufferedPanel;

			// Draw netlines
			foreach (var line in project.Lines)
			{
				var p = new Pen(line.LineColor, 4);
				var linepoints = line.LinePoints[layer.LayerNumber];
				for (int i = 0; i < linepoints.Count - 1; i++)
				{
					g.DrawLine(p, layer.ImageToPanel(linepoints[i]), layer.ImageToPanel(linepoints[i + 1]));
				}
			}

			// Draw end of active net line
			if (NetActive)
			{
				var line = project.Lines[ActiveLine];
				var p = new Pen(line.LineColor, 4);
				var linepoints = line.LinePoints[layer.LayerNumber];
				g.DrawLine(p, layer.ImageToPanel(linepoints[linepoints.Count - 1]), layer.Crosshair);
			}
		}

		// Redraw panel
		private void RefreshTimer_Tick(object sender, EventArgs e)
		{
			TopPanel.Invalidate();
			BottomPanel.Invalidate();
		}

		// Set crosshair
		private void TopBottomPanel_MouseMove(object sender, MouseEventArgs e)
		{
			TopPanel.Crosshair = e.Location;
			BottomPanel.Crosshair = e.Location;
		}

		private void TopBottomPanel_MouseEnter(object sender, EventArgs e)
		{
			TopPanel.ShowCrosshair = true;
			BottomPanel.ShowCrosshair = true;
		}

		private void TopBottomPanel_MouseLeave(object sender, EventArgs e)
		{
			TopPanel.ShowCrosshair = false;
			BottomPanel.ShowCrosshair = false;
		}

		// Panel mouse click event, active tool action
		private void TopBottomPanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (!NetActive) {
					NetActive = true;
					project.Lines.Add(new NetLine());
					ActiveLine = project.Lines.Count - 1;
				}
				project.Lines[ActiveLine].LinePoints[0].Add(TopPanel.CrosshairToImage());
				project.Lines[ActiveLine].LinePoints[1].Add(BottomPanel.CrosshairToImage());
			}
			else if (e.Button == MouseButtons.Right)
			{
				NetActive = false;
				if ( project.Lines[ActiveLine].LinePoints[0].Count < 2 && project.Lines[ActiveLine].LinePoints[1].Count < 2)
				{
					project.Lines.RemoveAt(ActiveLine);
				}
			}
		}

		// Cancel active tool
		private void toolCancel_Click(object sender, EventArgs e)
		{

		}

		// Save project
		private void saveProjectMenu_Click(object sender, EventArgs e)
		{
			SaveProject(false);
		}

		private void saveProjectAsMenu_Click(object sender, EventArgs e)
		{
			SaveProject(true);
		}

		private void SaveProject(bool saveas)
		{
			if (project.ProjectFilePath == "" || saveas)
			{
				if (ProjectSaveDialog.ShowDialog() == DialogResult.OK)
				{
					project.ProjectFilePath = ProjectSaveDialog.FileName;
					Text = "CircuitReverse - " + project.ProjectFilePath;
				}
				else
				{
					return;
				}
			}

			using (var zip = new ZipArchive(new FileStream(project.ProjectFilePath, FileMode.Create), ZipArchiveMode.Create))
			{
				if (!(TopPanel.img is null))
				{
					var top = zip.CreateEntry("top.png");
					using (var s = top.Open())
					{
						TopPanel.img.Save(s, ImageFormat.Png);
					}
				}

				if (!(BottomPanel.img is null))
				{
					var bot = zip.CreateEntry("bottom.png");
					using (var s = bot.Open())
					{
						BottomPanel.img.Save(s, ImageFormat.Png);
					}
				}
			}
			statusStripMain.Items["statusLabelDefault"].Text = "Project saved";
		}

		// Open project
		private void openProjectMenu_Click(object sender, EventArgs e)
		{
			if (ProjectOpenDialog.ShowDialog() == DialogResult.OK)
			{
				project.ProjectFilePath = ProjectOpenDialog.FileName;
				Text = "CircuitReverse - " + project.ProjectFilePath;
			}
			else
			{
				return;
			}

			using (var zip = new ZipArchive(new FileStream(project.ProjectFilePath, FileMode.Open), ZipArchiveMode.Read))
			{
				var top = zip.GetEntry("top.png");
				if ( !(top is null))
				{
					using ( var s = top.Open())
					{
						TopPanel.img = new Bitmap(s);
					}
				}

				var bot = zip.GetEntry("bottom.png");
				if (!(bot is null))
				{
					using (var s = bot.Open())
					{
						BottomPanel.img = new Bitmap(s);
					}
				}
			}
			statusStripMain.Items["statusLabelDefault"].Text = "Project loaded";
		}

		// Form key handler
		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Escape )
			{
				toolCancel_Click(sender, e);
			}
			else if ( e.KeyCode == Keys.D0 )
			{
				toolLayerSelect.SelectedIndex = 0;
			}
			else if (e.KeyCode == Keys.D1)
			{
				toolLayerSelect.SelectedIndex = 1;
			}
			else if (e.KeyCode == Keys.D2)
			{
				toolLayerSelect.SelectedIndex = 2;
			}
		}
	}
}
