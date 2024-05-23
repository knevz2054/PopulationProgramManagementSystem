namespace DataProcessingSystem
{
    partial class frmViewLog
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
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClearLogs = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.chkView = new System.Windows.Forms.CheckBox();
            this.dtpViewLogs = new System.Windows.Forms.DateTimePicker();
            this.lvLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlBottom.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClearLogs);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 546);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1050, 42);
            this.pnlBottom.TabIndex = 0;
            // 
            // btnClearLogs
            // 
            this.btnClearLogs.BackColor = System.Drawing.Color.CadetBlue;
            this.btnClearLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLogs.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearLogs.ForeColor = System.Drawing.Color.White;
            this.btnClearLogs.Location = new System.Drawing.Point(0, 0);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Size = new System.Drawing.Size(1050, 42);
            this.btnClearLogs.TabIndex = 0;
            this.btnClearLogs.Text = "Clear Logs";
            this.btnClearLogs.UseVisualStyleBackColor = false;
            this.btnClearLogs.Click += new System.EventHandler(this.btnClearLogs_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.CadetBlue;
            this.pnlTop.Controls.Add(this.chkView);
            this.pnlTop.Controls.Add(this.dtpViewLogs);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1050, 42);
            this.pnlTop.TabIndex = 1;
            // 
            // chkView
            // 
            this.chkView.AutoSize = true;
            this.chkView.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkView.ForeColor = System.Drawing.Color.White;
            this.chkView.Location = new System.Drawing.Point(124, 13);
            this.chkView.Name = "chkView";
            this.chkView.Size = new System.Drawing.Size(108, 19);
            this.chkView.TabIndex = 1;
            this.chkView.Text = "View by date";
            this.chkView.UseVisualStyleBackColor = true;
            this.chkView.CheckedChanged += new System.EventHandler(this.chkView_CheckedChanged);
            // 
            // dtpViewLogs
            // 
            this.dtpViewLogs.Enabled = false;
            this.dtpViewLogs.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpViewLogs.Location = new System.Drawing.Point(12, 12);
            this.dtpViewLogs.Name = "dtpViewLogs";
            this.dtpViewLogs.Size = new System.Drawing.Size(106, 20);
            this.dtpViewLogs.TabIndex = 0;
            this.dtpViewLogs.ValueChanged += new System.EventHandler(this.dtpViewLogs_ValueChanged);
            // 
            // lvLog
            // 
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLog.HideSelection = false;
            this.lvLog.Location = new System.Drawing.Point(0, 42);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(1050, 504);
            this.lvLog.TabIndex = 2;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Activity Log";
            this.columnHeader2.Width = 1000;
            // 
            // frmViewLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1050, 588);
            this.Controls.Add(this.lvLog);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmViewLog";
            this.Text = "frmViewLog";
            this.Load += new System.EventHandler(this.frmViewLog_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnClearLogs;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.DateTimePicker dtpViewLogs;
        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox chkView;
    }
}