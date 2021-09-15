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

			TopPanel.Layer = LayerEnum.TOP;
			TopPanel.project = this;

			BottomPanel.Layer = LayerEnum.BOTTOM;
			BottomPanel.project = this;
		}

		// Resize event to make form responsive
		private void MainForm_Resize(object sender, EventArgs e)
		{
			// Resize TopPanel
			TopPanel.Size = new Size((Size.Width - 52) / 3, Size.Height - 116);

			// Align BottomPanel to TopPanel
			BottomPanel.Size = new Size(TopPanel.Size.Width, TopPanel.Size.Height);
			BottomPanel.Location = new Point(TopPanel.Location.X + TopPanel.Size.Width + 18, BottomPanel.Location.Y);

			// Align objectPropertyGrid to BottomPanel
			objectPropertyGrid.Size = new Size(TopPanel.Size.Width, (TopPanel.Size.Height - 6) / 2);
			objectPropertyGrid.Location = new Point(BottomPanel.Location.X + BottomPanel.Size.Width + 6, objectPropertyGrid.Location.Y);

			// Align objectList to objectPropertyGrid
			objectList.Size = objectPropertyGrid.Size;
			objectList.Location = new Point(objectPropertyGrid.Location.X, objectPropertyGrid.Location.Y + objectPropertyGrid.Size.Height + 6);
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

		// Invalidate panels in fix intervals instead of on every change to unload CPU
		private void RefreshTimer_Tick(object sender, EventArgs e)
		{
			TopPanel.Invalidate();
			BottomPanel.Invalidate();
		}

		private void saveProjectMenu_Click(object sender, EventArgs e)
		{
			SaveProject(false);
		}

		private void saveProjectAsMenu_Click(object sender, EventArgs e)
		{
			SaveProject(true);
		}

		private void SavePanelImg(BufferedPanel panel, ZipArchive archive, string entryName)
		{
			if (!(panel.img is null))
			{
				var entry = archive.CreateEntry(entryName);
				using (var s = entry.Open())
				{
					panel.img.Save(s, ImageFormat.Png);
				}
			}
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
				SavePanelImg(TopPanel, zip, "top.png");
				SavePanelImg(BottomPanel, zip, "bottom.png");
			}
			statusStripMain.Items["statusLabelDefault"].Text = "Project saved";
		}

		private void OpenPanelImg(BufferedPanel panel, ZipArchive archive, string entryName)
		{
			var entry = archive.GetEntry(entryName);
			if (!(entry is null))
			{
				using (var s = entry.Open())
				{
					panel.img = new Bitmap(s);
				}
			}
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
				OpenPanelImg(TopPanel, zip, "top.png");
				OpenPanelImg(BottomPanel, zip, "bottom.png");
			}
			statusStripMain.Items["statusLabelDefault"].Text = "Project loaded";
		}

		// Handle form key presses
		// TODO forward to active tool
		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Escape )
			{
				CancelTool(sender, e);
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
