namespace WallpaperUtils {
	partial class MultiWallpaperPicker {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			WallpaperUtils.WallpaperConfig wallpaperConfig1 = new WallpaperUtils.WallpaperConfig();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._screenComboBox = new System.Windows.Forms.ComboBox();
			this.wpPicker = new WallpaperUtils.WallpaperPicker();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._screenComboBox);
			this.groupBox1.Controls.Add(this.wpPicker);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(434, 246);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Screen Settings";
			// 
			// _screenComboBox
			// 
			this._screenComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._screenComboBox.FormattingEnabled = true;
			this._screenComboBox.Items.AddRange(new object[] {
            "Screen 1",
            "Screen 2"});
			this._screenComboBox.Location = new System.Drawing.Point(6, 19);
			this._screenComboBox.Name = "_screenComboBox";
			this._screenComboBox.Size = new System.Drawing.Size(121, 21);
			this._screenComboBox.TabIndex = 0;
			this._screenComboBox.SelectedIndexChanged += new System.EventHandler(this._screenComboBox_SelectedIndexChanged);
			// 
			// wpPicker
			// 
			this.wpPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			wallpaperConfig1.BackgroundColor = System.Drawing.Color.Black;
			wallpaperConfig1.DirectoryPath = null;
			wallpaperConfig1.ImagePath = null;
			wallpaperConfig1.IncludeSubDirs = false;
			wallpaperConfig1.Name = null;
			wallpaperConfig1.SelectionStyle = WallpaperUtils.WallpaperSelectionStyle.None;
			wallpaperConfig1.StretchStyle = WallpaperUtils.WallpaperStretchStyle.Center;
			this.wpPicker.Config = wallpaperConfig1;
			this.wpPicker.Location = new System.Drawing.Point(6, 46);
			this.wpPicker.MinimumSize = new System.Drawing.Size(431, 188);
			this.wpPicker.Name = "wpPicker";
			this.wpPicker.Size = new System.Drawing.Size(431, 188);
			this.wpPicker.TabIndex = 1;
			this.wpPicker.ConfigChanged += new System.EventHandler<WallpaperUtils.ConfigChangedEventArgs>(this.wpPicker_ConfigChanged);
			// 
			// MultiWallpaperPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "MultiWallpaperPicker";
			this.Size = new System.Drawing.Size(434, 246);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private WallpaperPicker wpPicker;
		private System.Windows.Forms.ComboBox _screenComboBox;
	}
}
