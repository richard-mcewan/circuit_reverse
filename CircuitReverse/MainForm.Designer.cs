namespace CircuitReverse
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
			this.statusStripMain = new System.Windows.Forms.StatusStrip();
			this.StatusLabelDefault = new System.Windows.Forms.ToolStripStatusLabel();
			this.ProjectSaveDialog = new System.Windows.Forms.SaveFileDialog();
			this.BottomPanel = new CircuitReverse.BufferedPanel();
			this.TopPanel = new CircuitReverse.BufferedPanel();
			this.ProjectOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.objectPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadTopImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadBottomImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStripMain = new System.Windows.Forms.MenuStrip();
			this.toolStripMain = new System.Windows.Forms.ToolStrip();
			this.toolNetLine = new System.Windows.Forms.ToolStripButton();
			this.toolPin = new System.Windows.Forms.ToolStripButton();
			this.toolCancel = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolLayerSelect = new System.Windows.Forms.ToolStripComboBox();
			this.statusStripMain.SuspendLayout();
			this.menuStripMain.SuspendLayout();
			this.toolStripMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// RefreshTimer
			// 
			this.RefreshTimer.Enabled = true;
			this.RefreshTimer.Interval = 30;
			this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
			// 
			// statusStripMain
			// 
			this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabelDefault});
			this.statusStripMain.Location = new System.Drawing.Point(0, 439);
			this.statusStripMain.Name = "statusStripMain";
			this.statusStripMain.Size = new System.Drawing.Size(884, 22);
			this.statusStripMain.TabIndex = 5;
			// 
			// StatusLabelDefault
			// 
			this.StatusLabelDefault.Name = "StatusLabelDefault";
			this.StatusLabelDefault.Size = new System.Drawing.Size(0, 17);
			// 
			// ProjectSaveDialog
			// 
			this.ProjectSaveDialog.FileName = "project.zip";
			this.ProjectSaveDialog.Filter = "ZIP file|*.zip";
			// 
			// BottomPanel
			// 
			this.BottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.BottomPanel.Location = new System.Drawing.Point(300, 52);
			this.BottomPanel.Name = "BottomPanel";
			this.BottomPanel.Size = new System.Drawing.Size(282, 384);
			this.BottomPanel.TabIndex = 2;
			this.BottomPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.BottomPanel_Paint);
			this.BottomPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TopBottomPanel_MouseClick);
			this.BottomPanel.MouseEnter += new System.EventHandler(this.TopBottomPanel_MouseEnter);
			this.BottomPanel.MouseLeave += new System.EventHandler(this.TopBottomPanel_MouseLeave);
			this.BottomPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopBottomPanel_MouseMove);
			// 
			// TopPanel
			// 
			this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.TopPanel.Location = new System.Drawing.Point(12, 52);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(282, 384);
			this.TopPanel.TabIndex = 1;
			this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TopPanel_Paint);
			this.TopPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TopBottomPanel_MouseClick);
			this.TopPanel.MouseEnter += new System.EventHandler(this.TopBottomPanel_MouseEnter);
			this.TopPanel.MouseLeave += new System.EventHandler(this.TopBottomPanel_MouseLeave);
			this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopBottomPanel_MouseMove);
			// 
			// ProjectOpenDialog
			// 
			this.ProjectOpenDialog.Filter = "ZIP file|*.zip";
			// 
			// objectPropertyGrid
			// 
			this.objectPropertyGrid.Location = new System.Drawing.Point(588, 52);
			this.objectPropertyGrid.Name = "objectPropertyGrid";
			this.objectPropertyGrid.Size = new System.Drawing.Size(284, 384);
			this.objectPropertyGrid.TabIndex = 6;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTopImageToolStripMenuItem,
            this.loadBottomImageToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
            this.openProjectToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// loadTopImageToolStripMenuItem
			// 
			this.loadTopImageToolStripMenuItem.Name = "loadTopImageToolStripMenuItem";
			this.loadTopImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.loadTopImageToolStripMenuItem.Text = "Load Top Image";
			this.loadTopImageToolStripMenuItem.Click += new System.EventHandler(this.LoadTopMenu_Click);
			// 
			// loadBottomImageToolStripMenuItem
			// 
			this.loadBottomImageToolStripMenuItem.Name = "loadBottomImageToolStripMenuItem";
			this.loadBottomImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.loadBottomImageToolStripMenuItem.Text = "Load Bottom Image";
			this.loadBottomImageToolStripMenuItem.Click += new System.EventHandler(this.LoadBottomMenu_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
			// 
			// saveProjectToolStripMenuItem
			// 
			this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
			this.saveProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.saveProjectToolStripMenuItem.Text = "Save project";
			this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectMenu_Click);
			// 
			// saveProjectAsToolStripMenuItem
			// 
			this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
			this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.saveProjectAsToolStripMenuItem.Text = "Save project as...";
			this.saveProjectAsToolStripMenuItem.Click += new System.EventHandler(this.saveProjectAsMenu_Click);
			// 
			// openProjectToolStripMenuItem
			// 
			this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
			this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.openProjectToolStripMenuItem.Text = "Open project";
			this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectMenu_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
			// 
			// menuStripMain
			// 
			this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1});
			this.menuStripMain.Location = new System.Drawing.Point(0, 0);
			this.menuStripMain.Name = "menuStripMain";
			this.menuStripMain.Size = new System.Drawing.Size(884, 24);
			this.menuStripMain.TabIndex = 4;
			this.menuStripMain.Text = "menuStrip1";
			// 
			// toolStripMain
			// 
			this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCancel,
            this.toolNetLine,
            this.toolPin,
            this.toolStripSeparator2,
            this.toolLayerSelect});
			this.toolStripMain.Location = new System.Drawing.Point(0, 24);
			this.toolStripMain.Name = "toolStripMain";
			this.toolStripMain.Size = new System.Drawing.Size(884, 25);
			this.toolStripMain.TabIndex = 7;
			this.toolStripMain.Text = "toolStrip1";
			// 
			// toolNetLine
			// 
			this.toolNetLine.CheckOnClick = true;
			this.toolNetLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolNetLine.Image = global::CircuitReverse.Properties.Resources.AssociationRelationship_16x;
			this.toolNetLine.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolNetLine.Name = "toolNetLine";
			this.toolNetLine.Size = new System.Drawing.Size(23, 22);
			this.toolNetLine.Text = "toolStripButton1";
			this.toolNetLine.ToolTipText = "Draw line";
			// 
			// toolPin
			// 
			this.toolPin.CheckOnClick = true;
			this.toolPin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolPin.Image = global::CircuitReverse.Properties.Resources.Pin_16x;
			this.toolPin.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolPin.Name = "toolPin";
			this.toolPin.Size = new System.Drawing.Size(23, 22);
			this.toolPin.Text = "toolStripButton2";
			this.toolPin.ToolTipText = "Draw pin";
			// 
			// toolCancel
			// 
			this.toolCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolCancel.Image = global::CircuitReverse.Properties.Resources.Cancel_16x;
			this.toolCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolCancel.Name = "toolCancel";
			this.toolCancel.Size = new System.Drawing.Size(23, 22);
			this.toolCancel.ToolTipText = "Cancel action (Esc)";
			this.toolCancel.Click += new System.EventHandler(this.toolCancel_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolLayerSelect
			// 
			this.toolLayerSelect.Items.AddRange(new object[] {
            "Top Layer (0)",
            "Bottom Layer (1)",
            "Both layers (2)"});
			this.toolLayerSelect.Name = "toolLayerSelect";
			this.toolLayerSelect.Size = new System.Drawing.Size(121, 25);
			this.toolLayerSelect.Text = "Both layers (2)";
			this.toolLayerSelect.ToolTipText = "Select layer to draw on";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 461);
			this.Controls.Add(this.toolStripMain);
			this.Controls.Add(this.objectPropertyGrid);
			this.Controls.Add(this.statusStripMain);
			this.Controls.Add(this.BottomPanel);
			this.Controls.Add(this.TopPanel);
			this.Controls.Add(this.menuStripMain);
			this.DoubleBuffered = true;
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStripMain;
			this.Name = "MainForm";
			this.Text = "CircuitReverse";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.statusStripMain.ResumeLayout(false);
			this.statusStripMain.PerformLayout();
			this.menuStripMain.ResumeLayout(false);
			this.menuStripMain.PerformLayout();
			this.toolStripMain.ResumeLayout(false);
			this.toolStripMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private BufferedPanel TopPanel;
		private BufferedPanel BottomPanel;
		private System.Windows.Forms.Timer RefreshTimer;
		private System.Windows.Forms.StatusStrip statusStripMain;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabelDefault;
		private System.Windows.Forms.SaveFileDialog ProjectSaveDialog;
		private System.Windows.Forms.OpenFileDialog ProjectOpenDialog;
		private System.Windows.Forms.PropertyGrid objectPropertyGrid;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadTopImageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadBottomImageToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.MenuStrip menuStripMain;
		private System.Windows.Forms.ToolStrip toolStripMain;
		private System.Windows.Forms.ToolStripButton toolNetLine;
		private System.Windows.Forms.ToolStripButton toolPin;
		private System.Windows.Forms.ToolStripButton toolCancel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripComboBox toolLayerSelect;
	}
}

