namespace WallpaperChanger {
	partial class TimeSpanPicker {
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
			this._secs = new System.Windows.Forms.NumericUpDown();
			this._mins = new System.Windows.Forms.NumericUpDown();
			this._hours = new System.Windows.Forms.NumericUpDown();
			this._days = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this._secs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._mins)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._hours)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._days)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _secs
			// 
			this._secs.Location = new System.Drawing.Point(64, 92);
			this._secs.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this._secs.Name = "_secs";
			this._secs.Size = new System.Drawing.Size(32, 20);
			this._secs.TabIndex = 15;
			this._secs.Value = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this._secs.ValueChanged += new System.EventHandler(this._timespan_ValueChanged);
			// 
			// _mins
			// 
			this._mins.Location = new System.Drawing.Point(64, 66);
			this._mins.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this._mins.Name = "_mins";
			this._mins.Size = new System.Drawing.Size(32, 20);
			this._mins.TabIndex = 16;
			this._mins.Value = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this._mins.ValueChanged += new System.EventHandler(this._timespan_ValueChanged);
			// 
			// _hours
			// 
			this._hours.Location = new System.Drawing.Point(64, 40);
			this._hours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
			this._hours.Name = "_hours";
			this._hours.Size = new System.Drawing.Size(32, 20);
			this._hours.TabIndex = 17;
			this._hours.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
			this._hours.ValueChanged += new System.EventHandler(this._timespan_ValueChanged);
			// 
			// _days
			// 
			this._days.Location = new System.Drawing.Point(64, 14);
			this._days.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
			this._days.Name = "_days";
			this._days.Size = new System.Drawing.Size(32, 20);
			this._days.TabIndex = 17;
			this._days.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
			this._days.ValueChanged += new System.EventHandler(this._timespan_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 19;
			this.label1.Text = "Days: ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 19;
			this.label2.Text = "Hours:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 19;
			this.label3.Text = "Minutes";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 94);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(52, 13);
			this.label4.TabIndex = 19;
			this.label4.Text = "Seconds:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this._mins);
			this.groupBox1.Controls.Add(this._days);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this._secs);
			this.groupBox1.Controls.Add(this._hours);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.MaximumSize = new System.Drawing.Size(103, 118);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(103, 118);
			this.groupBox1.TabIndex = 20;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "TimespanPicker";
			// 
			// TimespanPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.MaximumSize = new System.Drawing.Size(103, 118);
			this.Name = "TimespanPicker";
			this.Size = new System.Drawing.Size(103, 118);
			((System.ComponentModel.ISupportInitialize)(this._secs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._mins)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._hours)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._days)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NumericUpDown _secs;
		private System.Windows.Forms.NumericUpDown _mins;
		private System.Windows.Forms.NumericUpDown _hours;
		private System.Windows.Forms.NumericUpDown _days;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
