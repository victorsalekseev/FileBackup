namespace Netcode.Scan
{
    partial class FormDetails
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
            this.tabControlDt = new System.Windows.Forms.TabControl();
            this.tabPageFS = new System.Windows.Forms.TabPage();
            this.propertyGridFS = new System.Windows.Forms.PropertyGrid();
            this.tabPageExe = new System.Windows.Forms.TabPage();
            this.propertyGridExe = new System.Windows.Forms.PropertyGrid();
            this.tabControlDt.SuspendLayout();
            this.tabPageFS.SuspendLayout();
            this.tabPageExe.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlDt
            // 
            this.tabControlDt.Controls.Add(this.tabPageFS);
            this.tabControlDt.Controls.Add(this.tabPageExe);
            this.tabControlDt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDt.Location = new System.Drawing.Point(0, 0);
            this.tabControlDt.Name = "tabControlDt";
            this.tabControlDt.SelectedIndex = 0;
            this.tabControlDt.Size = new System.Drawing.Size(444, 495);
            this.tabControlDt.TabIndex = 0;
            // 
            // tabPageFS
            // 
            this.tabPageFS.Controls.Add(this.propertyGridFS);
            this.tabPageFS.Location = new System.Drawing.Point(4, 22);
            this.tabPageFS.Name = "tabPageFS";
            this.tabPageFS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFS.Size = new System.Drawing.Size(436, 469);
            this.tabPageFS.TabIndex = 0;
            this.tabPageFS.Text = "ФС";
            this.tabPageFS.UseVisualStyleBackColor = true;
            // 
            // propertyGridFS
            // 
            this.propertyGridFS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridFS.Location = new System.Drawing.Point(3, 3);
            this.propertyGridFS.Name = "propertyGridFS";
            this.propertyGridFS.Size = new System.Drawing.Size(430, 463);
            this.propertyGridFS.TabIndex = 0;
            // 
            // tabPageExe
            // 
            this.tabPageExe.Controls.Add(this.propertyGridExe);
            this.tabPageExe.Location = new System.Drawing.Point(4, 22);
            this.tabPageExe.Name = "tabPageExe";
            this.tabPageExe.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExe.Size = new System.Drawing.Size(436, 469);
            this.tabPageExe.TabIndex = 2;
            this.tabPageExe.Text = "Версия";
            this.tabPageExe.UseVisualStyleBackColor = true;
            // 
            // propertyGridExe
            // 
            this.propertyGridExe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridExe.Location = new System.Drawing.Point(3, 3);
            this.propertyGridExe.Name = "propertyGridExe";
            this.propertyGridExe.Size = new System.Drawing.Size(430, 463);
            this.propertyGridExe.TabIndex = 0;
            // 
            // FormDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 495);
            this.Controls.Add(this.tabControlDt);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDetails";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Свойства файла";
            this.tabControlDt.ResumeLayout(false);
            this.tabPageFS.ResumeLayout(false);
            this.tabPageExe.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlDt;
        private System.Windows.Forms.TabPage tabPageFS;
        private System.Windows.Forms.PropertyGrid propertyGridFS;
        private System.Windows.Forms.TabPage tabPageExe;
        private System.Windows.Forms.PropertyGrid propertyGridExe;
    }
}