﻿namespace WallpaperUtils {
	partial class WallpaperPicker {
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
			this._randomDirTB = new System.Windows.Forms.TextBox();
			this._changeImageButton = new System.Windows.Forms.Button();
			this._browseDirButton = new System.Windows.Forms.Button();
			this._imagePathTB = new System.Windows.Forms.TextBox();
			this._browseImageButton = new System.Windows.Forms.Button();
			this._colorButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this._noImageRB = new System.Windows.Forms.RadioButton();
			this._imageRB = new System.Windows.Forms.RadioButton();
			this._randomImageRB = new System.Windows.Forms.RadioButton();
			this._includeSubdirsCB = new System.Windows.Forms.CheckBox();
			this._stretchStyleCB = new System.Windows.Forms.ComboBox();
			this._styleLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _randomDirTB
			// 
			this._randomDirTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this._randomDirTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this._randomDirTB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this._randomDirTB.Enabled = false;
			this._randomDirTB.Location = new System.Drawing.Point(3, 98);
			this._randomDirTB.Name = "_randomDirTB";
			this._randomDirTB.Size = new System.Drawing.Size(335, 20);
			this._randomDirTB.TabIndex = 5;
			this._randomDirTB.TextChanged += new System.EventHandler(this._randomDirTB_TextChanged);
			// 
			// _changeImageButton
			// 
			this._changeImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._changeImageButton.Enabled = false;
			this._changeImageButton.Location = new System.Drawing.Point(344, 124);
			this._changeImageButton.Name = "_changeImageButton";
			this._changeImageButton.Size = new System.Drawing.Size(75, 23);
			this._changeImageButton.TabIndex = 7;
			this._changeImageButton.Text = "Change Image";
			this._changeImageButton.UseVisualStyleBackColor = true;
			this._changeImageButton.Click += new System.EventHandler(this._changeImageButton_Click);
			// 
			// _browseDirButton
			// 
			this._browseDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._browseDirButton.Enabled = false;
			this._browseDirButton.Location = new System.Drawing.Point(344, 96);
			this._browseDirButton.Name = "_browseDirButton";
			this._browseDirButton.Size = new System.Drawing.Size(75, 23);
			this._browseDirButton.TabIndex = 6;
			this._browseDirButton.Text = "Browse...";
			this._browseDirButton.UseVisualStyleBackColor = true;
			this._browseDirButton.Click += new System.EventHandler(this._browseDirButton_Click);
			// 
			// _imagePathTB
			// 
			this._imagePathTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this._imagePathTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this._imagePathTB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this._imagePathTB.Enabled = false;
			this._imagePathTB.Location = new System.Drawing.Point(3, 49);
			this._imagePathTB.Name = "_imagePathTB";
			this._imagePathTB.Size = new System.Drawing.Size(335, 20);
			this._imagePathTB.TabIndex = 2;
			this._imagePathTB.TextChanged += new System.EventHandler(this._imagePathTB_TextChanged);
			// 
			// _browseImageButton
			// 
			this._browseImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._browseImageButton.Enabled = false;
			this._browseImageButton.Location = new System.Drawing.Point(344, 47);
			this._browseImageButton.Name = "_browseImageButton";
			this._browseImageButton.Size = new System.Drawing.Size(75, 23);
			this._browseImageButton.TabIndex = 3;
			this._browseImageButton.Text = "Browse...";
			this._browseImageButton.UseVisualStyleBackColor = true;
			this._browseImageButton.Click += new System.EventHandler(this._browseImageButton_Click);
			// 
			// _colorButton
			// 
			this._colorButton.BackColor = System.Drawing.Color.Black;
			this._colorButton.Location = new System.Drawing.Point(98, 153);
			this._colorButton.Name = "_colorButton";
			this._colorButton.Size = new System.Drawing.Size(33, 23);
			this._colorButton.TabIndex = 9;
			this._colorButton.UseVisualStyleBackColor = false;
			this._colorButton.Click += new System.EventHandler(this._colorButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 158);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Background Color";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// _noImageRB
			// 
			this._noImageRB.AutoSize = true;
			this._noImageRB.Checked = true;
			this._noImageRB.Location = new System.Drawing.Point(3, 3);
			this._noImageRB.Name = "_noImageRB";
			this._noImageRB.Size = new System.Drawing.Size(71, 17);
			this._noImageRB.TabIndex = 0;
			this._noImageRB.TabStop = true;
			this._noImageRB.Text = "No Image";
			this._noImageRB.UseVisualStyleBackColor = true;
			this._noImageRB.CheckedChanged += new System.EventHandler(this._noImageRB_CheckedChanged);
			// 
			// _imageRB
			// 
			this._imageRB.AutoSize = true;
			this._imageRB.Location = new System.Drawing.Point(3, 26);
			this._imageRB.Name = "_imageRB";
			this._imageRB.Size = new System.Drawing.Size(54, 17);
			this._imageRB.TabIndex = 1;
			this._imageRB.Text = "Image";
			this._imageRB.UseVisualStyleBackColor = true;
			this._imageRB.CheckedChanged += new System.EventHandler(this._imageRB_CheckedChanged);
			// 
			// _randomImageRB
			// 
			this._randomImageRB.AutoSize = true;
			this._randomImageRB.Location = new System.Drawing.Point(3, 75);
			this._randomImageRB.Name = "_randomImageRB";
			this._randomImageRB.Size = new System.Drawing.Size(97, 17);
			this._randomImageRB.TabIndex = 4;
			this._randomImageRB.Text = "Random Image";
			this._randomImageRB.UseVisualStyleBackColor = true;
			this._randomImageRB.CheckedChanged += new System.EventHandler(this._randomImageRB_CheckedChanged);
			// 
			// _includeSubdirsCB
			// 
			this._includeSubdirsCB.AutoSize = true;
			this._includeSubdirsCB.Location = new System.Drawing.Point(3, 128);
			this._includeSubdirsCB.Name = "_includeSubdirsCB";
			this._includeSubdirsCB.Size = new System.Drawing.Size(131, 17);
			this._includeSubdirsCB.TabIndex = 10;
			this._includeSubdirsCB.Text = "Include Subdirectories";
			this._includeSubdirsCB.UseVisualStyleBackColor = true;
			this._includeSubdirsCB.CheckedChanged += new System.EventHandler(this._includeSubdirsCB_CheckedChanged);
			// 
			// _stretchStyleCB
			// 
			this._stretchStyleCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._stretchStyleCB.FormattingEnabled = true;
			this._stretchStyleCB.Items.AddRange(new object[] {
            "Center",
            "Stretch",
            "StretchRatio",
            "This shouldn\'t be here"});
			this._stretchStyleCB.Location = new System.Drawing.Point(298, 155);
			this._stretchStyleCB.Name = "_stretchStyleCB";
			this._stretchStyleCB.Size = new System.Drawing.Size(121, 21);
			this._stretchStyleCB.TabIndex = 11;
			this._stretchStyleCB.SelectedIndexChanged += new System.EventHandler(this._stretchStyleCB_SelectedIndexChanged);
			this._stretchStyleCB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._stretchStyleCB_KeyPress);
			// 
			// _styleLabel
			// 
			this._styleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._styleLabel.AutoSize = true;
			this._styleLabel.Location = new System.Drawing.Point(262, 158);
			this._styleLabel.Name = "_styleLabel";
			this._styleLabel.Size = new System.Drawing.Size(30, 13);
			this._styleLabel.TabIndex = 12;
			this._styleLabel.Text = "Style";
			// 
			// WallpaperPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._styleLabel);
			this.Controls.Add(this._stretchStyleCB);
			this.Controls.Add(this._includeSubdirsCB);
			this.Controls.Add(this._randomImageRB);
			this.Controls.Add(this._imageRB);
			this.Controls.Add(this._noImageRB);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._colorButton);
			this.Controls.Add(this._browseImageButton);
			this.Controls.Add(this._browseDirButton);
			this.Controls.Add(this._changeImageButton);
			this.Controls.Add(this._imagePathTB);
			this.Controls.Add(this._randomDirTB);
			this.Name = "WallpaperPicker";
			this.Size = new System.Drawing.Size(422, 188);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _randomDirTB;
		private System.Windows.Forms.Button _changeImageButton;
		private System.Windows.Forms.Button _browseDirButton;
		private System.Windows.Forms.TextBox _imagePathTB;
		private System.Windows.Forms.Button _browseImageButton;
		private System.Windows.Forms.Button _colorButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.RadioButton _noImageRB;
		private System.Windows.Forms.RadioButton _imageRB;
		private System.Windows.Forms.RadioButton _randomImageRB;
		private System.Windows.Forms.CheckBox _includeSubdirsCB;
		private System.Windows.Forms.ComboBox _stretchStyleCB;
		private System.Windows.Forms.Label _styleLabel;
	}
}
