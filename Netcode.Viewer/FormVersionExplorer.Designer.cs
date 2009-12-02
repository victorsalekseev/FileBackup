namespace Netcode.Viewer
{
    partial class FormVersionExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVersionExplorer));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.listView_trace = new System.Windows.Forms.ListView();
            this.����� = new System.Windows.Forms.ColumnHeader();
            this.�������� = new System.Windows.Forms.ColumnHeader();
            this.����������� = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_filelist = new System.Windows.Forms.Panel();
            this.fileTreeExplorer = new Netcode.Common.Controls.FileTreeExplorer();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.panel_tool = new System.Windows.Forms.Panel();
            this.toolStrip_recovery = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBox_MethodRec = new System.Windows.Forms.ToolStripComboBox();
            this.folderTreeExplorer = new Netcode.Controls.FolderTreeExplorer();
            this.folderBrowserDialog_recover_dir = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxSelectFolderRecTo = new Local_DCrypt.Controls.TextBoxSelectFolder();
            this.toolStripSplitButtonRecover = new System.Windows.Forms.ToolStripSplitButton();
            this.���ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton_checked = new System.Windows.Forms.ToolStripSplitButton();
            this.���ToolStripMenuItemCh = new System.Windows.Forms.ToolStripMenuItem();
            this.�����ToolStripMenuItemCh = new System.Windows.Forms.ToolStripMenuItem();
            this.�����ToolStripMenuItemCh = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel_filelist.SuspendLayout();
            this.panel_tool.SuspendLayout();
            this.toolStrip_recovery.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "comment.png");
            this.imageList.Images.SetKeyName(1, "comment_w.png");
            this.imageList.Images.SetKeyName(2, "edit_add.png");
            this.imageList.Images.SetKeyName(3, "info.png");
            this.imageList.Images.SetKeyName(4, "stop.png");
            // 
            // listView_trace
            // 
            this.listView_trace.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.�����,
            this.��������,
            this.�����������});
            this.listView_trace.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView_trace.FullRowSelect = true;
            this.listView_trace.GridLines = true;
            this.listView_trace.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_trace.LargeImageList = this.imageList;
            this.listView_trace.Location = new System.Drawing.Point(0, 325);
            this.listView_trace.MultiSelect = false;
            this.listView_trace.Name = "listView_trace";
            this.listView_trace.Size = new System.Drawing.Size(692, 141);
            this.listView_trace.SmallImageList = this.imageList;
            this.listView_trace.TabIndex = 21;
            this.listView_trace.UseCompatibleStateImageBehavior = false;
            this.listView_trace.View = System.Windows.Forms.View.Details;
            // 
            // �����
            // 
            this.�����.Text = "�����";
            this.�����.Width = 134;
            // 
            // ��������
            // 
            this.��������.Text = "��������";
            this.��������.Width = 131;
            // 
            // �����������
            // 
            this.�����������.Text = "�����������";
            this.�����������.Width = 394;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_filelist);
            this.panel1.Controls.Add(this.folderTreeExplorer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 325);
            this.panel1.TabIndex = 22;
            // 
            // panel_filelist
            // 
            this.panel_filelist.Controls.Add(this.fileTreeExplorer);
            this.panel_filelist.Controls.Add(this.textBox_path);
            this.panel_filelist.Controls.Add(this.panel_tool);
            this.panel_filelist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_filelist.Location = new System.Drawing.Point(242, 0);
            this.panel_filelist.Name = "panel_filelist";
            this.panel_filelist.Size = new System.Drawing.Size(450, 325);
            this.panel_filelist.TabIndex = 3;
            // 
            // fileTreeExplorer
            // 
            this.fileTreeExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTreeExplorer.Location = new System.Drawing.Point(0, 20);
            this.fileTreeExplorer.Name = "fileTreeExplorer";
            this.fileTreeExplorer.Size = new System.Drawing.Size(450, 258);
            this.fileTreeExplorer.TabIndex = 7;
            // 
            // textBox_path
            // 
            this.textBox_path.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_path.Location = new System.Drawing.Point(0, 0);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.ReadOnly = true;
            this.textBox_path.Size = new System.Drawing.Size(450, 20);
            this.textBox_path.TabIndex = 6;
            // 
            // panel_tool
            // 
            this.panel_tool.Controls.Add(this.textBoxSelectFolderRecTo);
            this.panel_tool.Controls.Add(this.toolStrip_recovery);
            this.panel_tool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_tool.Location = new System.Drawing.Point(0, 278);
            this.panel_tool.Name = "panel_tool";
            this.panel_tool.Size = new System.Drawing.Size(450, 47);
            this.panel_tool.TabIndex = 0;
            // 
            // toolStrip_recovery
            // 
            this.toolStrip_recovery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonRecover,
            this.toolStripSeparator,
            this.toolStripButton_checked,
            this.toolStripComboBox_MethodRec});
            this.toolStrip_recovery.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_recovery.Name = "toolStrip_recovery";
            this.toolStrip_recovery.Size = new System.Drawing.Size(450, 25);
            this.toolStrip_recovery.TabIndex = 0;
            this.toolStrip_recovery.Text = "toolStrip1";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripComboBox_MethodRec
            // 
            this.toolStripComboBox_MethodRec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox_MethodRec.DropDownWidth = 175;
            this.toolStripComboBox_MethodRec.Enabled = false;
            this.toolStripComboBox_MethodRec.Items.AddRange(new object[] {
            "������������ � �����...",
            "������������ � ����������� �����"});
            this.toolStripComboBox_MethodRec.Name = "toolStripComboBox_MethodRec";
            this.toolStripComboBox_MethodRec.Size = new System.Drawing.Size(177, 25);
            // 
            // folderTreeExplorer
            // 
            this.folderTreeExplorer.Dock = System.Windows.Forms.DockStyle.Left;
            this.folderTreeExplorer.Location = new System.Drawing.Point(0, 0);
            this.folderTreeExplorer.Name = "folderTreeExplorer";
            this.folderTreeExplorer.Size = new System.Drawing.Size(242, 325);
            this.folderTreeExplorer.TabIndex = 2;
            // 
            // textBoxSelectFolderRecTo
            // 
            this.textBoxSelectFolderRecTo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxSelectFolderRecTo.InputText = "";
            this.textBoxSelectFolderRecTo.Location = new System.Drawing.Point(0, 26);
            this.textBoxSelectFolderRecTo.Name = "textBoxSelectFolderRecTo";
            this.textBoxSelectFolderRecTo.Size = new System.Drawing.Size(450, 21);
            this.textBoxSelectFolderRecTo.TabIndex = 2;
            // 
            // toolStripSplitButtonRecover
            // 
            this.toolStripSplitButtonRecover.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.���ToolStripMenuItem,
            this.�����ToolStripMenuItem,
            this.�����ToolStripMenuItem});
            this.toolStripSplitButtonRecover.Image = global::Netcode.Viewer.Properties.Resources.db_update;
            this.toolStripSplitButtonRecover.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonRecover.Name = "toolStripSplitButtonRecover";
            this.toolStripSplitButtonRecover.Size = new System.Drawing.Size(197, 22);
            this.toolStripSplitButtonRecover.Text = "������������ / ������������";
            // 
            // ���ToolStripMenuItem
            // 
            this.���ToolStripMenuItem.Enabled = false;
            this.���ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.���ToolStripMenuItem.Image = global::Netcode.Viewer.Properties.Resources.folder_and_file_yellow;
            this.���ToolStripMenuItem.Name = "���ToolStripMenuItem";
            this.���ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.���ToolStripMenuItem.Text = "���";
            // 
            // �����ToolStripMenuItem
            // 
            this.�����ToolStripMenuItem.Enabled = false;
            this.�����ToolStripMenuItem.Image = global::Netcode.Viewer.Properties.Resources.comment;
            this.�����ToolStripMenuItem.Name = "�����ToolStripMenuItem";
            this.�����ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.�����ToolStripMenuItem.Text = "�����";
            // 
            // �����ToolStripMenuItem
            // 
            this.�����ToolStripMenuItem.Enabled = false;
            this.�����ToolStripMenuItem.Image = global::Netcode.Viewer.Properties.Resources.folder_yellow;
            this.�����ToolStripMenuItem.Name = "�����ToolStripMenuItem";
            this.�����ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.�����ToolStripMenuItem.Text = "�����";
            // 
            // toolStripButton_checked
            // 
            this.toolStripButton_checked.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_checked.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.���ToolStripMenuItemCh,
            this.�����ToolStripMenuItemCh,
            this.�����ToolStripMenuItemCh});
            this.toolStripButton_checked.Image = global::Netcode.Viewer.Properties.Resources.checked_file;
            this.toolStripButton_checked.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_checked.Name = "toolStripButton_checked";
            this.toolStripButton_checked.Size = new System.Drawing.Size(32, 22);
            this.toolStripButton_checked.Text = "������ ��������� ��������";
            // 
            // ���ToolStripMenuItemCh
            // 
            this.���ToolStripMenuItemCh.Image = global::Netcode.Viewer.Properties.Resources.folder_and_file_yellow;
            this.���ToolStripMenuItemCh.Name = "���ToolStripMenuItemCh";
            this.���ToolStripMenuItemCh.Size = new System.Drawing.Size(152, 22);
            this.���ToolStripMenuItemCh.Text = "���";
            this.���ToolStripMenuItemCh.Visible = false;
            // 
            // �����ToolStripMenuItemCh
            // 
            this.�����ToolStripMenuItemCh.Image = global::Netcode.Viewer.Properties.Resources.comment;
            this.�����ToolStripMenuItemCh.Name = "�����ToolStripMenuItemCh";
            this.�����ToolStripMenuItemCh.Size = new System.Drawing.Size(152, 22);
            this.�����ToolStripMenuItemCh.Text = "�����";
            // 
            // �����ToolStripMenuItemCh
            // 
            this.�����ToolStripMenuItemCh.Image = global::Netcode.Viewer.Properties.Resources.folder_yellow;
            this.�����ToolStripMenuItemCh.Name = "�����ToolStripMenuItemCh";
            this.�����ToolStripMenuItemCh.Size = new System.Drawing.Size(152, 22);
            this.�����ToolStripMenuItemCh.Text = "�����";
            this.�����ToolStripMenuItemCh.Visible = false;
            // 
            // FormVersionExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 466);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView_trace);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormVersionExplorer";
            this.Text = "�������� �����-���������";
            this.panel1.ResumeLayout(false);
            this.panel_filelist.ResumeLayout(false);
            this.panel_filelist.PerformLayout();
            this.panel_tool.ResumeLayout(false);
            this.panel_tool.PerformLayout();
            this.toolStrip_recovery.ResumeLayout(false);
            this.toolStrip_recovery.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ListView listView_trace;
        private System.Windows.Forms.ColumnHeader �����;
        private System.Windows.Forms.ColumnHeader ��������;
        private System.Windows.Forms.ColumnHeader �����������;
        private System.Windows.Forms.Panel panel1;
        private Netcode.Controls.FolderTreeExplorer folderTreeExplorer;
        private System.Windows.Forms.Panel panel_filelist;
        private System.Windows.Forms.Panel panel_tool;
        private System.Windows.Forms.ToolStrip toolStrip_recovery;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonRecover;
        private System.Windows.Forms.ToolStripMenuItem ���ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_MethodRec;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_recover_dir;
        private Netcode.Common.Controls.FileTreeExplorer fileTreeExplorer;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.ToolStripSplitButton toolStripButton_checked;
        private System.Windows.Forms.ToolStripMenuItem ���ToolStripMenuItemCh;
        private System.Windows.Forms.ToolStripMenuItem �����ToolStripMenuItemCh;
        private System.Windows.Forms.ToolStripMenuItem �����ToolStripMenuItemCh;
        private Local_DCrypt.Controls.TextBoxSelectFolder textBoxSelectFolderRecTo;
    }
}