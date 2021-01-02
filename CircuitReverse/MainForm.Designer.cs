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
			this.LoadTopButton = new System.Windows.Forms.Button();
			this.TopPanel = new BufferedPanel();
			this.BottomPanel = new BufferedPanel();
			this.LoadBottomButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// LoadTopButton
			// 
			this.LoadTopButton.Location = new System.Drawing.Point(12, 426);
			this.LoadTopButton.Name = "LoadTopButton";
			this.LoadTopButton.Size = new System.Drawing.Size(120, 23);
			this.LoadTopButton.TabIndex = 0;
			this.LoadTopButton.Text = "Load Top Image";
			this.LoadTopButton.UseVisualStyleBackColor = true;
			this.LoadTopButton.Click += new System.EventHandler(this.LoadTopButton_Click);
			// 
			// TopPanel
			// 
			this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.TopPanel.Location = new System.Drawing.Point(12, 12);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(300, 408);
			this.TopPanel.TabIndex = 1;
			this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TopPanel_Paint);
			this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopBottomPanel_MouseMove);
			// 
			// BottomPanel
			// 
			this.BottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.BottomPanel.Location = new System.Drawing.Point(318, 12);
			this.BottomPanel.Name = "BottomPanel";
			this.BottomPanel.Size = new System.Drawing.Size(288, 408);
			this.BottomPanel.TabIndex = 2;
			this.BottomPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.BottomPanel_Paint);
			this.BottomPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopBottomPanel_MouseMove);
			// 
			// LoadBottomButton
			// 
			this.LoadBottomButton.Location = new System.Drawing.Point(138, 426);
			this.LoadBottomButton.Name = "LoadBottomButton";
			this.LoadBottomButton.Size = new System.Drawing.Size(120, 23);
			this.LoadBottomButton.TabIndex = 3;
			this.LoadBottomButton.Text = "Load Bottom Image";
			this.LoadBottomButton.UseVisualStyleBackColor = true;
			this.LoadBottomButton.Click += new System.EventHandler(this.LoadBottomButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 461);
			this.Controls.Add(this.LoadBottomButton);
			this.Controls.Add(this.BottomPanel);
			this.Controls.Add(this.TopPanel);
			this.Controls.Add(this.LoadTopButton);
			this.DoubleBuffered = true;
			this.Name = "MainForm";
			this.Text = "CircuitReverse";
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button LoadTopButton;
		private BufferedPanel TopPanel;
		private BufferedPanel BottomPanel;
		private System.Windows.Forms.Button LoadBottomButton;
	}
}

