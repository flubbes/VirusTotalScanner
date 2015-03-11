namespace VirusTotalScanner
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvwAlertLog = new System.Windows.Forms.ListView();
            this.lvwScanLog = new System.Windows.Forms.ListView();
            this.notifierIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblAlerts = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpbxStats = new System.Windows.Forms.GroupBox();
            this.colScanType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScanFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScanTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScanResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScanHits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbxTotalAlerts = new System.Windows.Forms.TextBox();
            this.lblTotalAlerts = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxTotalScans = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.grpbxStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Resize += new System.EventHandler(this.menuStrip1_Resize);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // lvwAlertLog
            // 
            this.lvwAlertLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwAlertLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colType,
            this.colFile,
            this.colTime});
            this.lvwAlertLog.FullRowSelect = true;
            this.lvwAlertLog.GridLines = true;
            this.lvwAlertLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwAlertLog.Location = new System.Drawing.Point(12, 56);
            this.lvwAlertLog.Name = "lvwAlertLog";
            this.lvwAlertLog.Size = new System.Drawing.Size(325, 393);
            this.lvwAlertLog.TabIndex = 1;
            this.lvwAlertLog.UseCompatibleStateImageBehavior = false;
            this.lvwAlertLog.View = System.Windows.Forms.View.Details;
            // 
            // lvwScanLog
            // 
            this.lvwScanLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwScanLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colScanType,
            this.colScanFile,
            this.colScanTime,
            this.colScanHits,
            this.colScanResult});
            this.lvwScanLog.FullRowSelect = true;
            this.lvwScanLog.GridLines = true;
            this.lvwScanLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwScanLog.Location = new System.Drawing.Point(343, 56);
            this.lvwScanLog.Name = "lvwScanLog";
            this.lvwScanLog.Size = new System.Drawing.Size(455, 393);
            this.lvwScanLog.TabIndex = 2;
            this.lvwScanLog.UseCompatibleStateImageBehavior = false;
            this.lvwScanLog.View = System.Windows.Forms.View.Details;
            // 
            // notifierIcon
            // 
            this.notifierIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifierIcon.Icon")));
            this.notifierIcon.Text = "VirusTotal Filesystem Scanner for Desktop";
            this.notifierIcon.Visible = true;
            this.notifierIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifierIcon_MouseDoubleClick);
            // 
            // lblAlerts
            // 
            this.lblAlerts.AutoSize = true;
            this.lblAlerts.Location = new System.Drawing.Point(10, 40);
            this.lblAlerts.Name = "lblAlerts";
            this.lblAlerts.Size = new System.Drawing.Size(33, 13);
            this.lblAlerts.TabIndex = 3;
            this.lblAlerts.Text = "Alerts";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Scan Log";
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 50;
            // 
            // colFile
            // 
            this.colFile.Text = "File";
            this.colFile.Width = 150;
            // 
            // colTime
            // 
            this.colTime.Text = "Time";
            this.colTime.Width = 100;
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // grpbxStats
            // 
            this.grpbxStats.Controls.Add(this.label1);
            this.grpbxStats.Controls.Add(this.tbxTotalScans);
            this.grpbxStats.Controls.Add(this.lblTotalAlerts);
            this.grpbxStats.Controls.Add(this.tbxTotalAlerts);
            this.grpbxStats.Location = new System.Drawing.Point(804, 49);
            this.grpbxStats.Name = "grpbxStats";
            this.grpbxStats.Size = new System.Drawing.Size(168, 400);
            this.grpbxStats.TabIndex = 5;
            this.grpbxStats.TabStop = false;
            this.grpbxStats.Text = "Stats";
            // 
            // colScanType
            // 
            this.colScanType.Text = "Type";
            this.colScanType.Width = 50;
            // 
            // colScanFile
            // 
            this.colScanFile.Text = "File";
            this.colScanFile.Width = 150;
            // 
            // colScanTime
            // 
            this.colScanTime.Text = "Time";
            this.colScanTime.Width = 100;
            // 
            // colScanResult
            // 
            this.colScanResult.Text = "Result";
            this.colScanResult.Width = 80;
            // 
            // colScanHits
            // 
            this.colScanHits.Text = "Hits";
            this.colScanHits.Width = 50;
            // 
            // tbxTotalAlerts
            // 
            this.tbxTotalAlerts.Enabled = false;
            this.tbxTotalAlerts.Location = new System.Drawing.Point(72, 18);
            this.tbxTotalAlerts.Name = "tbxTotalAlerts";
            this.tbxTotalAlerts.ReadOnly = true;
            this.tbxTotalAlerts.Size = new System.Drawing.Size(90, 20);
            this.tbxTotalAlerts.TabIndex = 0;
            // 
            // lblTotalAlerts
            // 
            this.lblTotalAlerts.AutoSize = true;
            this.lblTotalAlerts.Location = new System.Drawing.Point(3, 21);
            this.lblTotalAlerts.Name = "lblTotalAlerts";
            this.lblTotalAlerts.Size = new System.Drawing.Size(63, 13);
            this.lblTotalAlerts.TabIndex = 1;
            this.lblTotalAlerts.Text = "Total Alerts:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Total Scans:";
            // 
            // tbxTotalScans
            // 
            this.tbxTotalScans.Enabled = false;
            this.tbxTotalScans.Location = new System.Drawing.Point(72, 40);
            this.tbxTotalScans.Name = "tbxTotalScans";
            this.tbxTotalScans.ReadOnly = true;
            this.tbxTotalScans.Size = new System.Drawing.Size(90, 20);
            this.tbxTotalScans.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.grpbxStats);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAlerts);
            this.Controls.Add(this.lvwScanLog);
            this.Controls.Add(this.lvwAlertLog);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "VirusTotal Filesystem Scanner for Desktop";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpbxStats.ResumeLayout(false);
            this.grpbxStats.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ListView lvwAlertLog;
        private System.Windows.Forms.ListView lvwScanLog;
        private System.Windows.Forms.NotifyIcon notifierIcon;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colFile;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.Label lblAlerts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpbxStats;
        private System.Windows.Forms.ColumnHeader colScanType;
        private System.Windows.Forms.ColumnHeader colScanFile;
        private System.Windows.Forms.ColumnHeader colScanTime;
        private System.Windows.Forms.ColumnHeader colScanHits;
        private System.Windows.Forms.ColumnHeader colScanResult;
        private System.Windows.Forms.Label lblTotalAlerts;
        private System.Windows.Forms.TextBox tbxTotalAlerts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxTotalScans;
    }
}

