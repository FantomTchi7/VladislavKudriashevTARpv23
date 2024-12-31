using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Kino
{
    public partial class VaatamineVorm : Form
    {
        private SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KinoAndmebaas.mdf;Integrated Security=True");
        private SqlCommand command;
        private SqlDataReader reader;

        private FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel
        {
            AutoScroll = true,
            Anchor = AnchorStyles.Top | AnchorStyles.Left,
            Top = 25
        };
        private TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Left,
            Height = 25
        };
        private Button button99 = new Button
        {
            Text = "Logi sisse",
            Anchor = AnchorStyles.Right
        };

        private Button button1 = new Button
        {
            Text = "Kinokava"
        };

        public VaatamineVorm()
        {
            InitializeComponent();
            this.Resize += (sender, e) => resize();
            this.Text = "Vaatamine";
            button99.Click += button1_Click;
            this.Controls.Add(tableLayoutPanel1);
            tableLayoutPanel1.Controls.Add(button1, 0, 0);
            tableLayoutPanel1.Controls.Add(button99, 1, 0);

            this.Controls.Add(flowLayoutPanel1);

            DisplayScreenings(connection);
        }

        private void resize()
        {
            tableLayoutPanel1.Width = this.Width - SystemInformation.VerticalScrollBarWidth;
            flowLayoutPanel1.Width = this.Width - SystemInformation.VerticalScrollBarWidth;
            flowLayoutPanel1.Height = this.Height - 25;

            flowLayoutPanel1.Left = (flowLayoutPanel1.Width - (GetColumnCount(flowLayoutPanel1) * 420)) / 2;
        }

        private int GetColumnCount(FlowLayoutPanel flp)
        {
            if (flp.Controls.Count == 0)
                return 0;

            int columnCount = (flp.ClientRectangle.Width - flp.Padding.Left - flp.Padding.Right) /
                              (flp.Controls[0].Width + flp.Controls[0].Margin.Left + flp.Controls[0].Margin.Right);
            return columnCount > 0 ? columnCount : 1;
        }


        public void DisplayScreenings(SqlConnection conn)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(
                    "SELECT " +
                    "Seanssid.ID AS SeanssID, Seanssid.Aeg, " +
                    "filmid.ID AS FilmID, filmid.Nimetus AS FilmNimetus, filmid.Kirjeldus, filmid.Kestus, filmid.Väljalaskeaeg, filmid.Poster, " +
                    "keeled.Nimetus AS KeelNimetus, " +
                    "saalid.Nimetus AS SaalNimetus, saalid.Tuup " +
                    "FROM Seanssid " +
                    "INNER JOIN Filmid filmid ON Seanssid.FilmID = filmid.ID " +
                    "INNER JOIN Keeled keeled ON Seanssid.KeelID = keeled.ID " +
                    "INNER JOIN Saalid saalid ON Seanssid.SaalID = saalid.ID;",
                    conn);

                SqlDataReader reader = command.ExecuteReader();

                flowLayoutPanel1.Controls.Clear();

                while (reader.Read())
                {
                    Panel moviePanel = new Panel
                    {
                        BorderStyle = BorderStyle.FixedSingle,
                        Width = 400,
                        Height = 200,
                        Padding = new Padding(10),
                        Margin = new Padding(10)
                    };
                    PictureBox poster = new PictureBox
                    {
                        Width = 100,
                        Height = 150,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    try
                    {
                        poster.Image = ByteArrayToImage((byte[])reader["Poster"]);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Viga", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Label titleLabel = new Label
                    {
                        Text = reader["FilmNimetus"].ToString(),
                        Font = new Font("Arial", 14, FontStyle.Bold),
                        AutoSize = true
                    };
                    Label descriptionLabel = new Label
                    {
                        Text = reader["Kirjeldus"].ToString(),
                        AutoEllipsis = true,
                        Width = 250,
                        Height = 50
                    };
                    Label releaseDateLabel = new Label
                    {
                        Text = $"Release Date: {Convert.ToDateTime(reader["Väljalaskeaeg"]).ToShortDateString()}",
                        AutoSize = true
                    };
                    Label durationLabel = new Label
                    {
                        Text = $"Duration: {reader["Kestus"]} min",
                        AutoSize = true
                    };
                    Label languageLabel = new Label
                    {
                        Text = $"Language: {reader["KeelNimetus"]}",
                        AutoSize = true
                    };
                    Label hallLabel = new Label
                    {
                        Text = $"Hall: {reader["SaalNimetus"]} ({reader["Tuup"]})",
                        AutoSize = true
                    };
                    FlowLayoutPanel infoPanel = new FlowLayoutPanel
                    {
                        FlowDirection = FlowDirection.TopDown,
                        AutoSize = true
                    };

                    infoPanel.Controls.Add(titleLabel);
                    infoPanel.Controls.Add(descriptionLabel);
                    infoPanel.Controls.Add(releaseDateLabel);
                    infoPanel.Controls.Add(durationLabel);
                    infoPanel.Controls.Add(languageLabel);
                    infoPanel.Controls.Add(hallLabel);

                    FlowLayoutPanel mainPanel = new FlowLayoutPanel
                    {
                        FlowDirection = FlowDirection.LeftToRight,
                        AutoSize = true
                    };

                    mainPanel.Controls.Add(poster);
                    mainPanel.Controls.Add(infoPanel);

                    moviePanel.Controls.Add(mainPanel);
                    flowLayoutPanel1.Controls.Add(moviePanel);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error connecting to the database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        public void UpdateData()
        {
            button99.Text = Globals.kasutajaTuup;
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