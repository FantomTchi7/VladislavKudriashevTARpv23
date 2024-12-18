using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kino
{
    public partial class VaatamineVorm : Form
    {
        private Button button1 = new Button { Text = "Logi sisse", Location = new Point(50, 50) };
        private Label label1 = new Label { Location = new Point(25, 25) };

        public VaatamineVorm()
        {
            InitializeComponent();
            this.Text = "Vaatamine";
            label1.Text = "Türe: " + Globals.kasutajaTuup;
            button1.Click += button1_Click;
            this.Controls.Add(label1);
            this.Controls.Add(button1);
        }

        public void UpdateData()
        {
            label1.Text = "Türe: " + Globals.kasutajaTuup;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Globals.sisselogimineVorm.Show();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}