namespace CircuitReverse
{
	partial class LoadImageForm
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
			this.ImageOKButton = new System.Windows.Forms.Button();
			this.OpenImageDialog = new System.Windows.Forms.OpenFileDialog();
			this.ImageFlipButton = new System.Windows.Forms.Button();
			this.ImageRotateLeftButton = new System.Windows.Forms.Button();
			this.ImageRotateRightButton = new System.Windows.Forms.Button();
			this.ImageClipButton = new System.Windows.Forms.Button();
			this.ImageClearClipButton = new System.Windows.Forms.Button();
			this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
			this.ImagePanel = new CircuitReverse.BufferedPanel();
			this.ImageScaleInput = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.ImageScaleInput)).BeginInit();
			this.SuspendLayout();
			// 
			// ImageOKButton
			// 
			this.ImageOKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ImageOKButton.Location = new System.Drawing.Point(287, 368);
			this.ImageOKButton.Name = "ImageOKButton";
			this.ImageOKButton.Size = new System.Drawing.Size(75, 23);
			this.ImageOKButton.TabIndex = 1;
			this.ImageOKButton.Text = "OK";
			this.ImageOKButton.UseVisualStyleBackColor = true;
			// 
			// OpenImageDialog
			// 
			this.OpenImageDialog.Filter = "Image|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif";
			// 
			// ImageFlipButton
			// 
			this.ImageFlipButton.Image = global::CircuitReverse.Properties.Resources.FlipHorizontal_16x;
			this.ImageFlipButton.Location = new System.Drawing.Point(12, 368);
			this.ImageFlipButton.Name = "ImageFlipButton";
			this.ImageFlipButton.Size = new System.Drawing.Size(23, 23);
			this.ImageFlipButton.TabIndex = 2;
			this.ImageFlipButton.UseVisualStyleBackColor = true;
			this.ImageFlipButton.Click += new System.EventHandler(this.ImageFlipButton_Click);
			// 
			// ImageRotateLeftButton
			// 
			this.ImageRotateLeftButton.Image = global::CircuitReverse.Properties.Resources.RotateLeft_16x;
			this.ImageRotateLeftButton.Location = new System.Drawing.Point(41, 368);
			this.ImageRotateLeftButton.Name = "ImageRotateLeftButton";
			this.ImageRotateLeftButton.Size = new System.Drawing.Size(23, 23);
			this.ImageRotateLeftButton.TabIndex = 3;
			this.ImageRotateLeftButton.UseVisualStyleBackColor = true;
			this.ImageRotateLeftButton.Click += new System.EventHandler(this.ImageRotateLeftButton_Click);
			// 
			// ImageRotateRightButton
			// 
			this.ImageRotateRightButton.Image = global::CircuitReverse.Properties.Resources.RotateRight_16x;
			this.ImageRotateRightButton.Location = new System.Drawing.Point(70, 368);
			this.ImageRotateRightButton.Name = "ImageRotateRightButton";
			this.ImageRotateRightButton.Size = new System.Drawing.Size(23, 23);
			this.ImageRotateRightButton.TabIndex = 4;
			this.ImageRotateRightButton.UseVisualStyleBackColor = true;
			this.ImageRotateRightButton.Click += new System.EventHandler(this.ImageRotateRightButton_Click);
			// 
			// ImageClipButton
			// 
			this.ImageClipButton.Enabled = false;
			this.ImageClipButton.Location = new System.Drawing.Point(99, 368);
			this.ImageClipButton.Name = "ImageClipButton";
			this.ImageClipButton.Size = new System.Drawing.Size(75, 23);
			this.ImageClipButton.TabIndex = 5;
			this.ImageClipButton.Text = "Clip";
			this.ImageClipButton.UseVisualStyleBackColor = true;
			this.ImageClipButton.Click += new System.EventHandler(this.ImageClipButton_Click);
			// 
			// ImageClearClipButton
			// 
			this.ImageClearClipButton.Image = global::CircuitReverse.Properties.Resources.Cancel_16x;
			this.ImageClearClipButton.Location = new System.Drawing.Point(180, 368);
			this.ImageClearClipButton.Name = "ImageClearClipButton";
			this.ImageClearClipButton.Size = new System.Drawing.Size(23, 23);
			this.ImageClearClipButton.TabIndex = 0;
			this.ImageClearClipButton.UseVisualStyleBackColor = true;
			this.ImageClearClipButton.Click += new System.EventHandler(this.ImageClearClipButton_Click);
			// 
			// RefreshTimer
			// 
			this.RefreshTimer.Enabled = true;
			this.RefreshTimer.Interval = 30;
			this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
			// 
			// ImagePanel
			// 
			this.ImagePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ImagePanel.Location = new System.Drawing.Point(12, 12);
			this.ImagePanel.Name = "ImagePanel";
			this.ImagePanel.Size = new System.Drawing.Size(350, 350);
			this.ImagePanel.TabIndex = 0;
			this.ImagePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ImagePanel_Paint);
			this.ImagePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImagePanel_MouseClick);
			this.ImagePanel.MouseEnter += new System.EventHandler(this.ImagePanel_MouseEnter);
			this.ImagePanel.MouseLeave += new System.EventHandler(this.ImagePanel_MouseLeave);
			this.ImagePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImagePanel_MouseMove);
			// 
			// ImageScaleInput
			// 
			this.ImageScaleInput.DecimalPlaces = 2;
			this.ImageScaleInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.ImageScaleInput.Location = new System.Drawing.Point(210, 370);
			this.ImageScaleInput.Name = "ImageScaleInput";
			this.ImageScaleInput.Size = new System.Drawing.Size(71, 20);
			this.ImageScaleInput.TabIndex = 6;
			// 
			// LoadImageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(374, 403);
			this.Controls.Add(this.ImageScaleInput);
			this.Controls.Add(this.ImageClearClipButton);
			this.Controls.Add(this.ImageClipButton);
			this.Controls.Add(this.ImageRotateRightButton);
			this.Controls.Add(this.ImageRotateLeftButton);
			this.Controls.Add(this.ImageFlipButton);
			this.Controls.Add(this.ImageOKButton);
			this.Controls.Add(this.ImagePanel);
			this.DoubleBuffered = true;
			this.MinimizeBox = false;
			this.Name = "LoadImageForm";
			this.Text = "Load image";
			this.Load += new System.EventHandler(this.LoadImageForm_Load);
			this.Resize += new System.EventHandler(this.LoadImageForm_Resize);
			((System.ComponentModel.ISupportInitialize)(this.ImageScaleInput)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BufferedPanel ImagePanel;
		private System.Windows.Forms.Button ImageOKButton;
		private System.Windows.Forms.OpenFileDialog OpenImageDialog;
		private System.Windows.Forms.Button ImageFlipButton;
		private System.Windows.Forms.Button ImageRotateLeftButton;
		private System.Windows.Forms.Button ImageRotateRightButton;
		private System.Windows.Forms.Button ImageClipButton;
		private System.Windows.Forms.Button ImageClearClipButton;
		private System.Windows.Forms.Timer RefreshTimer;
		private System.Windows.Forms.NumericUpDown ImageScaleInput;
	}
}