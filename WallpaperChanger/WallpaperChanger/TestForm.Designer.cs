namespace WallpaperChanger
{
    partial class TestForm
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
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
					this._statusStrip = new System.Windows.Forms.StatusStrip();
					this._mainMenuStrip = new System.Windows.Forms.MenuStrip();
					this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
					this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
					this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
					this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
					this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
					this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
					this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this._wpPath1 = new System.Windows.Forms.TextBox();
					this._setWPButton = new System.Windows.Forms.Button();
					this._browseButton = new System.Windows.Forms.Button();
					this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
					this._browseButton2 = new System.Windows.Forms.Button();
					this._wpPath2 = new System.Windows.Forms.TextBox();
					this._togglePathsButton = new System.Windows.Forms.Button();
					this._picBox = new System.Windows.Forms.PictureBox();
					this._genImageButton = new System.Windows.Forms.Button();
					this._colorButton2 = new System.Windows.Forms.Button();
					this._colorDialog = new System.Windows.Forms.ColorDialog();
					this._colorButton1 = new System.Windows.Forms.Button();
					this._xNud1 = new System.Windows.Forms.NumericUpDown();
					this._yNud1 = new System.Windows.Forms.NumericUpDown();
					this._xNud2 = new System.Windows.Forms.NumericUpDown();
					this._yNud2 = new System.Windows.Forms.NumericUpDown();
					this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
					this._notifyIconCM = new System.Windows.Forms.ContextMenuStrip(this.components);
					this.showHideApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.changeWallpaperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.multiWallpaperPicker1 = new WallpaperUtils.MultiWallpaperPicker();
					this.timespanPicker1 = new WallpaperChanger.TimeSpanPicker();
					this._mainMenuStrip.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this._picBox)).BeginInit();
					((System.ComponentModel.ISupportInitialize)(this._xNud1)).BeginInit();
					((System.ComponentModel.ISupportInitialize)(this._yNud1)).BeginInit();
					((System.ComponentModel.ISupportInitialize)(this._xNud2)).BeginInit();
					((System.ComponentModel.ISupportInitialize)(this._yNud2)).BeginInit();
					this._notifyIconCM.SuspendLayout();
					this.SuspendLayout();
					// 
					// _statusStrip
					// 
					this._statusStrip.Location = new System.Drawing.Point(0, 610);
					this._statusStrip.Name = "_statusStrip";
					this._statusStrip.Size = new System.Drawing.Size(1020, 22);
					this._statusStrip.TabIndex = 0;
					this._statusStrip.Text = "statusStrip1";
					// 
					// _mainMenuStrip
					// 
					this._mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
					this._mainMenuStrip.Location = new System.Drawing.Point(0, 0);
					this._mainMenuStrip.Name = "_mainMenuStrip";
					this._mainMenuStrip.Size = new System.Drawing.Size(1020, 24);
					this._mainMenuStrip.TabIndex = 1;
					this._mainMenuStrip.Text = "menuStrip1";
					// 
					// fileToolStripMenuItem
					// 
					this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
					this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
					this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
					this.fileToolStripMenuItem.Text = "&File";
					// 
					// newToolStripMenuItem
					// 
					this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
					this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.newToolStripMenuItem.Name = "newToolStripMenuItem";
					this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
					this.newToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
					this.newToolStripMenuItem.Text = "&New";
					// 
					// openToolStripMenuItem
					// 
					this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
					this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.openToolStripMenuItem.Name = "openToolStripMenuItem";
					this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
					this.openToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
					this.openToolStripMenuItem.Text = "&Open";
					this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
					// 
					// toolStripSeparator
					// 
					this.toolStripSeparator.Name = "toolStripSeparator";
					this.toolStripSeparator.Size = new System.Drawing.Size(148, 6);
					// 
					// saveToolStripMenuItem
					// 
					this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
					this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
					this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
					this.saveToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
					this.saveToolStripMenuItem.Text = "&Save";
					this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
					// 
					// toolStripSeparator1
					// 
					this.toolStripSeparator1.Name = "toolStripSeparator1";
					this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
					// 
					// printToolStripMenuItem
					// 
					this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
					this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.printToolStripMenuItem.Name = "printToolStripMenuItem";
					this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
					this.printToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
					this.printToolStripMenuItem.Text = "&Print";
					// 
					// printPreviewToolStripMenuItem
					// 
					this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
					this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
					this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
					this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
					// 
					// toolStripSeparator2
					// 
					this.toolStripSeparator2.Name = "toolStripSeparator2";
					this.toolStripSeparator2.Size = new System.Drawing.Size(148, 6);
					// 
					// exitToolStripMenuItem
					// 
					this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
					this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
					this.exitToolStripMenuItem.Text = "E&xit";
					this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
					// 
					// editToolStripMenuItem
					// 
					this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
					this.editToolStripMenuItem.Name = "editToolStripMenuItem";
					this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
					this.editToolStripMenuItem.Text = "&Edit";
					// 
					// undoToolStripMenuItem
					// 
					this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
					this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
					this.undoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
					this.undoToolStripMenuItem.Text = "&Undo";
					// 
					// redoToolStripMenuItem
					// 
					this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
					this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
					this.redoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
					this.redoToolStripMenuItem.Text = "&Redo";
					// 
					// toolStripSeparator3
					// 
					this.toolStripSeparator3.Name = "toolStripSeparator3";
					this.toolStripSeparator3.Size = new System.Drawing.Size(147, 6);
					// 
					// cutToolStripMenuItem
					// 
					this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
					this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
					this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
					this.cutToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
					this.cutToolStripMenuItem.Text = "Cu&t";
					// 
					// copyToolStripMenuItem
					// 
					this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
					this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
					this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
					this.copyToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
					this.copyToolStripMenuItem.Text = "&Copy";
					// 
					// pasteToolStripMenuItem
					// 
					this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
					this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
					this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
					this.pasteToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
					this.pasteToolStripMenuItem.Text = "&Paste";
					// 
					// toolStripSeparator4
					// 
					this.toolStripSeparator4.Name = "toolStripSeparator4";
					this.toolStripSeparator4.Size = new System.Drawing.Size(147, 6);
					// 
					// selectAllToolStripMenuItem
					// 
					this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
					this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
					this.selectAllToolStripMenuItem.Text = "Select &All";
					// 
					// toolsToolStripMenuItem
					// 
					this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
					this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
					this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
					this.toolsToolStripMenuItem.Text = "&Tools";
					// 
					// customizeToolStripMenuItem
					// 
					this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
					this.customizeToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
					this.customizeToolStripMenuItem.Text = "&Customize";
					// 
					// optionsToolStripMenuItem
					// 
					this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
					this.optionsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
					this.optionsToolStripMenuItem.Text = "&Options";
					// 
					// helpToolStripMenuItem
					// 
					this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
					this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
					this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
					this.helpToolStripMenuItem.Text = "&Help";
					// 
					// contentsToolStripMenuItem
					// 
					this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
					this.contentsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
					this.contentsToolStripMenuItem.Text = "&Contents";
					// 
					// indexToolStripMenuItem
					// 
					this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
					this.indexToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
					this.indexToolStripMenuItem.Text = "&Index";
					// 
					// searchToolStripMenuItem
					// 
					this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
					this.searchToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
					this.searchToolStripMenuItem.Text = "&Search";
					// 
					// toolStripSeparator5
					// 
					this.toolStripSeparator5.Name = "toolStripSeparator5";
					this.toolStripSeparator5.Size = new System.Drawing.Size(126, 6);
					// 
					// aboutToolStripMenuItem
					// 
					this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
					this.aboutToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
					this.aboutToolStripMenuItem.Text = "&About...";
					// 
					// _wpPath1
					// 
					this._wpPath1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this._wpPath1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
					this._wpPath1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
					this._wpPath1.Location = new System.Drawing.Point(12, 54);
					this._wpPath1.Name = "_wpPath1";
					this._wpPath1.Size = new System.Drawing.Size(890, 20);
					this._wpPath1.TabIndex = 2;
					this._wpPath1.Text = "C:\\Documents and Settings\\jquintus\\My Documents\\My Pictures\\The Wall\\anti_mind_vi" +
							"rus.png";
					// 
					// _setWPButton
					// 
					this._setWPButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this._setWPButton.Location = new System.Drawing.Point(939, 576);
					this._setWPButton.Name = "_setWPButton";
					this._setWPButton.Size = new System.Drawing.Size(75, 23);
					this._setWPButton.TabIndex = 3;
					this._setWPButton.Text = "Set Wallpaper";
					this._setWPButton.UseVisualStyleBackColor = true;
					this._setWPButton.Click += new System.EventHandler(this._setWPButton_Click);
					// 
					// _browseButton
					// 
					this._browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
					this._browseButton.Location = new System.Drawing.Point(908, 51);
					this._browseButton.Name = "_browseButton";
					this._browseButton.Size = new System.Drawing.Size(25, 23);
					this._browseButton.TabIndex = 4;
					this._browseButton.Text = "...";
					this._browseButton.UseVisualStyleBackColor = true;
					this._browseButton.Click += new System.EventHandler(this._browseButton_Click);
					// 
					// _browseButton2
					// 
					this._browseButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
					this._browseButton2.Location = new System.Drawing.Point(908, 83);
					this._browseButton2.Name = "_browseButton2";
					this._browseButton2.Size = new System.Drawing.Size(25, 23);
					this._browseButton2.TabIndex = 6;
					this._browseButton2.Text = "...";
					this._browseButton2.UseVisualStyleBackColor = true;
					this._browseButton2.Click += new System.EventHandler(this._browseButton2_Click);
					// 
					// _wpPath2
					// 
					this._wpPath2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this._wpPath2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
					this._wpPath2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
					this._wpPath2.Location = new System.Drawing.Point(12, 83);
					this._wpPath2.Name = "_wpPath2";
					this._wpPath2.Size = new System.Drawing.Size(890, 20);
					this._wpPath2.TabIndex = 5;
					this._wpPath2.Text = "C:\\Documents and Settings\\jquintus\\My Documents\\My Pictures\\The Wall\\buddhists (2" +
							").gif";
					// 
					// _togglePathsButton
					// 
					this._togglePathsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
					this._togglePathsButton.Location = new System.Drawing.Point(939, 51);
					this._togglePathsButton.Name = "_togglePathsButton";
					this._togglePathsButton.Size = new System.Drawing.Size(75, 52);
					this._togglePathsButton.TabIndex = 7;
					this._togglePathsButton.Text = "Toggle";
					this._togglePathsButton.UseVisualStyleBackColor = true;
					this._togglePathsButton.Click += new System.EventHandler(this._togglePathsButton_Click);
					// 
					// _picBox
					// 
					this._picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
											| System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this._picBox.Location = new System.Drawing.Point(12, 109);
					this._picBox.Name = "_picBox";
					this._picBox.Size = new System.Drawing.Size(1002, 461);
					this._picBox.TabIndex = 8;
					this._picBox.TabStop = false;
					// 
					// _genImageButton
					// 
					this._genImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this._genImageButton.Location = new System.Drawing.Point(858, 576);
					this._genImageButton.Name = "_genImageButton";
					this._genImageButton.Size = new System.Drawing.Size(75, 23);
					this._genImageButton.TabIndex = 9;
					this._genImageButton.Text = "Preview";
					this._genImageButton.UseVisualStyleBackColor = true;
					this._genImageButton.Click += new System.EventHandler(this._genImageButton_Click);
					// 
					// _colorButton2
					// 
					this._colorButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this._colorButton2.BackColor = System.Drawing.SystemColors.Desktop;
					this._colorButton2.Location = new System.Drawing.Point(821, 576);
					this._colorButton2.Name = "_colorButton2";
					this._colorButton2.Size = new System.Drawing.Size(31, 23);
					this._colorButton2.TabIndex = 10;
					this._colorButton2.UseVisualStyleBackColor = false;
					this._colorButton2.Click += new System.EventHandler(this._colorButton2_Click);
					// 
					// _colorButton1
					// 
					this._colorButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this._colorButton1.BackColor = System.Drawing.SystemColors.Desktop;
					this._colorButton1.Location = new System.Drawing.Point(784, 576);
					this._colorButton1.Name = "_colorButton1";
					this._colorButton1.Size = new System.Drawing.Size(31, 23);
					this._colorButton1.TabIndex = 10;
					this._colorButton1.UseVisualStyleBackColor = false;
					this._colorButton1.Click += new System.EventHandler(this._colorButton1_Click);
					// 
					// _xNud1
					// 
					this._xNud1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this._xNud1.Location = new System.Drawing.Point(12, 576);
					this._xNud1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
					this._xNud1.Name = "_xNud1";
					this._xNud1.Size = new System.Drawing.Size(48, 20);
					this._xNud1.TabIndex = 11;
					this._xNud1.Value = new decimal(new int[] {
            650,
            0,
            0,
            0});
					// 
					// _yNud1
					// 
					this._yNud1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this._yNud1.Location = new System.Drawing.Point(66, 576);
					this._yNud1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
					this._yNud1.Name = "_yNud1";
					this._yNud1.Size = new System.Drawing.Size(48, 20);
					this._yNud1.TabIndex = 11;
					this._yNud1.Value = new decimal(new int[] {
            650,
            0,
            0,
            0});
					// 
					// _xNud2
					// 
					this._xNud2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this._xNud2.Location = new System.Drawing.Point(120, 576);
					this._xNud2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
					this._xNud2.Name = "_xNud2";
					this._xNud2.Size = new System.Drawing.Size(48, 20);
					this._xNud2.TabIndex = 11;
					this._xNud2.Value = new decimal(new int[] {
            650,
            0,
            0,
            0});
					// 
					// _yNud2
					// 
					this._yNud2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this._yNud2.Location = new System.Drawing.Point(174, 576);
					this._yNud2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
					this._yNud2.Name = "_yNud2";
					this._yNud2.Size = new System.Drawing.Size(48, 20);
					this._yNud2.TabIndex = 11;
					this._yNud2.Value = new decimal(new int[] {
            650,
            0,
            0,
            0});
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
            this.changeWallpaperToolStripMenuItem});
					this._notifyIconCM.Name = "_notifyIconCM";
					this._notifyIconCM.Size = new System.Drawing.Size(192, 48);
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
					// multiWallpaperPicker1
					// 
					this.multiWallpaperPicker1.Location = new System.Drawing.Point(12, 109);
					this.multiWallpaperPicker1.Name = "multiWallpaperPicker1";
					this.multiWallpaperPicker1.Size = new System.Drawing.Size(470, 246);
					this.multiWallpaperPicker1.TabIndex = 12;
					// 
					// timespanPicker1
					// 
					this.timespanPicker1.Location = new System.Drawing.Point(547, 215);
					this.timespanPicker1.MaximumSize = new System.Drawing.Size(103, 118);
					this.timespanPicker1.Name = "timespanPicker1";
					this.timespanPicker1.Size = new System.Drawing.Size(103, 118);
					this.timespanPicker1.TabIndex = 13;
					this.timespanPicker1.TimeSpan = System.TimeSpan.Parse("00:00:00");
					// 
					// TestForm
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(1020, 632);
					this.Controls.Add(this.timespanPicker1);
					this.Controls.Add(this._yNud2);
					this.Controls.Add(this.multiWallpaperPicker1);
					this.Controls.Add(this._xNud2);
					this.Controls.Add(this._yNud1);
					this.Controls.Add(this._xNud1);
					this.Controls.Add(this._colorButton1);
					this.Controls.Add(this._colorButton2);
					this.Controls.Add(this._genImageButton);
					this.Controls.Add(this._picBox);
					this.Controls.Add(this._togglePathsButton);
					this.Controls.Add(this._browseButton2);
					this.Controls.Add(this._wpPath2);
					this.Controls.Add(this._browseButton);
					this.Controls.Add(this._setWPButton);
					this.Controls.Add(this._wpPath1);
					this.Controls.Add(this._statusStrip);
					this.Controls.Add(this._mainMenuStrip);
					this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
					this.MainMenuStrip = this._mainMenuStrip;
					this.Name = "TestForm";
					this.Text = "Wallpaper Changer";
					this.Load += new System.EventHandler(this.TestForm_Load);
					this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
					this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
					this._mainMenuStrip.ResumeLayout(false);
					this._mainMenuStrip.PerformLayout();
					((System.ComponentModel.ISupportInitialize)(this._picBox)).EndInit();
					((System.ComponentModel.ISupportInitialize)(this._xNud1)).EndInit();
					((System.ComponentModel.ISupportInitialize)(this._yNud1)).EndInit();
					((System.ComponentModel.ISupportInitialize)(this._xNud2)).EndInit();
					((System.ComponentModel.ISupportInitialize)(this._yNud2)).EndInit();
					this._notifyIconCM.ResumeLayout(false);
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.MenuStrip _mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
				private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox _wpPath1;
        private System.Windows.Forms.Button _setWPButton;
        private System.Windows.Forms.Button _browseButton;
        private System.Windows.Forms.OpenFileDialog _openFileDialog;
        private System.Windows.Forms.Button _browseButton2;
        private System.Windows.Forms.TextBox _wpPath2;
        private System.Windows.Forms.Button _togglePathsButton;
        private System.Windows.Forms.PictureBox _picBox;
        private System.Windows.Forms.Button _genImageButton;
        private System.Windows.Forms.Button _colorButton2;
        private System.Windows.Forms.ColorDialog _colorDialog;
        private System.Windows.Forms.Button _colorButton1;
        private System.Windows.Forms.NumericUpDown _xNud1;
        private System.Windows.Forms.NumericUpDown _yNud1;
        private System.Windows.Forms.NumericUpDown _xNud2;
        private System.Windows.Forms.NumericUpDown _yNud2;
				private System.Windows.Forms.NotifyIcon _notifyIcon;
				private System.Windows.Forms.ContextMenuStrip _notifyIconCM;
				private System.Windows.Forms.ToolStripMenuItem showHideApplicationToolStripMenuItem;
				private System.Windows.Forms.ToolStripMenuItem changeWallpaperToolStripMenuItem;
				private WallpaperUtils.MultiWallpaperPicker multiWallpaperPicker1;
				private TimeSpanPicker timespanPicker1;
    }
}

