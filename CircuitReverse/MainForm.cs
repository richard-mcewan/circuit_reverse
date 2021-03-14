using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace CircuitReverse
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private string ProjectFilePath = "";

		private bool NetActive = false;
		private NetLine ActiveLine = new NetLine();

		private void MainForm_Resize(object sender, EventArgs e)
		{
			TopPanel.Size = new Size(Size.Width / 3, Size.Height - 91);

			BottomPanel.Size = new Size(Size.Width / 3, Size.Height - 91);
			BottomPanel.Location = new Point(Size.Width / 3 + 18, 27);
		}

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

		private void DrawLayer(object sender, Graphics g)
		{
			var layer = sender as BufferedPanel;
			var p = new Pen(ActiveLine.LineColor, 4);
			for (int i = 0; i < ActiveLine.LinePoints.Count - 1; i++)
			{
				g.DrawLine(p, layer.ImageToPanel(ActiveLine.LinePoints[i]), layer.ImageToPanel(ActiveLine.LinePoints[i + 1]));
			}
			if (NetActive)
			{
				g.DrawLine(p, layer.ImageToPanel(ActiveLine.LinePoints[ActiveLine.LinePoints.Count - 1]), layer.Crosshair);
			}
		}

		private void RefreshTimer_Tick(object sender, EventArgs e)
		{
			TopPanel.Invalidate();
			BottomPanel.Invalidate();
		}

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

		private void TopBottomPanel_MouseClick(object sender, MouseEventArgs e)
		{
			var layer = sender as BufferedPanel;
			if (e.Button == MouseButtons.Left)
			{
				if (!NetActive) {
					ActiveLine.LinePoints.Clear();
					NetActive = true;
				}
				ActiveLine.LinePoints.Add(layer.PanelToImage(e.Location));
			}
			else if (e.Button == MouseButtons.Right)
			{
				NetActive = false;
			}
		}

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
			if (ProjectFilePath == "" || saveas)
			{
				if (ProjectSaveDialog.ShowDialog() == DialogResult.OK)
				{
					ProjectFilePath = ProjectSaveDialog.FileName;
					Text = "CircuitReverse - " + ProjectFilePath;
				}
				else
				{
					return;
				}
			}

			using (var zip = new ZipArchive(new FileStream(ProjectFilePath, FileMode.Create), ZipArchiveMode.Create))
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

		private void openProjectMenu_Click(object sender, EventArgs e)
		{
			if (ProjectOpenDialog.ShowDialog() == DialogResult.OK)
			{
				ProjectFilePath = ProjectOpenDialog.FileName;
				Text = "CircuitReverse - " + ProjectFilePath;
			}
			else
			{
				return;
			}

			using (var zip = new ZipArchive(new FileStream(ProjectFilePath, FileMode.Open), ZipArchiveMode.Read))
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
	}
}
