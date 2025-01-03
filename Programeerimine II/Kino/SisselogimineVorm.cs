using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Kino
{
    public partial class SisselogimineVorm : Form
    {
        private SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Alena\\source\\repos\\FantomTchi7\\VladislavKudriashevTARpv23\\Programeerimine II\\Kino\\KinoAndmebaas.mdf\";Integrated Security=True");
        private SqlCommand command;
        private SqlDataReader reader;

        private TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
        private Label label1 = new Label { Text = "Nimi või email:", AutoSize = true };
        private Label label2 = new Label { Text = "Parool:", AutoSize = true };
        private TextBox textBox1 = new TextBox();
        private TextBox textBox2 = new TextBox { PasswordChar = '*' };
        private Button button1 = new Button { Text = "Logi sisse" };

        public SisselogimineVorm()
        {
            InitializeComponent();
            resize();
            this.Text = "Sisselogimine";
            this.MinimumSize = new Size(275, 175);
            button1.Click += button1_Click;

            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(textBox1, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(textBox2, 1, 1);
            tableLayoutPanel1.Controls.Add(button1, 0, 2);
            tableLayoutPanel1.SetColumnSpan(button1, 2);
            tableLayoutPanel1.AutoSize = true;
            this.Controls.Add(tableLayoutPanel1);
            this.ClientSizeChanged += (sender, e) => resize();
        }

        private void resize()
        {
            tableLayoutPanel1.Left = (this.ClientSize.Width - tableLayoutPanel1.Width) / 2;
            tableLayoutPanel1.Top = (this.ClientSize.Height - tableLayoutPanel1.Height) / 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nimiVoiEmail = textBox1.Text;
            string parool = textBox2.Text;

            try
            {
                connection.Open();
                command = new SqlCommand("SELECT Huudnimi, Tüüp FROM Kontod WHERE (Huudnimi = @nimiVoiEmail OR Email = @nimiVoiEmail) AND Parool = @parool", connection);
                command.Parameters.AddWithValue("@nimiVoiEmail", nimiVoiEmail);
                command.Parameters.AddWithValue("@parool", parool);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Globals.kasutajaTuup = reader["Tüüp"].ToString();
                    this.Hide();
                    Globals.vaatamineVorm.UpdateData();
                    Globals.vaatamineVorm.Show();
                }
                else
                {
                    MessageBox.Show("Vale kasutajanimi/email või parool.", "Sisselogimine ebaõnnestus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException sqlE)
            {
                MessageBox.Show($"Viga andmebaasiga ühenduse loomisel: {sqlE.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}