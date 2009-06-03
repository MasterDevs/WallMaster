namespace WallpaperChanger
{
    partial class WallmasterUI
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
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WallmasterUI));
					this._statusStrip = new System.Windows.Forms.StatusStrip();
					this._mainMenuStrip = new System.Windows.Forms.MenuStrip();
					this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
					this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this._setWPButton = new System.Windows.Forms.Button();
					this._picBox = new System.Windows.Forms.PictureBox();
					this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
					this._notifyIconCM = new System.Windows.Forms.ContextMenuStrip(this.components);
					this.showHideApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.changeWallpaperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
					this._changeAllButton = new System.Windows.Forms.Button();
					this._changePaperTimer = new System.Windows.Forms.Timer(this.components);
					this.timeSpanPicker1 = new WallpaperChanger.TimeSpanPicker();
					this.multiWpPicker = new WallpaperUtils.MultiWallpaperPicker();
					this._mainMenuStrip.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this._picBox)).BeginInit();
					this._notifyIconCM.SuspendLayout();
					this.SuspendLayout();
					// 
					// _statusStrip
					// 
					this._statusStrip.Location = new System.Drawing.Point(0, 707);
					this._statusStrip.Name = "_statusStrip";
					this._statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
					this._statusStrip.Size = new System.Drawing.Size(1190, 22);
					this._statusStrip.TabIndex = 0;
					this._statusStrip.Text = "statusStrip1";
					// 
					// _mainMenuStrip
					// 
					this._mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
					this._mainMenuStrip.Location = new System.Drawing.Point(0, 0);
					this._mainMenuStrip.Name = "_mainMenuStrip";
					this._mainMenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
					this._mainMenuStrip.Size = new System.Drawing.Size(1190, 24);
					this._mainMenuStrip.TabIndex = 1;
					this._mainMenuStrip.Text = "menuStrip1";
					// 
					// fileToolStripMenuItem
					// 
					this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
					this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
					this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
					this.fileToolStripMenuItem.Text = "&File";
					// 
					// openToolStripMenuItem
					// 
					this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
					this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.openToolStripMenuItem.Name = "openToolStripMenuItem";
					this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
					this.openToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
					this.openToolStripMenuItem.Text = "&Open Configuration";
					this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
					// 
					// saveToolStripMenuItem
					// 
					this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
					this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
					this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
					this.saveToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
					this.saveToolStripMenuItem.Text = "&Save Configuration";
					this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
					// 
					// toolStripSeparator1
					// 
					this.toolStripSeparator1.Name = "toolStripSeparator1";
					this.toolStripSeparator1.Size = new System.Drawing.Size(216, 6);
					// 
					// exitToolStripMenuItem
					// 
					this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
					this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
					this.exitToolStripMenuItem.Text = "E&xit";
					this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
					// 
					// _setWPButton
					// 
					this._setWPButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this._setWPButton.Location = new System.Drawing.Point(1095, 665);
					this._setWPButton.Name = "_setWPButton";
					this._setWPButton.Size = new System.Drawing.Size(87, 27);
					this._setWPButton.TabIndex = 3;
					this._setWPButton.Text = "Set Wallpaper";
					this._setWPButton.UseVisualStyleBackColor = true;
					this._setWPButton.Click += new System.EventHandler(this._setWPButton_Click);
					// 
					// _picBox
					// 
					this._picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
											| System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this._picBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
					this._picBox.ImageLocation = "";
					this._picBox.Location = new System.Drawing.Point(14, 31);
					this._picBox.Name = "_picBox";
					this._picBox.Size = new System.Drawing.Size(1168, 378);
					this._picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
					this._picBox.TabIndex = 8;
					this._picBox.TabStop = false;
					// 
					// _notifyIcon
					// 
					this._notifyIcon.ContextMenuStrip = this._notifyIconCM;
					this._notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_notifyIcon.Icon")));
					this._notifyIcon.Text = "Wallpaper Changer";
					this._notifyIcon.Visible = true;
					this._notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._notifyIcon_MouseDoubleClick);
					// 
					// _notifyIconCM
					// 
					this._notifyIconCM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideApplicationToolStripMenuItem,
            this.changeWallpaperToolStripMenuItem,
            this.exitToolStripMenuItem1});
					this._notifyIconCM.Name = "_notifyIconCM";
					this._notifyIconCM.Size = new System.Drawing.Size(192, 70);
					// 
					// showHideApplicationToolStripMenuItem
					// 
					this.showHideApplicationToolStripMenuItem.Name = "showHideApplicationToolStripMenuItem";
					this.showHideApplicationToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
					this.showHideApplicationToolStripMenuItem.Text = "Show/Hide Application";
					this.showHideApplicationToolStripMenuItem.Click += new System.EventHandler(this.showHideApplicationToolStripMenuItem_Click);
					// 
					// changeWallpaperToolStripMenuItem
					// 
					this.changeWallpaperToolStripMenuItem.Name = "changeWallpaperToolStripMenuItem";
					this.changeWallpaperToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
					this.changeWallpaperToolStripMenuItem.Text = "Change Wallpaper";
					this.changeWallpaperToolStripMenuItem.Click += new System.EventHandler(this.changeWallpaperToolStripMenuItem_Click);
					// 
					// exitToolStripMenuItem1
					// 
					this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
					this.exitToolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
					this.exitToolStripMenuItem1.Text = "E&xit";
					this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
					// 
					// _changeAllButton
					// 
					this._changeAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this._changeAllButton.Location = new System.Drawing.Point(1001, 665);
					this._changeAllButton.Name = "_changeAllButton";
					this._changeAllButton.Size = new System.Drawing.Size(87, 27);
					this._changeAllButton.TabIndex = 13;
					this._changeAllButton.Text = "Change All";
					this._changeAllButton.UseVisualStyleBackColor = true;
					this._changeAllButton.Click += new System.EventHandler(this._changeAllButton_Click);
					// 
					// _changePaperTimer
					// 
					this._changePaperTimer.Enabled = true;
					this._changePaperTimer.Tick += new System.EventHandler(this._changePaperTimer_Tick);
					// 
					// timeSpanPicker1
					// 
					this.timeSpanPicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.timeSpanPicker1.Heading = "Frequency";
					this.timeSpanPicker1.Location = new System.Drawing.Point(1056, 522);
					this.timeSpanPicker1.MaximumSize = new System.Drawing.Size(120, 136);
					this.timeSpanPicker1.Name = "timeSpanPicker1";
					this.timeSpanPicker1.Size = new System.Drawing.Size(120, 136);
					this.timeSpanPicker1.TabIndex = 14;
					this.timeSpanPicker1.TimeSpan = System.TimeSpan.Parse("00:00:05");
					this.timeSpanPicker1.TimeSpanPickerValueChanged += new System.EventHandler<WallpaperChanger.TimeSpanPickerValueChangedEventArgs>(this.timeSpanPicker1_TimeSpanPickerValueChanged);
					// 
					// multiWpPicker
					// 
					this.multiWpPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this.multiWpPicker.Location = new System.Drawing.Point(14, 417);
					this.multiWpPicker.Name = "multiWpPicker";
					this.multiWpPicker.Size = new System.Drawing.Size(506, 284);
					this.multiWpPicker.TabIndex = 12;
					this.multiWpPicker.ConfigChanged += new System.EventHandler<WallpaperUtils.ConfigsChangedEventArgs>(this.multiWpPicker_ConfigChanged);
					// 
					// WallmasterUI
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(1190, 729);
					this.Controls.Add(this.timeSpanPicker1);
					this.Controls.Add(this._changeAllButton);
					this.Controls.Add(this._setWPButton);
					this.Controls.Add(this.multiWpPicker);
					this.Controls.Add(this._picBox);
					this.Controls.Add(this._statusStrip);
					this.Controls.Add(this._mainMenuStrip);
					this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
					this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
					this.MainMenuStrip = this._mainMenuStrip;
					this.Name = "WallmasterUI";
					this.Text = "Wallpaper Changer";
					this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
					this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
					this._mainMenuStrip.ResumeLayout(false);
					this._mainMenuStrip.PerformLayout();
					((System.ComponentModel.ISupportInitialize)(this._picBox)).EndInit();
					this._notifyIconCM.ResumeLayout(false);
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.MenuStrip _mainMenuStrip;
				private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
				private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
				private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
				private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
				private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
				private System.Windows.Forms.Button _setWPButton;
				private System.Windows.Forms.PictureBox _picBox;
				private System.Windows.Forms.NotifyIcon _notifyIcon;
				private System.Windows.Forms.ContextMenuStrip _notifyIconCM;
				private System.Windows.Forms.ToolStripMenuItem showHideApplicationToolStripMenuItem;
				private System.Windows.Forms.ToolStripMenuItem changeWallpaperToolStripMenuItem;
				private WallpaperUtils.MultiWallpaperPicker multiWpPicker;
				private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
				private System.Windows.Forms.Button _changeAllButton;
				private System.Windows.Forms.Timer _changePaperTimer;
				private TimeSpanPicker timeSpanPicker1;
    }
}

