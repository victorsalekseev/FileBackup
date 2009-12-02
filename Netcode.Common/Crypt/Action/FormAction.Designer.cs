namespace Necode.Common.Action
{
    partial class FormAction
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar_crypt = new System.Windows.Forms.ProgressBar();
            this.button_ok = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listBox_log = new System.Windows.Forms.ListBox();
            this.textBox_dst_file = new System.Windows.Forms.TextBox();
            this.label_dst_file = new System.Windows.Forms.Label();
            this.textBox_src_file = new System.Windows.Forms.TextBox();
            this.label_src_file = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar_crypt);
            this.panel1.Controls.Add(this.button_ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 139);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 28);
            this.panel1.TabIndex = 2;
            // 
            // progressBar_crypt
            // 
            this.progressBar_crypt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_crypt.Location = new System.Drawing.Point(3, 2);
            this.progressBar_crypt.Name = "progressBar_crypt";
            this.progressBar_crypt.Size = new System.Drawing.Size(508, 23);
            this.progressBar_crypt.TabIndex = 8;
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Enabled = false;
            this.button_ok.Location = new System.Drawing.Point(517, 2);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(36, 23);
            this.button_ok.TabIndex = 0;
            this.button_ok.Text = "ОК";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listBox_log);
            this.panel2.Controls.Add(this.textBox_dst_file);
            this.panel2.Controls.Add(this.label_dst_file);
            this.panel2.Controls.Add(this.textBox_src_file);
            this.panel2.Controls.Add(this.label_src_file);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(556, 139);
            this.panel2.TabIndex = 3;
            // 
            // listBox_log
            // 
            this.listBox_log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_log.FormattingEnabled = true;
            this.listBox_log.Location = new System.Drawing.Point(3, 55);
            this.listBox_log.Name = "listBox_log";
            this.listBox_log.Size = new System.Drawing.Size(550, 82);
            this.listBox_log.TabIndex = 6;
            // 
            // textBox_dst_file
            // 
            this.textBox_dst_file.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_dst_file.Enabled = false;
            this.textBox_dst_file.Location = new System.Drawing.Point(64, 29);
            this.textBox_dst_file.Name = "textBox_dst_file";
            this.textBox_dst_file.Size = new System.Drawing.Size(489, 20);
            this.textBox_dst_file.TabIndex = 3;
            // 
            // label_dst_file
            // 
            this.label_dst_file.AutoSize = true;
            this.label_dst_file.Location = new System.Drawing.Point(3, 32);
            this.label_dst_file.Name = "label_dst_file";
            this.label_dst_file.Size = new System.Drawing.Size(59, 13);
            this.label_dst_file.TabIndex = 2;
            this.label_dst_file.Text = "Приемник";
            // 
            // textBox_src_file
            // 
            this.textBox_src_file.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_src_file.Enabled = false;
            this.textBox_src_file.Location = new System.Drawing.Point(64, 3);
            this.textBox_src_file.Name = "textBox_src_file";
            this.textBox_src_file.Size = new System.Drawing.Size(489, 20);
            this.textBox_src_file.TabIndex = 1;
            // 
            // label_src_file
            // 
            this.label_src_file.AutoSize = true;
            this.label_src_file.Location = new System.Drawing.Point(3, 6);
            this.label_src_file.Name = "label_src_file";
            this.label_src_file.Size = new System.Drawing.Size(55, 13);
            this.label_src_file.TabIndex = 0;
            this.label_src_file.Text = "Источник";
            // 
            // FormAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 167);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAction";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Копирование";
            this.Shown += new System.EventHandler(this.FormAction_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAction_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox_dst_file;
        private System.Windows.Forms.Label label_dst_file;
        private System.Windows.Forms.TextBox textBox_src_file;
        private System.Windows.Forms.Label label_src_file;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.ListBox listBox_log;
        private System.Windows.Forms.ProgressBar progressBar_crypt;
    }
}