using Microsoft.Data.SqlClient;

namespace Kino
{
    public partial class BroneerimineVorm : Form
    {
        private FlowLayoutPanel vooluPaigutus1 = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            Dock = DockStyle.Fill,
            WrapContents = false,
            AutoSize = true
        };
        private FlowLayoutPanel vooluPaigutus2 = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            Dock = DockStyle.Fill,
            AutoSize = true
        };
        private Dictionary<Point, int> valitudPiletitüübid = new Dictionary<Point, int>();
        private Label infoSilt = new Label
        {
            AutoSize = true,
            Text = "Vali istekohad. Sinine - vaba, Punane - hõivatud, Roheline - valitud",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        private Button kinnitaNupp = new Button
        {
            Text = "Kinnita broneering",
            AutoSize = true,
            Dock = DockStyle.Fill,
            Enabled = false
        };
        private List<Point> valitudIstmed = new List<Point>();

        private int read;
        private int veerud;
        private Button[,] istmeNupud;

        private readonly int seanssID;

        public BroneerimineVorm(int seanssID)
        {
            InitializeComponent();
            this.seanssID = seanssID;

            this.Text = "Istekoha broneerimine";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            kinnitaNupp.Click += KinnitaNupp_Click;

            this.Controls.Add(vooluPaigutus1);
            vooluPaigutus1.Controls.Add(infoSilt);

            try
            {
                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    using (SqlCommand käsk = new SqlCommand("SELECT SaalRead, SaalVeerud FROM Saalid s JOIN Seanssid ss ON s.ID = ss.SaalID WHERE ss.ID = @SeanssID", ühendus))
                    {
                        käsk.Parameters.AddWithValue("@SeanssID", seanssID);
                        using (SqlDataReader lugeja = käsk.ExecuteReader())
                        {
                            if (lugeja.Read())
                            {
                                read = lugeja.GetInt32(0);
                                veerud = lugeja.GetInt32(1);
                            }
                        }
                    }

                    TableLayoutPanel isteTabel = LooIstekohaNupud();
                    vooluPaigutus1.Controls.Add(isteTabel);
                    vooluPaigutus1.Controls.Add(vooluPaigutus2);
                    LaeIstekohaStaatus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            vooluPaigutus1.Controls.Add(kinnitaNupp);
        }

        private ComboBox LooPiletitüübiRippmenüü()
        {
            ComboBox rippmenüü = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            List<KeyValuePair<int, string>> piletitüübid = new List<KeyValuePair<int, string>>();

            try
            {
                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    SqlCommand käsk = new SqlCommand("SELECT ID, Tuup FROM Piletituubid;", ühendus);
                    using (SqlDataReader lugeja = käsk.ExecuteReader())
                    {
                        while (lugeja.Read())
                        {
                            piletitüübid.Add(new KeyValuePair<int, string>(
                                (int)lugeja["ID"],
                                lugeja["Tuup"].ToString()
                            ));
                        }
                    }
                }

                rippmenüü.DisplayMember = "Value";
                rippmenüü.ValueMember = "Key";
                rippmenüü.DataSource = new List<KeyValuePair<int, string>>(piletitüübid);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Andmebaasi viga: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return rippmenüü;
        }

        private TableLayoutPanel LooIstekohaNupud()
        {
            TableLayoutPanel isteTabel = new TableLayoutPanel
            {
                Name = "isteTabel",
                ColumnCount = veerud,
                RowCount = read,
                Dock = DockStyle.Fill,
                AutoSize = true
            };

            for (int veerg = 0; veerg < veerud; veerg++)
            {
                isteTabel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            }
            for (int rida = 0; rida < read; rida++)
            {
                isteTabel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            istmeNupud = new Button[read, veerud];
            int nupuSuurus = 40;

            for (int i = 0; i < read; i++)
            {
                for (int j = 0; j < veerud; j++)
                {
                    Button nupp = new Button
                    {
                        Text = $"{i + 1}-{j + 1}",
                        BackColor = Color.LightBlue,
                        Width = nupuSuurus,
                        Height = nupuSuurus
                    };

                    nupp.Click += IstmeNupp_Click;
                    istmeNupud[i, j] = nupp;

                    isteTabel.Controls.Add(nupp, j, i);
                }
            }

            return isteTabel;
        }

        private void LaeIstekohaStaatus()
        {
            string päring = @"
                SELECT Rida, Veerg
                FROM Piletid
                WHERE SeanssID = @SeanssID";

            using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
            {
                using (SqlCommand käsk = new SqlCommand(päring, ühendus))
                {
                    käsk.Parameters.AddWithValue("@SeanssID", seanssID);

                    using (SqlDataReader lugeja = käsk.ExecuteReader())
                    {
                        while (lugeja.Read())
                        {
                            int rida = lugeja.GetInt32(0) - 1;
                            int veerg = lugeja.GetInt32(1) - 1;

                            if (rida < read && veerg < veerud)
                            {
                                istmeNupud[rida, veerg].BackColor = Color.Red;
                                istmeNupud[rida, veerg].Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        private void IstmeNupp_Click(object sender, EventArgs e)
        {
            Button nupp = (Button)sender;
            Point asukoht = SaaNupuIndeksid(nupp);

            if (nupp.BackColor == Color.LightBlue)
            {
                nupp.BackColor = Color.LightGreen;
                valitudIstmed.Add(asukoht);

                TableLayoutPanel istePaneel = new TableLayoutPanel
                {
                    ColumnCount = 2,
                    RowCount = 1,
                    Dock = DockStyle.Fill,
                    AutoSize = true
                };

                Label isteSilt = new Label
                {
                    Text = $"Rida {asukoht.X + 1}, Koht {asukoht.Y + 1}:",
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleRight,
                    Dock = DockStyle.Right
                };

                ComboBox piletitüübiRippmenüü = LooPiletitüübiRippmenüü();
                piletitüübiRippmenüü.Dock = DockStyle.Left;
                piletitüübiRippmenüü.Tag = asukoht;
                piletitüübiRippmenüü.SelectedIndexChanged += PiletitüübiRippmenüü_SelectedIndexChanged;

                istePaneel.Controls.Add(isteSilt, 0, 0);
                istePaneel.Controls.Add(piletitüübiRippmenüü, 1, 0);

                istePaneel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64));
                istePaneel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));

                vooluPaigutus2.Controls.Add(istePaneel);

                if (piletitüübiRippmenüü.Items.Count > 0)
                {
                    KeyValuePair<int, string> valitudElement = (KeyValuePair<int, string>)piletitüübiRippmenüü.SelectedItem;
                    valitudPiletitüübid[asukoht] = valitudElement.Key;
                }
            }
            else if (nupp.BackColor == Color.LightGreen)
            {
                nupp.BackColor = Color.LightBlue;
                valitudIstmed.Remove(asukoht);
                valitudPiletitüübid.Remove(asukoht);

                List<Control> eemaldatavadKontrollid = new List<Control>();
                foreach (Control kontroll in vooluPaigutus2.Controls)
                {
                    if (kontroll is Panel paneel)
                    {
                        foreach (Control paneeliKontroll in paneel.Controls)
                        {
                            if (paneeliKontroll is ComboBox rippmenüü &&
                                rippmenüü.Tag is Point salvestatudAsukoht &&
                                salvestatudAsukoht.Equals(asukoht))
                            {
                                eemaldatavadKontrollid.Add(paneel);
                                break;
                            }
                        }
                    }
                }

                foreach (Control kontroll in eemaldatavadKontrollid)
                {
                    vooluPaigutus2.Controls.Remove(kontroll);
                }
            }

            kinnitaNupp.Enabled = valitudIstmed.Count > 0;
        }

        private void PiletitüübiRippmenüü_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox rippmenüü && rippmenüü.Tag is Point asukoht)
            {
                KeyValuePair<int, string> valitudElement = (KeyValuePair<int, string>)rippmenüü.SelectedItem;
                valitudPiletitüübid[asukoht] = valitudElement.Key;
            }
        }

        private Point SaaNupuIndeksid(Button nupp)
        {
            for (int i = 0; i < read; i++)
            {
                for (int j = 0; j < veerud; j++)
                {
                    if (istmeNupud[i, j] == nupp)
                    {
                        return new Point(i, j);
                    }
                }
            }
            return Point.Empty;
        }

        private void KinnitaNupp_Click(object sender, EventArgs e)
        {
            decimal hind = 0;
            try
            {
                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    foreach (Point istekoht in valitudIstmed)
                    {
                        using (SqlCommand käsk = new SqlCommand("INSERT INTO Piletid (Rida, Veerg, PiletituupID, KontoID, SeanssID) VALUES (@Rida, @Veerg, @PiletituupID, @KontoID, @SeanssID)", ühendus))
                        {
                            käsk.Parameters.AddWithValue("@Rida", istekoht.X + 1);
                            käsk.Parameters.AddWithValue("@Veerg", istekoht.Y + 1);
                            käsk.Parameters.AddWithValue("@PiletituupID", valitudPiletitüübid[istekoht]);
                            käsk.Parameters.AddWithValue("@KontoID", Globaalsed.kasutajaID);
                            käsk.Parameters.AddWithValue("@SeanssID", seanssID);
                            käsk.ExecuteNonQuery();
                            hind += SaaPiletihind(valitudPiletitüübid[istekoht]);
                        }
                    }
                }
                MessageBox.Show($"Broneering õnnestus! Sinu piletite hinnad: {hind}€", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga broneeringul: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal SaaPiletihind(int piletitüüpID)
        {
            decimal hind = 0;
            using (SqlConnection uusÜhendus = new SqlConnection(Globaalsed.SaaÜhendus().ConnectionString))
            {
                try
                {
                    uusÜhendus.Open();

                    using (SqlCommand käsk = new SqlCommand("SELECT Hind FROM Piletituubid WHERE ID = @ID", uusÜhendus))
                    {
                        käsk.Parameters.AddWithValue("@ID", piletitüüpID);
                        object tulemus = käsk.ExecuteScalar();
                        if (tulemus != null && tulemus != DBNull.Value)
                        {
                            hind = (decimal)tulemus;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Andmebaasi viga: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return hind;
        }
    }
}