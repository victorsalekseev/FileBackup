namespace Netcode.Common.Controls
{
    partial class FileTreeExplorer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTreeExplorer));
            this.tV = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tV
            // 
            this.tV.CheckBoxes = true;
            this.tV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tV.ImageIndex = 0;
            this.tV.ImageList = this.imageList;
            this.tV.Location = new System.Drawing.Point(0, 0);
            this.tV.Name = "tV";
            this.tV.SelectedImageIndex = 0;
            this.tV.ShowNodeToolTips = true;
            this.tV.Size = new System.Drawing.Size(150, 150);
            this.tV.TabIndex = 1;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "comment.png");
            this.imageList.Images.SetKeyName(1, "comment_w.png");
            // 
            // FileTreeExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tV);
            this.Name = "FileTreeExplorer";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView tV;
        private System.Windows.Forms.ImageList imageList;
    }
}
