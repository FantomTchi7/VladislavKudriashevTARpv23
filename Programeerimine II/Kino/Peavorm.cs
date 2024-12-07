﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kino
{
    public partial class Peavorm : Form
    {
        private static TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
        private static Label label1 = new Label
        {
            Dock = DockStyle.Left,
            Text = "Nimi:",
            AutoSize = true
        };
        private static Label label2 = new Label
        {
            Dock = DockStyle.Left,
            Text = "Parool:",
            AutoSize = true
        };
        private static TextBox textBox1 = new TextBox
        {
            Dock = DockStyle.Right,
        };
        private static TextBox textBox2 = new TextBox
        {
            Dock = DockStyle.Right,
            PasswordChar = '*'
        };
        private static Label label3 = new Label
        {
            Text = "Kas teil pole kontot? Looge konto.",
            AutoSize = true,
            Anchor = AnchorStyles.Top
        };
        private static Button button1 = new Button
        {
            Text = "Logi sisse",
            Anchor = AnchorStyles.Top
        };

        public Peavorm()
        {
            InitializeComponent();
            CenterTableLayoutPanel();

            this.Text = "Sisselogimine";

            label3.MouseEnter += label3_MouseEnter;
            label3.MouseLeave += label3_MouseLeave;

            tableLayoutPanel1.Anchor = AnchorStyles.None;

            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(textBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(textBox1, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(button1, 0, 3);

            tableLayoutPanel1.AutoSize = true;

            this.Controls.Add(tableLayoutPanel1);

            this.Resize += (sender, e) => CenterTableLayoutPanel();
        }

        private void CenterTableLayoutPanel()
        {
            tableLayoutPanel1.Left = (this.ClientSize.Width - tableLayoutPanel1.Width) / 2;
            tableLayoutPanel1.Top = (this.ClientSize.Height - tableLayoutPanel1.Height) / 2;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.Font = new Font(label1.Font, FontStyle.Underline);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.Font = new Font(label1.Font, FontStyle.Regular);
        }
    }
}