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
        private SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KinoAndmebaas.mdf;Integrated Security=True");
        private SqlCommand command;
        private SqlDataReader reader;

        private FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
        };
        private TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel
        {
            Dock = DockStyle.Bottom,
            AutoSize = true
        };
        private Button button99 = new Button
        {
            Text = "Logi sisse",
            Anchor = AnchorStyles.Right,
        };

        private Button button1 = new Button
        {
            Text = "Kinokava"
        };

        private Label label1 = new Label
        {
            Location = new Point(75, 55)
        };

        public VaatamineVorm()
        {
            InitializeComponent();
            this.Text = "Vaatamine";
            label1.Text = Globals.kasutajaTuup;
            button99.Click += button1_Click;
            this.Controls.Add(label1);
            this.Controls.Add(tableLayoutPanel1);
            tableLayoutPanel1.Controls.Add(button1, 0, 0);
            tableLayoutPanel1.Controls.Add(button99, 1, 0);

            this.Controls.Add(flowLayoutPanel1);

            DisplayMovies(connection);
        }

        public void DisplayMovies(SqlConnection conn)
        {
            try
            {
                conn.Open();
                command = new SqlCommand("SELECT Seanssid.ID, Seanssid.Aeg, filmid.ID AS FilmID, filmid.Nimetus AS FilmNimetus, keeled.ID AS KeelID, keeled.Nimetus AS KeelNimetus, saalid.ID AS SaalID, saalid.Nimetus AS SaalNimetus FROM Seanssid INNER JOIN Filmid filmid ON Seanssid.FilmID = filmid.ID INNER JOIN Keeled keeled ON Seanssid.KeelID = keeled.ID INNER JOIN Saalid saalid ON Seanssid.SaalID = saalid.ID;", connection);
                reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    Panel panel = new Panel();

                    Label label = new Label
                    {
                        AutoSize = true,
                        Text = reader.GetString(3)
                    };
                    Label label2 = new Label
                    {
                        AutoSize = true,
                        Text = reader.GetInt32(4).ToString()
                    };
                    Label label3 = new Label
                    {
                        AutoSize = true,
                        Text = reader.GetString(5)
                    };
                    

                    panel.Controls.Add(label);
                    panel.Controls.Add(label2);
                    panel.Controls.Add(label3);


                    flowLayoutPanel1.Controls.Add(panel);
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

        public void UpdateData()
        {
            label1.Text = Globals.kasutajaTuup;
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