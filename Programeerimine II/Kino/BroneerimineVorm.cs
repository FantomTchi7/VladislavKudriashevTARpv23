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
    public partial class BroneerimineVorm : Form
    {
        private int i;
        private int j;

        private readonly SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Alena\\source\\repos\\FantomTchi7\\VladislavKudriashevTARpv23\\Programeerimine II\\Kino\\KinoAndmebaas.mdf\";Integrated Security=True");
        private SqlCommand command;
        private SqlDataReader reader;
        private string query;

        private FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            Dock = DockStyle.Fill
        };
        private TableLayoutPanel seatTable;
        private readonly int seanssID;
        private int read;
        private int veerud;
        private int rida;
        private int veerg;
        private Button[,] istmeNupud;
        private int buttonSize;
        private int startX;
        private int startY;
        private Button button;
        private Point location;
        private Dictionary<Point, int> valitudPiletituubid = new Dictionary<Point, int>();
        private Label infoLabel = new Label
        {
            AutoSize = true,
            Text = "Vali istekohad. Sinine - vaba, Punane - hõivatud, Roheline - valitud"
        };
        private Button kinnitaNupp = new Button
        {
            Text = "Kinnita broneering",
            AutoSize = true,
            Enabled = false
        };
        private List<Point> valitudIstmed = new List<Point>();
        private decimal hind;

        public BroneerimineVorm(int seanssID)
        {
            InitializeComponent();
            this.seanssID = seanssID;
            
            this.Text = "Istekoha broneerimine";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            kinnitaNupp.Click += KinnitaNupp_Click;

            this.Controls.Add(flowLayoutPanel1);
            flowLayoutPanel1.Controls.Add(infoLabel);

            try
            {
                try { connection.Open(); } catch { }

                using (command = new SqlCommand("SELECT SaalRead, SaalVeerud FROM Saalid s JOIN Seanssid ss ON s.ID = ss.SaalID WHERE ss.ID = @SeanssID", connection))
                {
                    command.Parameters.AddWithValue("@SeanssID", seanssID);
                    using (reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            read = reader.GetInt32(0);
                            veerud = reader.GetInt32(1);
                        }
                    }
                }

                seatTable = CreateSeatButtons();
                flowLayoutPanel1.Controls.Add(seatTable);

                LoadSeatStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }

            flowLayoutPanel1.Controls.Add(kinnitaNupp);
        }

        private ComboBox CreateTicketTypeComboBox()
        {
            ComboBox comboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            List<KeyValuePair<int, string>> ticketTypes = new List<KeyValuePair<int, string>>();

            try
            {
                try { connection.Open(); } catch { }
                command = new SqlCommand("SELECT ID, Tuup FROM Piletituubid;", connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ticketTypes.Add(new KeyValuePair<int, string>(
                        (int)reader["ID"],
                        reader["Tuup"].ToString()
                    ));
                }

                comboBox.DisplayMember = "Value";
                comboBox.ValueMember = "Key";
                comboBox.DataSource = new List<KeyValuePair<int, string>>(ticketTypes);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }

            return comboBox;
        }

        private TableLayoutPanel CreateSeatButtons()
        {
            seatTable = new TableLayoutPanel
            {
                Name = "seatTable",
                ColumnCount = veerud,
                RowCount = read,
                Width = this.Width,
                Height = this.Height
            };

            for (veerg = 0; veerg < veerud; veerg++)
            {
                seatTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            }
            for (rida = 0; rida < read; rida++)
            {
                seatTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            istmeNupud = new Button[read, veerud];
            buttonSize = 40;

            for (i = 0; i < read; i++)
            {
                for (j = 0; j < veerud; j++)
                {
                    button = new Button
                    {
                        Text = $"{i + 1}-{j + 1}",
                        BackColor = Color.LightBlue,
                        Width = buttonSize,
                        Height = buttonSize
                    };

                    button.Click += IstmeNupp_Click;
                    istmeNupud[i, j] = button;

                    seatTable.Controls.Add(button, j, i);
                }
            }

            return seatTable;
        }

        private void LoadSeatStatus()
        {
            query = @"
                SELECT Rida, Veerg
                FROM Piletid
                WHERE SeanssID = @SeanssID";

            using (command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SeanssID", seanssID);
                try { connection.Open(); } catch { }

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rida = reader.GetInt32(0) - 1;
                        veerg = reader.GetInt32(1) - 1;

                        if (rida < read && veerg < veerud)
                        {
                            istmeNupud[rida, veerg].BackColor = Color.Red;
                            istmeNupud[rida, veerg].Enabled = false;
                        }
                    }
                }
            }
        }

        private void IstmeNupp_Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            location = GetButtonIndices(button);

            if (button.BackColor == Color.LightBlue)
            {
                button.BackColor = Color.LightGreen;
                valitudIstmed.Add(location);

                Panel seatPanel = new Panel
                {
                    AutoSize = true,
                    Margin = new Padding(5)
                };

                Label seatLabel = new Label
                {
                    Text = $"Rida {location.X + 1}, Koht {location.Y + 1}:",
                    AutoSize = true
                };

                ComboBox ticketTypeCombo = CreateTicketTypeComboBox();
                ticketTypeCombo.Tag = location;
                ticketTypeCombo.SelectedIndexChanged += TicketTypeCombo_SelectedIndexChanged;

                seatPanel.Controls.Add(seatLabel);
                seatPanel.Controls.Add(ticketTypeCombo);
                ticketTypeCombo.Location = new Point(seatLabel.Width + 10, 0);

                flowLayoutPanel1.Controls.Add(seatPanel);

                if (ticketTypeCombo.Items.Count > 0)
                {
                    var selectedItem = (KeyValuePair<int, string>)ticketTypeCombo.SelectedItem;
                    valitudPiletituubid[location] = selectedItem.Key;
                }
            }
            else if (button.BackColor == Color.LightGreen)
            {
                button.BackColor = Color.LightBlue;
                valitudIstmed.Remove(location);
                valitudPiletituubid.Remove(location);

                List<Control> controlsToRemove = new List<Control>();
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is Panel panel)
                    {
                        foreach (Control panelControl in panel.Controls)
                        {
                            if (panelControl is ComboBox combo &&
                                combo.Tag is Point storedLocation &&
                                storedLocation.Equals(location))
                            {
                                controlsToRemove.Add(panel);
                                break;
                            }
                        }
                    }
                }

                foreach (Control control in controlsToRemove)
                {
                    flowLayoutPanel1.Controls.Remove(control);
                }
            }

            kinnitaNupp.Enabled = valitudIstmed.Count > 0;
        }

        private void TicketTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox combo && combo.Tag is Point location)
            {
                var selectedItem = (KeyValuePair<int, string>)combo.SelectedItem;
                valitudPiletituubid[location] = selectedItem.Key;
            }
        }

        private Point GetButtonIndices(Button button)
        {
            for (i = 0; i < read; i++)
            {
                for (j = 0; j < veerud; j++)
                {
                    if (istmeNupud[i, j] == button)
                    {
                        return new Point(i, j);
                    }
                }
            }
            return Point.Empty;
        }

        private void KinnitaNupp_Click(object sender, EventArgs e)
        {
            hind = 0;
            try
            {
                try { connection.Open(); } catch { }

                foreach (Point istekoht in valitudIstmed)
                {
                    using (command = new SqlCommand("INSERT INTO Piletid (Rida, Veerg, PiletituupID, KontoID, SeanssID) VALUES (@Rida, @Veerg, @PiletituupID, @KontoID, @SeanssID)", connection))
                    {
                        command.Parameters.AddWithValue("@Rida", istekoht.X + 1);
                        command.Parameters.AddWithValue("@Veerg", istekoht.Y + 1);
                        command.Parameters.AddWithValue("@PiletituupID", valitudPiletituubid[istekoht]);
                        command.Parameters.AddWithValue("@KontoID", Globals.kasutajaID);
                        command.Parameters.AddWithValue("@SeanssID", seanssID);
                        command.ExecuteNonQuery();
                        hind += GetTicketPrice(valitudPiletituubid[istekoht]);
                    }
                }
                MessageBox.Show($"Broneering õnnestus! Sinu piletite hinnad: {hind}€", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga broneeringul: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private decimal GetTicketPrice(int piletituupID)
        {
            decimal hind = 0;

            using (SqlConnection newConnection = new SqlConnection(connection.ConnectionString))
            {
                try
                {
                    newConnection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT Hind FROM Piletituubid WHERE ID = @ID", newConnection))
                    {
                        command.Parameters.AddWithValue("@ID", piletituupID);
                        var result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            hind = (decimal)result;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return hind;
        }
    }
}