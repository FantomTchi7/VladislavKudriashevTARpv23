using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace Kino
{
    public static class Globals
    {
        public static string kasutajaTuup = "Vaataja";
        public static string kasutajaNimi = "Vaataja";
        public static int kasutajaID = 3;
        public static VaatamineVorm vaatamineVorm = new VaatamineVorm();
        public static SisselogimineVorm sisselogimineVorm = new SisselogimineVorm();
        public static BroneerimineVorm broneerimineVorm;
    }

    public partial class VaatamineVorm : Form
    {
        private int i;
        private int j;

        private SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Alena\\source\\repos\\FantomTchi7\\VladislavKudriashevTARpv23\\Programeerimine II\\Kino\\KinoAndmebaas.mdf\";Integrated Security=True");
        private SqlCommand command;
        private SqlDataReader reader;

        private const int tableLayoutPanel1Height = 30;
        private const int moviePanelWidth = 400;
        private const int moviePanelHeight = 200;
        private const int moviePosterWidth = 200 * 2 / 3;
        private const int padding = 10;
        private const int margin = 10;
        private int usableWidth;
        private int controlSpan;
        private int flowLayoutPanel1X;

        private int columnWidth;
        private int availableWidth;

        private List<KeyValuePair<int, string>> kinoList;
        private Dictionary<DateTime, List<Panel>> screeningsByDate = new Dictionary<DateTime, List<Panel>>();
        private List<Panel> moviePanels;
        private DateTime day;

        private TextInfo textInfo = new CultureInfo("et-EE").TextInfo;
        private CultureInfo cultureInfo = new CultureInfo("et-EE");

        private MemoryStream ms;

        private FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel
        {
            AutoScroll = true,
            Top = tableLayoutPanel1Height
        };
        private TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel
        {
            Height = tableLayoutPanel1Height
        };
        private FlowLayoutPanel flowLayoutPanelLeft = new FlowLayoutPanel
        {
            AutoSize = true,
            Padding = new Padding(0),
            Margin = new Padding(0)
        };
        private FlowLayoutPanel flowLayoutPanelRight = new FlowLayoutPanel
        {
            AutoSize = true,
            Padding = new Padding(0),
            Margin = new Padding(0),
            Anchor = AnchorStyles.Right
        };
        private Button button1 = new Button
        {
            Text = "Kinokava"
        };
        private Button button2 = new Button
        {
            Text = "Filmid"
        };
        private Button button50 = new Button
        {
            Text = "Haldus"
        };
        private ComboBox comboBox98 = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        private Button button99 = new Button
        {
            Text = "Logi sisse"
        };

        private ComboBox comboBoxTable = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        private DataGridView dataGridView1 = new DataGridView
        {
            Name = "dataGridView1",
            Dock = DockStyle.Fill,
            AutoSize = true
        };

        // MoviePanel

        Panel moviePanel;
        PictureBox poster;
        Label titleLabel;
        Label descriptionLabel;
        Label releaseDateLabel;
        Label durationLabel;
        Label languageLabel;
        Label hallLabel;
        Label timeLabel;
        Label directorLabel;
        Label zanrLabel;
        Label ratingLabel;
        FlowLayoutPanel infoPanel;
        FlowLayoutPanel mainPanel;
        Label dayHeader;
        Label noScreeningsLabel;

        Button reservationButton;

        public VaatamineVorm()
        {
            InitializeComponent();
            PopulateComboBox();
            Text = "Vaatamine";
            ClientSizeChanged += (_, _) => resize();

            tableLayoutPanel1 = CreateButtonPanel();
            Controls.Add(tableLayoutPanel1);
            Controls.Add(flowLayoutPanel1);

            resize();
        }

        private void resize()
        {
            usableWidth = this.Width - SystemInformation.VerticalScrollBarWidth;
            tableLayoutPanel1.Width = usableWidth;

            if (!flowLayoutPanel1.Controls.ContainsKey("dataGridView1"))
            {
                // The first call is to correctly populate flowLayoutPanel1; if this step is omitted, then an increase in the control's width won't add columns appropriately. 
                flowLayoutPanel1.Width = usableWidth;

                flowLayoutPanel1.Height = this.Height - (tableLayoutPanel1Height * 2) - (SystemInformation.HorizontalScrollBarHeight / 2);

                controlSpan = GetColumnCount(flowLayoutPanel1) * (moviePanelWidth + padding + margin);
                flowLayoutPanel1X = (flowLayoutPanel1.Width - controlSpan) / 2;

                // The second call is to make the scrollbar appear properly.
                flowLayoutPanel1.Width = this.Width - flowLayoutPanel1X;

                flowLayoutPanel1.Left = flowLayoutPanel1X - SystemInformation.VerticalScrollBarWidth;
            }
            else
            {
                flowLayoutPanel1.Left = 0;
            }
        }

        private int GetColumnCount(FlowLayoutPanel flp)
        {
            if (flp.Controls.Count == 0)
                return 1;

            columnWidth = moviePanelWidth + padding + margin;
            availableWidth = flp.ClientRectangle.Width - flp.Padding.Horizontal;

            return Math.Max(availableWidth / columnWidth, 1);
        }

        private void PopulateComboBox()
        {
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT ID, Nimetus FROM Kinod;", connection);
                reader = command.ExecuteReader();

                kinoList = new List<KeyValuePair<int, string>>();
                while (reader.Read())
                {
                    kinoList.Add(new KeyValuePair<int, string>((int)reader["ID"], reader["Nimetus"].ToString()));
                }

                comboBox98.DisplayMember = "Value";
                comboBox98.ValueMember = "Key";
                comboBox98.DataSource = kinoList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private TableLayoutPanel CreateButtonPanel()
        {
            button1.Click += (_, _) => LoadScreenings();
            button2.Click += (_, _) => LoadMovies();
            button50.Click += (_, _) => LoadHaldus();
            // button98.Click += (_, _) => ;
            button99.Click += (_, _) => OpenLoginForm();

            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.UserDeletingRow += DataGridView1_UserDeletingRow;

            flowLayoutPanelLeft.Controls.Add(button1);
            flowLayoutPanelLeft.Controls.Add(button2);

            flowLayoutPanelRight.Controls.Add(comboBox98);
            flowLayoutPanelRight.Controls.Add(button99);

            tableLayoutPanel1.Controls.Add(flowLayoutPanelLeft, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanelRight, 1, 0);
            return tableLayoutPanel1;
        }

        private void LoadScreenings()
        {
            screeningsByDate.Clear();

            ExecuteQuery(
                "SELECT " +
                "Seanssid.ID AS SeanssID, Seanssid.Aeg, " +
                "filmid.ID AS FilmID, filmid.Nimetus AS FilmNimetus, filmid.Kirjeldus, filmid.Kestus, filmid.Valjalaskeaeg, filmid.Poster, " +
                "keeled.Nimetus AS KeelNimetus, " +
                "saalid.Nimetus AS SaalNimetus, saalid.Tuup " +
                "FROM Seanssid " +
                "INNER JOIN Filmid filmid ON Seanssid.FilmID = filmid.ID " +
                "INNER JOIN Keeled keeled ON Seanssid.KeelID = keeled.ID " +
                "INNER JOIN Saalid saalid ON Seanssid.SaalID = saalid.ID " +
                "WHERE saalid.KinoID = @KinoID " +
                "ORDER BY Seanssid.Aeg;",
                reader => AddScreeningPanel(reader), new SqlParameter("@KinoID", ((KeyValuePair<int, string>)comboBox98.SelectedItem).Key));

            for (i = 0; i < 7; i++)
            {
                day = DateTime.Today.AddDays(i);

                dayHeader = new Label
                {
                    Text = textInfo.ToTitleCase(day.ToString("dddd", cultureInfo) + ", " + day.ToString("M", cultureInfo)),
                    Font = new Font("Arial", 16, FontStyle.Bold),
                    AutoSize = true,
                    Margin = new Padding(margin)
                };
                flowLayoutPanel1.SetFlowBreak(dayHeader, true);
                flowLayoutPanel1.Controls.Add(dayHeader);

                if (screeningsByDate.ContainsKey(day))
                {
                    moviePanels = screeningsByDate[day];
                    for (j = 0; j < moviePanels.Count; j++)
                    {
                        if (j == moviePanels.Count - 1)
                        {
                            flowLayoutPanel1.SetFlowBreak(moviePanels[j], true);
                        }
                        flowLayoutPanel1.Controls.Add(moviePanels[j]);
                    }
                }
                else
                {
                    noScreeningsLabel = new Label
                    {
                        Text = "Seansse pole saadaval.",
                        AutoSize = true,
                        Margin = new Padding(margin)
                    };
                    flowLayoutPanel1.SetFlowBreak(noScreeningsLabel, true);
                    flowLayoutPanel1.Controls.Add(noScreeningsLabel);
                }
            }
            resize();
        }

        private void LoadMovies()
        {
            ExecuteQuery(
                "SELECT " +
                "Filmid.ID, " +
                "Filmid.Nimetus, " +
                "Filmid.Kirjeldus, " +
                "Filmid.Kestus, " +
                "Filmid.Valjalaskeaeg, " +
                "Filmid.Poster, " +
                "Rezissoorid.Taisnimi AS RezissoorNimi, " +
                "Zanrid.Nimetus AS ZanrNimetus, " +
                "Vanusepiirangud.Nimetus AS VanusepiirangNimetus " +
                "FROM Filmid " +
                "INNER JOIN Rezissoorid ON Filmid.RezissoorID = Rezissoorid.ID " +
                "INNER JOIN Zanrid ON Filmid.ZanrID = Zanrid.ID " +
                "INNER JOIN Vanusepiirangud ON Filmid.VanusepiirangID = Vanusepiirangud.ID;",
                reader => AddMoviePanel(reader));
            resize();
        }

        private void ExecuteQuery(string query, Action<SqlDataReader> processRow, params SqlParameter[] parameters)
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                connection.Open();
                command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    processRow(reader);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void AddScreeningPanel(SqlDataReader reader)
        {
            DateTime screeningDate = Convert.ToDateTime(reader["Aeg"]).Date;

            if (!screeningsByDate.ContainsKey(screeningDate))
            {
                screeningsByDate[screeningDate] = new List<Panel>();
            }

            Panel moviePanel = CreateMoviePanel(reader, includeScreeningInfo: true);
            screeningsByDate[screeningDate].Add(moviePanel);
        }

        private void AddMoviePanel(SqlDataReader reader)
        {
            moviePanel = CreateMoviePanel(reader, includeScreeningInfo: false);
            flowLayoutPanel1.Controls.Add(moviePanel);
        }

        private Panel CreateMoviePanel(SqlDataReader reader, bool includeScreeningInfo)
        {
            poster = new PictureBox
            {
                Padding = new Padding(0),
                Margin = new Padding(0),
                Width = moviePosterWidth,
                Height = moviePanelHeight,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            try
            {
                poster.Image = ByteArrayToImage((byte[])reader["Poster"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            infoPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true
            };

            if (includeScreeningInfo)
            {
                titleLabel = new Label
                {
                    Text = reader["FilmNimetus"].ToString(),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true
                };
                descriptionLabel = new Label
                {
                    Text = reader["Kirjeldus"].ToString(),
                    AutoEllipsis = true,
                    Width = 250,
                    Height = 50
                };
                durationLabel = new Label
                {
                    Text = $"Kestus: {reader["Kestus"]} minutid",
                    AutoSize = true
                };
                languageLabel = new Label
                {
                    Text = $"Keel: {reader["KeelNimetus"]}",
                    AutoSize = true
                };
                hallLabel = new Label
                {
                    Text = $"Saal: {reader["SaalNimetus"]} ({reader["Tuup"]})",
                    AutoSize = true
                };
                timeLabel = new Label
                {
                    Text = $"Aeg: {Convert.ToDateTime(reader["Aeg"]).ToString("t", cultureInfo)}",
                    AutoSize = true
                };

                reservationButton = new Button
                {
                    Tag = (int)reader["SeanssID"],
                    Text = $"Broneerin"
                };

                reservationButton.Click += reservationButton_Click;

                infoPanel.Controls.Add(titleLabel);
                infoPanel.Controls.Add(descriptionLabel);
                infoPanel.Controls.Add(durationLabel);
                infoPanel.Controls.Add(languageLabel);
                infoPanel.Controls.Add(hallLabel);
                infoPanel.Controls.Add(timeLabel);
                infoPanel.Controls.Add(reservationButton);
            }
            else
            {
                titleLabel = new Label
                {
                    Text = reader["Nimetus"].ToString(),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true
                };
                descriptionLabel = new Label
                {
                    Text = reader["Kirjeldus"].ToString(),
                    AutoEllipsis = true,
                    Width = 250,
                    Height = 50
                };
                releaseDateLabel = new Label
                {
                    Text = $"Esilinastus: {Convert.ToDateTime(reader["Valjalaskeaeg"]).ToString("d", cultureInfo)}",
                    AutoSize = true
                };
                durationLabel = new Label
                {
                    Text = $"Kestus: {reader["Kestus"]} minutid",
                    AutoSize = true
                };
                directorLabel = new Label
                {
                    Text = $"Režissöör: {reader["RezissoorNimi"]}",
                    AutoSize = true
                };
                zanrLabel = new Label
                {
                    Text = $"Žanr: {reader["ZanrNimetus"]}",
                    AutoSize = true
                };
                ratingLabel = new Label
                {
                    Text = $"Vanusepiirang: {reader["VanusepiirangNimetus"]}",
                    AutoSize = true
                };

                infoPanel.Controls.Add(titleLabel);
                infoPanel.Controls.Add(descriptionLabel);
                infoPanel.Controls.Add(releaseDateLabel);
                infoPanel.Controls.Add(durationLabel);
                infoPanel.Controls.Add(directorLabel);
                infoPanel.Controls.Add(zanrLabel);
                infoPanel.Controls.Add(ratingLabel);
            }

            moviePanel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = moviePanelWidth,
                Height = moviePanelHeight,
                Margin = new Padding(margin)
            };

            mainPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };

            mainPanel.Controls.Add(poster);
            mainPanel.Controls.Add(infoPanel);

            moviePanel.Controls.Add(mainPanel);
            return moviePanel;
        }

        private void reservationButton_Click(object sender, EventArgs e)
        {
            Globals.broneerimineVorm = new BroneerimineVorm((int)((Button)sender).Tag);
            Globals.broneerimineVorm.ShowDialog();
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            ms = new MemoryStream(byteArray);
            return Image.FromStream(ms);
        }

        private void LoadHaldus()
        {
            flowLayoutPanel1.Controls.Clear();

            PopulateComboBoxTable();

            comboBoxTable.SelectedIndexChanged += (sender, e) => {
                flowLayoutPanel1.Controls.Remove(dataGridView1);
                LoadDataGridView($"SELECT * FROM {comboBoxTable.SelectedItem.ToString()}");
                flowLayoutPanel1.Controls.Add(dataGridView1);
            };

            flowLayoutPanel1.Controls.Add(comboBoxTable);
            flowLayoutPanel1.Controls.Add(dataGridView1);
        }

        private void PopulateComboBoxTable()
        {
            try
            {
                connection.Open();

                DataTable schemaTable = connection.GetSchema("Tables");
                List<string> tableNames = new List<string>();

                foreach (DataRow row in schemaTable.Rows)
                {
                    tableNames.Add(row["TABLE_NAME"].ToString());
                }

                comboBoxTable.DataSource = tableNames;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void LoadDataGridView(string query)
        {
            try
            {
                dataGridView1.DataSource = null;
                try
                {
                    connection.Open();
                } catch { }

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.DataSource is DataTable table)
                {
                    DataRow row = table.Rows[e.RowIndex];
                    string columnName = table.Columns[e.ColumnIndex].ColumnName;
                    object newValue = row[columnName];

                    string query = $"UPDATE {comboBoxTable.SelectedItem} SET {columnName} = @Value WHERE ID = @ID";

                    connection.Open();
                    using SqlCommand updateCommand = new SqlCommand(query, connection);
                    updateCommand.Parameters.AddWithValue("@Value", newValue);
                    updateCommand.Parameters.AddWithValue("@ID", row["ID"]);

                    int affectedRows = updateCommand.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        MessageBox.Show("No rows updated. Please check the primary key or constraints.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error updating database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                string query = $"DELETE FROM {comboBoxTable.SelectedItem} WHERE ID = @ID";

                connection.Open();
                using SqlCommand deleteCommand = new SqlCommand(query, connection);
                deleteCommand.Parameters.AddWithValue("@ID", e.Row.Cells["ID"].Value);

                deleteCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error deleting row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            finally
            {
                connection.Close();
            }
        }

        private void OpenLoginForm()
        {
            Hide();
            Globals.sisselogimineVorm?.Show();
        }

        public void UpdateData()
        {
            button99.Text = Globals.kasutajaNimi;

            if (Globals.kasutajaTuup == "Admin")
            {
                flowLayoutPanelLeft.Controls.Add(button50);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}