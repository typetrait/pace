namespace Pace.Server.Forms
{
    partial class FileExplorerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileExplorerForm));
            this.explorerImageList = new System.Windows.Forms.ImageList(this.components);
            this.directoryListView = new System.Windows.Forms.ListView();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.backButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // explorerImageList
            // 
            this.explorerImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("explorerImageList.ImageStream")));
            this.explorerImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.explorerImageList.Images.SetKeyName(0, "folder.png");
            this.explorerImageList.Images.SetKeyName(1, "page.png");
            // 
            // directoryListView
            // 
            this.directoryListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryListView.Location = new System.Drawing.Point(12, 54);
            this.directoryListView.Name = "directoryListView";
            this.directoryListView.Size = new System.Drawing.Size(535, 270);
            this.directoryListView.SmallImageList = this.explorerImageList;
            this.directoryListView.TabIndex = 5;
            this.directoryListView.UseCompatibleStateImageBehavior = false;
            this.directoryListView.View = System.Windows.Forms.View.List;
            this.directoryListView.SelectedIndexChanged += new System.EventHandler(this.directoryListView_SelectedIndexChanged);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBox.Location = new System.Drawing.Point(99, 14);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(419, 20);
            this.pathTextBox.TabIndex = 3;
            this.pathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pathTextBox_KeyDown);
            // 
            // backButton
            // 
            this.backButton.Image = global::Pace.Server.Properties.Resources.arrow_left;
            this.backButton.Location = new System.Drawing.Point(12, 12);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(23, 23);
            this.backButton.TabIndex = 0;
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // forwardButton
            // 
            this.forwardButton.Image = global::Pace.Server.Properties.Resources.arrow_right;
            this.forwardButton.Location = new System.Drawing.Point(41, 12);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(23, 23);
            this.forwardButton.TabIndex = 1;
            this.forwardButton.UseVisualStyleBackColor = true;
            // 
            // upButton
            // 
            this.upButton.Image = global::Pace.Server.Properties.Resources.arrow_up;
            this.upButton.Location = new System.Drawing.Point(70, 12);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(23, 23);
            this.upButton.TabIndex = 2;
            this.upButton.UseVisualStyleBackColor = true;
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Image = global::Pace.Server.Properties.Resources.arrow_rotate_anticlockwise;
            this.refreshButton.Location = new System.Drawing.Point(524, 12);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 23);
            this.refreshButton.TabIndex = 4;
            this.refreshButton.UseVisualStyleBackColor = true;
            // 
            // FileExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 336);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.forwardButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.directoryListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(575, 375);
            this.Name = "FileExplorerForm";
            this.Text = "File Explorer";
            this.Load += new System.EventHandler(this.FileExplorerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView directoryListView;
        private System.Windows.Forms.ImageList explorerImageList;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button refreshButton;
    }
}