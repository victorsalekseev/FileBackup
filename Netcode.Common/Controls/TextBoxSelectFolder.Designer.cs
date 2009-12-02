namespace Local_DCrypt.Controls
{
    partial class TextBoxSelectFolder
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
            this.button_choose = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.comboBox_path = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button_choose
            // 
            this.button_choose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_choose.Location = new System.Drawing.Point(199, 0);
            this.button_choose.Name = "button_choose";
            this.button_choose.Size = new System.Drawing.Size(26, 21);
            this.button_choose.TabIndex = 1;
            this.button_choose.Text = "...";
            this.button_choose.UseVisualStyleBackColor = true;
            // 
            // comboBox_path
            // 
            this.comboBox_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_path.FormattingEnabled = true;
            this.comboBox_path.Location = new System.Drawing.Point(0, 0);
            this.comboBox_path.Name = "comboBox_path";
            this.comboBox_path.Size = new System.Drawing.Size(197, 21);
            this.comboBox_path.TabIndex = 3;
            // 
            // TextBoxSelectFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_path);
            this.Controls.Add(this.button_choose);
            this.Name = "TextBoxSelectFolder";
            this.Size = new System.Drawing.Size(225, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_choose;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ComboBox comboBox_path;
    }
}
