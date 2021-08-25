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

			// set panel layer values
			TopPanel.Layer = LayerEnum.TOP;
			TopPanel.project = this;

			BottomPanel.Layer = LayerEnum.BOTTOM;
			BottomPanel.project = this;
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

		// Redraw panel
		private void RefreshTimer_Tick(object sender, EventArgs e)
		{
			TopPanel.Invalidate();
			BottomPanel.Invalidate();
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

		// Open project
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

		// Form key handler
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
