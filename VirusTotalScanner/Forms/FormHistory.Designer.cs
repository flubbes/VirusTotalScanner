namespace VirusTotalScanner.Forms
{
    partial class FormHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHistory));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvwVirusHistory = new System.Windows.Forms.ListView();
            this.colFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsVirusHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.cmsVirusHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeWindowToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeWindowToolStripMenuItem
            // 
            this.closeWindowToolStripMenuItem.Name = "closeWindowToolStripMenuItem";
            this.closeWindowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeWindowToolStripMenuItem.Text = "Close Window";
            this.closeWindowToolStripMenuItem.Click += new System.EventHandler(this.closeWindowToolStripMenuItem_Click);
            // 
            // lvwVirusHistory
            // 
            this.lvwVirusHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwVirusHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFile,
            this.colTime,
            this.colHits,
            this.colResult,
            this.colPath});
            this.lvwVirusHistory.ContextMenuStrip = this.cmsVirusHistory;
            this.lvwVirusHistory.FullRowSelect = true;
            this.lvwVirusHistory.GridLines = true;
            this.lvwVirusHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwVirusHistory.Location = new System.Drawing.Point(0, 27);
            this.lvwVirusHistory.Name = "lvwVirusHistory";
            this.lvwVirusHistory.Size = new System.Drawing.Size(584, 283);
            this.lvwVirusHistory.TabIndex = 3;
            this.lvwVirusHistory.UseCompatibleStateImageBehavior = false;
            this.lvwVirusHistory.View = System.Windows.Forms.View.Details;
            // 
            // colFile
            // 
            this.colFile.Text = "File";
            this.colFile.Width = 121;
            // 
            // colTime
            // 
            this.colTime.Text = "Time";
            this.colTime.Width = 77;
            // 
            // colHits
            // 
            this.colHits.Text = "Hits";
            this.colHits.Width = 50;
            // 
            // colResult
            // 
            this.colResult.Text = "Result";
            this.colResult.Width = 74;
            // 
            // colPath
            // 
            this.colPath.Text = "Path";
            this.colPath.Width = 237;
            // 
            // cmsVirusHistory
            // 
            this.cmsVirusHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInExplorerToolStripMenuItem,
            this.removeFromHistoryToolStripMenuItem});
            this.cmsVirusHistory.Name = "cmsVirusHistory";
            this.cmsVirusHistory.Size = new System.Drawing.Size(188, 48);
            // 
            // openInExplorerToolStripMenuItem
            // 
            this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
            this.openInExplorerToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openInExplorerToolStripMenuItem.Text = "Open in Explorer";
            // 
            // removeFromHistoryToolStripMenuItem
            // 
            this.removeFromHistoryToolStripMenuItem.Name = "removeFromHistoryToolStripMenuItem";
            this.removeFromHistoryToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.removeFromHistoryToolStripMenuItem.Text = "Remove from History";
            // 
            // FormHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 311);
            this.Controls.Add(this.lvwVirusHistory);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 350);
            this.MinimizeBox = false;
            this.Name = "FormHistory";
            this.Text = "Virus History";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cmsVirusHistory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeWindowToolStripMenuItem;
        private System.Windows.Forms.ListView lvwVirusHistory;
        private System.Windows.Forms.ColumnHeader colFile;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colHits;
        private System.Windows.Forms.ColumnHeader colResult;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ContextMenuStrip cmsVirusHistory;
        private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromHistoryToolStripMenuItem;
    }
}