namespace WallpaperChanger {
	partial class SimpleTestForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			WallpaperUtils.WallpaperConfig wallpaperConfig1 = new WallpaperUtils.WallpaperConfig();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this._OKButton = new System.Windows.Forms.Button();
			this._OpenDisplaySettingsButton = new System.Windows.Forms.Button();
			this._StretchWallpaperAllScreensCB = new System.Windows.Forms.CheckBox();
			this._CancelButton = new System.Windows.Forms.Button();
			this._ApplyButton = new System.Windows.Forms.Button();
			this._MainSplitContainer = new WallpaperChanger.BetterSplitContainer();
			this._PreviewImageBox = new WallpaperChanger.PictureBoxExtended();
			this._WallpaperSettingsGB = new System.Windows.Forms.GroupBox();
			this._WallpaperPicker = new WallpaperUtils.WallpaperPicker();
			this._MainSplitContainer.Panel1.SuspendLayout();
			this._MainSplitContainer.Panel2.SuspendLayout();
			this._MainSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._PreviewImageBox)).BeginInit();
			this._WallpaperSettingsGB.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Filter = "Images - (JPEG, BMP, PNG) |*.jpg;*.bmp;*.png;*.jpeg";
			// 
			// _OKButton
			// 
			this._OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._OKButton.Location = new System.Drawing.Point(558, 414);
			this._OKButton.Name = "_OKButton";
			this._OKButton.Size = new System.Drawing.Size(75, 23);
			this._OKButton.TabIndex = 3;
			this._OKButton.Text = "&OK";
			this._OKButton.UseVisualStyleBackColor = true;
			// 
			// _OpenDisplaySettingsButton
			// 
			this._OpenDisplaySettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._OpenDisplaySettingsButton.Location = new System.Drawing.Point(12, 414);
			this._OpenDisplaySettingsButton.Name = "_OpenDisplaySettingsButton";
			this._OpenDisplaySettingsButton.Size = new System.Drawing.Size(120, 23);
			this._OpenDisplaySettingsButton.TabIndex = 0;
			this._OpenDisplaySettingsButton.Text = "Display &Properties";
			this._OpenDisplaySettingsButton.UseVisualStyleBackColor = true;
			this._OpenDisplaySettingsButton.Click += new System.EventHandler(this.OpenDisplayPropertiesButton_Click);
			// 
			// _StretchWallpaperAllScreensCB
			// 
			this._StretchWallpaperAllScreensCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._StretchWallpaperAllScreensCB.AutoSize = true;
			this._StretchWallpaperAllScreensCB.Location = new System.Drawing.Point(194, 417);
			this._StretchWallpaperAllScreensCB.Name = "_StretchWallpaperAllScreensCB";
			this._StretchWallpaperAllScreensCB.Size = new System.Drawing.Size(226, 19);
			this._StretchWallpaperAllScreensCB.TabIndex = 0;
			this._StretchWallpaperAllScreensCB.Text = "Stretch wallpaper across all screens";
			this._StretchWallpaperAllScreensCB.UseVisualStyleBackColor = true;
			this._StretchWallpaperAllScreensCB.CheckedChanged += new System.EventHandler(this.StretchWallpaperAllScreensCB_CheckedChanged);
			// 
			// _CancelButton
			// 
			this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._CancelButton.Location = new System.Drawing.Point(639, 414);
			this._CancelButton.Name = "_CancelButton";
			this._CancelButton.Size = new System.Drawing.Size(75, 23);
			this._CancelButton.TabIndex = 4;
			this._CancelButton.Text = "&Cancel";
			this._CancelButton.UseVisualStyleBackColor = true;
			// 
			// _ApplyButton
			// 
			this._ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._ApplyButton.Location = new System.Drawing.Point(720, 414);
			this._ApplyButton.Name = "_ApplyButton";
			this._ApplyButton.Size = new System.Drawing.Size(75, 23);
			this._ApplyButton.TabIndex = 5;
			this._ApplyButton.Text = "&Apply";
			this._ApplyButton.UseVisualStyleBackColor = true;
			this._ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
			// 
			// _MainSplitContainer
			// 
			this._MainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this._MainSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._MainSplitContainer.Location = new System.Drawing.Point(12, 12);
			this._MainSplitContainer.MinimumSize = new System.Drawing.Size(786, 310);
			this._MainSplitContainer.Name = "_MainSplitContainer";
			this._MainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// _MainSplitContainer.Panel1
			// 
			this._MainSplitContainer.Panel1.Controls.Add(this._PreviewImageBox);
			// 
			// _MainSplitContainer.Panel2
			// 
			this._MainSplitContainer.Panel2.Controls.Add(this._WallpaperSettingsGB);
			this._MainSplitContainer.Panel2MinSize = 220;
			this._MainSplitContainer.Size = new System.Drawing.Size(788, 391);
			this._MainSplitContainer.SplitterDistance = 163;
			this._MainSplitContainer.TabIndex = 2;
			// 
			// _PreviewImageBox
			// 
			this._PreviewImageBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this._PreviewImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._PreviewImageBox.Location = new System.Drawing.Point(0, 0);
			this._PreviewImageBox.Name = "_PreviewImageBox";
			this._PreviewImageBox.Size = new System.Drawing.Size(784, 159);
			this._PreviewImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this._PreviewImageBox.TabIndex = 3;
			this._PreviewImageBox.TabStop = false;
			this._PreviewImageBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PreviewBox_MouseClick);
			// 
			// _WallpaperSettingsGB
			// 
			this._WallpaperSettingsGB.Controls.Add(this._WallpaperPicker);
			this._WallpaperSettingsGB.Dock = System.Windows.Forms.DockStyle.Top;
			this._WallpaperSettingsGB.Location = new System.Drawing.Point(0, 0);
			this._WallpaperSettingsGB.MinimumSize = new System.Drawing.Size(0, 220);
			this._WallpaperSettingsGB.Name = "_WallpaperSettingsGB";
			this._WallpaperSettingsGB.Size = new System.Drawing.Size(784, 220);
			this._WallpaperSettingsGB.TabIndex = 1;
			this._WallpaperSettingsGB.TabStop = false;
			this._WallpaperSettingsGB.Text = "Wallpaper Settings";
			// 
			// _WallpaperPicker
			// 
			wallpaperConfig1.Argb = -16777216;
			wallpaperConfig1.BackgroundColor = System.Drawing.Color.Black;
			wallpaperConfig1.DeviceName = null;
			wallpaperConfig1.DirectoryPath = null;
			wallpaperConfig1.ImagePath = null;
			wallpaperConfig1.IncludeSubDirs = false;
			wallpaperConfig1.Name = null;
			wallpaperConfig1.SelectionStyle = WallpaperUtils.WallpaperSelectionStyle.None;
			wallpaperConfig1.StretchStyle = WallpaperUtils.WallpaperStretchStyle.Center;
			this._WallpaperPicker.Config = wallpaperConfig1;
			this._WallpaperPicker.Dock = System.Windows.Forms.DockStyle.Fill;
			this._WallpaperPicker.Location = new System.Drawing.Point(3, 19);
			this._WallpaperPicker.Name = "_WallpaperPicker";
			this._WallpaperPicker.Padding = new System.Windows.Forms.Padding(10);
			this._WallpaperPicker.Size = new System.Drawing.Size(778, 198);
			this._WallpaperPicker.TabIndex = 0;
			// 
			// SimpleTestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(807, 449);
			this.Controls.Add(this._ApplyButton);
			this.Controls.Add(this._CancelButton);
			this.Controls.Add(this._StretchWallpaperAllScreensCB);
			this.Controls.Add(this._OpenDisplaySettingsButton);
			this.Controls.Add(this._OKButton);
			this.Controls.Add(this._MainSplitContainer);
			this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MinimumSize = new System.Drawing.Size(815, 483);
			this.Name = "SimpleTestForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SimpleTestForm";
			this.Resize += new System.EventHandler(this.SimpleTestForm_Resize);
			this._MainSplitContainer.Panel1.ResumeLayout(false);
			this._MainSplitContainer.Panel2.ResumeLayout(false);
			this._MainSplitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._PreviewImageBox)).EndInit();
			this._WallpaperSettingsGB.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private PictureBoxExtended _PreviewImageBox;
		private BetterSplitContainer _MainSplitContainer;
		private WallpaperUtils.WallpaperPicker _WallpaperPicker;
		private System.Windows.Forms.Button _OKButton;
		private System.Windows.Forms.GroupBox _WallpaperSettingsGB;
		private System.Windows.Forms.Button _OpenDisplaySettingsButton;
		private System.Windows.Forms.CheckBox _StretchWallpaperAllScreensCB;
		private System.Windows.Forms.Button _CancelButton;
		private System.Windows.Forms.Button _ApplyButton;
	}
}