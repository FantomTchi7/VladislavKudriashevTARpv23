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
    public partial class SisselogimineVorm : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KinoAndmebaas.mdf;Integrated Security=True");
        SqlCommand command;
        SqlDataAdapter adapter;
        SqlDataReader reader;

        private static TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
        private static Label label1 = new Label
        {
            Dock = DockStyle.Left,
            Text = "Nimi või email:",
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
        public SisselogimineVorm()
        {
            InitializeComponent();
            CenterTableLayoutPanel();

            this.Text = "Sisselogimine";
            this.MinimumSize = new Size(275, 175);

            label3.MouseEnter += label3_MouseEnter;
            label3.MouseLeave += label3_MouseLeave;
            button1.Click += button1_Click;
            label3.Click += label3_Click;

            tableLayoutPanel1.Anchor = AnchorStyles.None;

            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(textBox1, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(textBox2, 1, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(button1, 0, 3);

            tableLayoutPanel1.SetColumnSpan(label3, 2);
            tableLayoutPanel1.SetColumnSpan(button1, 2);

            tableLayoutPanel1.AutoSize = true;

            this.Controls.Add(tableLayoutPanel1);

            this.Resize += (sender, e) => CenterTableLayoutPanel();
        }
        private void CenterTableLayoutPanel()
        {
            tableLayoutPanel1.Left = (this.ClientSize.Width - tableLayoutPanel1.Width) / 2;
            tableLayoutPanel1.Top = (this.ClientSize.Height - tableLayoutPanel1.Height) / 2;
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string nimiVoiEmail = textBox1.Text;
            string parool = textBox2.Text;

            try
            {
                connection.Open();

                command = new SqlCommand("SELECT Huudnimi, Tuup FROM Kontod WHERE (Huudnimi = @nimiVoiEmail OR Email = @nimiVoiEmail) AND Parool = @parool", connection);
                command.Parameters.AddWithValue("@nimiVoiEmail", nimiVoiEmail);
                command.Parameters.AddWithValue("@parool", parool);

                reader = command.ExecuteReader();
                string result = null;
                string result2 = null;

                if (reader.Read())
                {
                    result = reader["Huudnimi"]?.ToString();
                    result2 = reader["Tuup"]?.ToString();
                }

                if (result != null)
                {
                    VaatamineVorm vaatamineVorm = new VaatamineVorm(result2);
                    vaatamineVorm.Closed += (s, args) => this.Show();
                    this.Hide();
                    vaatamineVorm.Show();
                }
                else
                {
                    MessageBox.Show("Vale kasutajanimi/email või parool.", "Sisselogimine ebaõnnestus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                connection.Close();
            }
            catch (SqlException sqlE)
            {
                MessageBox.Show($"Viga andmebaasiga ühenduse loomisel: {sqlE.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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