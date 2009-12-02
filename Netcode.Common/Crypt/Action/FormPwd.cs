using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Remote_DCrypt.Action
{
    public partial class FormPwd : Form
    {
        public string MainText
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public string Password
        {
            get { return textBoxCurr.Text; }
            set { textBoxCurr.Text = value; }
        }

        public FormPwd()
        {
            InitializeComponent();
            this.ActiveControl = textBoxCurr;
            button_cancel.Click += new EventHandler(button_cancel_Click);
            button_ok.Click += new EventHandler(button_ok_Click);
            textBoxCurr.KeyDown += new KeyEventHandler(textBox_KeyDown);
            textBoxNew.KeyDown += new KeyEventHandler(textBox_KeyDown);
        }

        public FormPwd(bool isLoadSettings)
        {
            InitializeComponent();
            this.ActiveControl = textBoxCurr;
            button_cancel.Click += new EventHandler(button_cancel_Click);
            button_ok.Click += new EventHandler(button_ok_Click);
            textBoxCurr.KeyDown += new KeyEventHandler(textBox_KeyDown);
            textBoxNew.KeyDown += new KeyEventHandler(textBox_KeyDown);
            if (isLoadSettings)
            {
                this.Height = 56;
            }
            else
            {
                this.Height = 56/*81*/;
            }
        }

        void button_ok_Click(object sender, EventArgs e)
        {
            CheckBlankPwd();
        }

        private void CheckBlankPwd()
        {
            if (textBoxCurr.Text.Length < 1 /*|| textBoxNew.Text.Length < 1*/)
            {
                this.ActiveControl = textBoxCurr;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }


        void button_cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckBlankPwd();
            }
        }

    }
}   