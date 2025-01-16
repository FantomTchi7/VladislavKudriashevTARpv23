using System.Data;
using System.Globalization;
using Microsoft.Data.SqlClient;

namespace Kino
{
    public partial class VaatamineVorm : Form
    {
        private const int tabeliPaigutuseKõrgus = 30;
        private const int filmiPaneeliLaius = 400;
        private const int filmiPaneeliKõrgus = 200;
        private const int filmiPostriLaius = 200 * 2 / 3;
        private const int polster = 10;
        private const int marginaal = 10;

        private Dictionary<DateTime, List<Panel>> seansidKuupäevaJärgi = new Dictionary<DateTime, List<Panel>>();

        private TextInfo tekstInfo = new CultureInfo("et-EE").TextInfo;
        private CultureInfo kultuuriInfo = new CultureInfo("et-EE");

        private FlowLayoutPanel vooluPaigutus1 = new FlowLayoutPanel
        {
            AutoScroll = true,
            Top = tabeliPaigutuseKõrgus
        };
        private TableLayoutPanel tabeliPaigutus1 = new TableLayoutPanel
        {
            Height = tabeliPaigutuseKõrgus
        };
        private FlowLayoutPanel vooluPaigutusVasak = new FlowLayoutPanel
        {
            AutoSize = true,
            Padding = new Padding(0),
            Margin = new Padding(0)
        };
        private FlowLayoutPanel vooluPaigutusParem = new FlowLayoutPanel
        {
            AutoSize = true,
            Padding = new Padding(0),
            Margin = new Padding(0),
            Anchor = AnchorStyles.Right
        };
        private Button nupp1 = new Button
        {
            Text = "Kinokava"
        };
        private Button nupp2 = new Button
        {
            Text = "Filmid"
        };
        private Button nupp50 = new Button
        {
            Text = "Haldus"
        };
        private ComboBox rippmenüü98 = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        private Button nupp99 = new Button
        {
            Text = "Logi sisse"
        };

        private ComboBox rippmenüüTabel = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        private DataGridView andmeTabel1 = new DataGridView
        {
            Name = "andmeTabel1",
            Dock = DockStyle.Fill,
            AutoSize = true
        };

        public VaatamineVorm()
        {
            InitializeComponent();
            TäidaRippmenüü();
            Text = "Vaatamine";
            ClientSizeChanged += (_, _) => suuruseMuutus();

            tabeliPaigutus1 = LooNupuPaneel();
            Controls.Add(tabeliPaigutus1);
            Controls.Add(vooluPaigutus1);

            suuruseMuutus();
        }

        private void suuruseMuutus()
        {
            int kasutatavLaius = this.Width - SystemInformation.VerticalScrollBarWidth;
            tabeliPaigutus1.Width = kasutatavLaius;

            if (!vooluPaigutus1.Controls.ContainsKey("andmeTabel1"))
            {
                vooluPaigutus1.Width = kasutatavLaius;
                vooluPaigutus1.Height = this.Height - (tabeliPaigutuseKõrgus * 2) - (SystemInformation.HorizontalScrollBarHeight / 2);

                int juhtimisvahemik = SaaVeergudeArv(vooluPaigutus1) * (filmiPaneeliLaius + polster + marginaal);
                int vooluPaigutuseX = (vooluPaigutus1.Width - juhtimisvahemik) / 2;

                vooluPaigutus1.Width = this.Width - vooluPaigutuseX;
                vooluPaigutus1.Left = vooluPaigutuseX - SystemInformation.VerticalScrollBarWidth;
            }
            else
            {
                vooluPaigutus1.Left = 0;
            }
        }

        private int SaaVeergudeArv(FlowLayoutPanel flp)
        {
            if (flp.Controls.Count == 0)
                return 1;

            int veeruLaius = filmiPaneeliLaius + polster + marginaal;
            int saadavalLaius = flp.ClientRectangle.Width - flp.Padding.Horizontal;

            return Math.Max(saadavalLaius / veeruLaius, 1);
        }

        private void TäidaRippmenüü()
        {
            try
            {
                List<KeyValuePair<int, string>> kinoNimekiri;
                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    SqlCommand käsk = new SqlCommand("SELECT ID, Nimetus FROM Kinod;", ühendus);
                    using (SqlDataReader lugeja = käsk.ExecuteReader())
                    {
                        kinoNimekiri = new List<KeyValuePair<int, string>>();
                        while (lugeja.Read())
                        {
                            kinoNimekiri.Add(new KeyValuePair<int, string>((int)lugeja["ID"], lugeja["Nimetus"].ToString()));
                        }
                    }
                }

                rippmenüü98.DisplayMember = "Value";
                rippmenüü98.ValueMember = "Key";
                rippmenüü98.DataSource = kinoNimekiri;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Andmebaasi viga: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TableLayoutPanel LooNupuPaneel()
        {
            nupp1.Click += (_, _) => LaeSeansid();
            nupp2.Click += (_, _) => LaeFilmid();
            nupp50.Click += (_, _) => LaeHaldus();
            nupp99.Click += (_, _) => AvaSisselogimiseVorm();

            andmeTabel1.CellEndEdit += AndmeTabel1_CellEndEdit;
            andmeTabel1.UserDeletingRow += AndmeTabel1_UserDeletingRow;

            vooluPaigutusVasak.Controls.Add(nupp1);
            vooluPaigutusVasak.Controls.Add(nupp2);

            vooluPaigutusParem.Controls.Add(rippmenüü98);
            vooluPaigutusParem.Controls.Add(nupp99);

            tabeliPaigutus1.Controls.Add(vooluPaigutusVasak, 0, 0);
            tabeliPaigutus1.Controls.Add(vooluPaigutusParem, 1, 0);
            return tabeliPaigutus1;
        }

        private void LaeSeansid()
        {
            seansidKuupäevaJärgi.Clear();

            Täidapäring(
                "SELECT " +
                "Seanssid.ID AS SeanssID, Seanssid.Aeg, " +
                "Filmid.ID AS FilmID, Filmid.Nimetus AS FilmNimetus, Filmid.Kirjeldus, Filmid.Kestus, Filmid.Valjalaskeaeg, Filmid.Poster, " +
                "Keeled.Nimetus AS KeelNimetus, " +
                "Saalid.Nimetus AS SaalNimetus, Saalid.Tuup " +
                "FROM Seanssid " +
                "INNER JOIN Filmid ON Seanssid.FilmID = Filmid.ID " +
                "INNER JOIN Keeled ON Seanssid.KeelID = Keeled.ID " +
                "INNER JOIN Saalid ON Seanssid.SaalID = Saalid.ID " +
                "WHERE Saalid.KinoID = @KinoID " +
                "ORDER BY Seanssid.Aeg;",
                lugeja => LisaSeansiPaneel(lugeja), new SqlParameter("@KinoID", ((KeyValuePair<int, string>)rippmenüü98.SelectedItem).Key));

            for (int i = 0; i < 7; i++)
            {
                DateTime päev = DateTime.Today.AddDays(i);

                Label päevaPealkiri = new Label
                {
                    Text = tekstInfo.ToTitleCase(päev.ToString("dddd", kultuuriInfo) + ", " + päev.ToString("M", kultuuriInfo)),
                    Font = new Font("Arial", 16, FontStyle.Bold),
                    AutoSize = true,
                    Margin = new Padding(marginaal)
                };
                vooluPaigutus1.SetFlowBreak(päevaPealkiri, true);
                vooluPaigutus1.Controls.Add(päevaPealkiri);

                if (seansidKuupäevaJärgi.ContainsKey(päev))
                {
                    List<Panel> filmiPaneelid = seansidKuupäevaJärgi[päev];
                    for (int j = 0; j < filmiPaneelid.Count; j++)
                    {
                        if (j == filmiPaneelid.Count - 1)
                        {
                            vooluPaigutus1.SetFlowBreak(filmiPaneelid[j], true);
                        }
                        vooluPaigutus1.Controls.Add(filmiPaneelid[j]);
                    }
                }
                else
                {
                    Label poleSeansseSilt = new Label
                    {
                        Text = "Seansse pole saadaval.",
                        AutoSize = true,
                        Margin = new Padding(marginaal)
                    };
                    vooluPaigutus1.SetFlowBreak(poleSeansseSilt, true);
                    vooluPaigutus1.Controls.Add(poleSeansseSilt);
                }
            }
            suuruseMuutus();
        }

        private void LaeFilmid()
        {
            Täidapäring(
                "SELECT " +
                "Filmid.ID, " +
                "Filmid.Nimetus, " +
                "Filmid.Kirjeldus, " +
                "Filmid.Kestus, " +
                "Filmid.Valjalaskeaeg, " +
                "Filmid.Poster, " +
                "Rezissoorid.Taisnimi AS RezisoorNimi, " +
                "Zanrid.Nimetus AS ZanrNimetus, " +
                "Vanusepiirangud.Nimetus AS VanusepiirangNimetus " +
                "FROM Filmid " +
                "INNER JOIN Rezissoorid ON Filmid.RezissoorID = Rezissoorid.ID " +
                "INNER JOIN Zanrid ON Filmid.ZanrID = Zanrid.ID " +
                "INNER JOIN Vanusepiirangud ON Filmid.VanusepiirangID = Vanusepiirangud.ID;",
                lugeja => LisaFilmiPaneel(lugeja));
            suuruseMuutus();
        }

        private void Täidapäring(string päring, Action<SqlDataReader> töötaRida, params SqlParameter[] parameetrid)
        {
            try
            {
                vooluPaigutus1.Controls.Clear();
                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    SqlCommand käsk = new SqlCommand(päring, ühendus);

                    if (parameetrid != null)
                    {
                        käsk.Parameters.AddRange(parameetrid);
                    }

                    using (SqlDataReader lugeja = käsk.ExecuteReader())
                    {
                        while (lugeja.Read())
                        {
                            töötaRida(lugeja);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Andmebaasi viga: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LisaSeansiPaneel(SqlDataReader lugeja)
        {
            DateTime seansiKuupäev = Convert.ToDateTime(lugeja["Aeg"]).Date;

            if (!seansidKuupäevaJärgi.ContainsKey(seansiKuupäev))
            {
                seansidKuupäevaJärgi[seansiKuupäev] = new List<Panel>();
            }

            Panel filmiPaneel = LooFilmiPaneel(lugeja, includeScreeningInfo: true);
            seansidKuupäevaJärgi[seansiKuupäev].Add(filmiPaneel);
        }

        private void LisaFilmiPaneel(SqlDataReader lugeja)
        {
            Panel filmiPaneel = LooFilmiPaneel(lugeja, includeScreeningInfo: false);
            vooluPaigutus1.Controls.Add(filmiPaneel);
        }

        private Panel LooFilmiPaneel(SqlDataReader lugeja, bool includeScreeningInfo)
        {
            PictureBox poster = new PictureBox
            {
                Padding = new Padding(0),
                Margin = new Padding(0),
                Width = filmiPostriLaius,
                Height = filmiPaneeliKõrgus,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            try
            {
                poster.Image = BaitiMassiivPildiks((byte[])lugeja["Poster"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            FlowLayoutPanel infoPaneel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true
            };

            if (includeScreeningInfo)
            {
                Label pealkiriSilt = new Label
                {
                    Text = lugeja["FilmNimetus"].ToString(),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true
                };
                Label kirjeldusSilt = new Label
                {
                    Text = lugeja["Kirjeldus"].ToString(),
                    AutoEllipsis = true,
                    Width = 250,
                    Height = 50
                };
                Label kestusSilt = new Label
                {
                    Text = $"Kestus: {lugeja["Kestus"]} minutid",
                    AutoSize = true
                };
                Label keelSilt = new Label
                {
                    Text = $"Keel: {lugeja["KeelNimetus"]}",
                    AutoSize = true
                };
                Label saalSilt = new Label
                {
                    Text = $"Saal: {lugeja["SaalNimetus"]} ({lugeja["Tuup"]})",
                    AutoSize = true
                };
                Label aegSilt = new Label
                {
                    Text = $"Aeg: {Convert.ToDateTime(lugeja["Aeg"]).ToString("t", kultuuriInfo)}",
                    AutoSize = true
                };

                Button broneerimiseNupp = new Button
                {
                    Tag = (int)lugeja["SeanssID"],
                    Text = $"Broneerin"
                };

                broneerimiseNupp.Click += broneerimiseNupp_Click;

                infoPaneel.Controls.Add(pealkiriSilt);
                infoPaneel.Controls.Add(kirjeldusSilt);
                infoPaneel.Controls.Add(kestusSilt);
                infoPaneel.Controls.Add(keelSilt);
                infoPaneel.Controls.Add(saalSilt);
                infoPaneel.Controls.Add(aegSilt);
                infoPaneel.Controls.Add(broneerimiseNupp);
            }
            else
            {
                Label pealkiriSilt = new Label
                {
                    Text = lugeja["Nimetus"].ToString(),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true
                };
                Label kirjeldusSilt = new Label
                {
                    Text = lugeja["Kirjeldus"].ToString(),
                    AutoEllipsis = true,
                    Width = 250,
                    Height = 50
                };
                Label väljalaskeKuupäevSilt = new Label
                {
                    Text = $"Esilinastus: {Convert.ToDateTime(lugeja["Valjalaskeaeg"]).ToString("d", kultuuriInfo)}",
                    AutoSize = true
                };
                Label kestusSilt = new Label
                {
                    Text = $"Kestus: {lugeja["Kestus"]} minutid",
                    AutoSize = true
                };
                Label režissöörSilt = new Label
                {
                    Text = $"Režissöör: {lugeja["RezisoorNimi"]}",
                    AutoSize = true
                };
                Label žanrSilt = new Label
                {
                    Text = $"Žanr: {lugeja["ZanrNimetus"]}",
                    AutoSize = true
                };
                Label hinnangSilt = new Label
                {
                    Text = $"Vanusepiirang: {lugeja["VanusepiirangNimetus"]}",
                    AutoSize = true
                };

                infoPaneel.Controls.Add(pealkiriSilt);
                infoPaneel.Controls.Add(kirjeldusSilt);
                infoPaneel.Controls.Add(väljalaskeKuupäevSilt);
                infoPaneel.Controls.Add(kestusSilt);
                infoPaneel.Controls.Add(režissöörSilt);
                infoPaneel.Controls.Add(žanrSilt);
                infoPaneel.Controls.Add(hinnangSilt);
            }

            Panel filmiPaneel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = filmiPaneeliLaius,
                Height = filmiPaneeliKõrgus,
                Margin = new Padding(marginaal)
            };

            FlowLayoutPanel peaminePaneel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };

            peaminePaneel.Controls.Add(poster);
            peaminePaneel.Controls.Add(infoPaneel);

            filmiPaneel.Controls.Add(peaminePaneel);
            return filmiPaneel;
        }

        private void broneerimiseNupp_Click(object sender, EventArgs e)
        {
            Globaalsed.broneerimineVorm = new BroneerimineVorm((int)((Button)sender).Tag);
            Globaalsed.broneerimineVorm.ShowDialog();
        }

        private Image BaitiMassiivPildiks(byte[] baitiMassiiv)
        {
            MemoryStream ms = new MemoryStream(baitiMassiiv);
            return Image.FromStream(ms);
        }

        private void LaeHaldus()
        {
            vooluPaigutus1.Controls.Clear();

            TäidaRippmenüüTabel();

            rippmenüüTabel.SelectedIndexChanged += (sender, e) => {
                vooluPaigutus1.Controls.Remove(andmeTabel1);
                LaeAndmeTabel($"SELECT * FROM {rippmenüüTabel.SelectedItem.ToString()}");
                vooluPaigutus1.Controls.Add(andmeTabel1);
            };

            vooluPaigutus1.Controls.Add(rippmenüüTabel);
            vooluPaigutus1.Controls.Add(andmeTabel1);
        }

        private void TäidaRippmenüüTabel()
        {
            try
            {
                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    DataTable skeemiTabel = ühendus.GetSchema("Tables");
                    List<string> tabeliNimed = new List<string>();

                    foreach (DataRow rida in skeemiTabel.Rows)
                    {
                        tabeliNimed.Add(rida["TABLE_NAME"].ToString());
                    }

                    rippmenüüTabel.DataSource = tabeliNimed;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Andmebaasi viga: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LaeAndmeTabel(string päring)
        {
            try
            {
                andmeTabel1.DataSource = null;
                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(päring, ühendus);
                    DataTable tabel = new DataTable();
                    adapter.Fill(tabel);

                    andmeTabel1.DataSource = tabel;

                    andmeTabel1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    andmeTabel1.ReadOnly = false;
                    andmeTabel1.AllowUserToAddRows = true;
                    andmeTabel1.AllowUserToDeleteRows = true;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Andmebaasi viga: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AndmeTabel1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (andmeTabel1.DataSource is DataTable tabel)
                {
                    DataRow rida = tabel.Rows[e.RowIndex];
                    string veeruNimi = tabel.Columns[e.ColumnIndex].ColumnName;
                    object uusVäärtus = rida[veeruNimi];

                    string päring = $"UPDATE {rippmenüüTabel.SelectedItem} SET {veeruNimi} = @Väärtus WHERE ID = @ID";

                    using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                    {
                        using SqlCommand uuendaKäsk = new SqlCommand(päring, ühendus);
                        uuendaKäsk.Parameters.AddWithValue("@Väärtus", uusVäärtus);
                        uuendaKäsk.Parameters.AddWithValue("@ID", rida["ID"]);

                        int mõjutatudRead = uuendaKäsk.ExecuteNonQuery();
                        if (mõjutatudRead == 0)
                        {
                            MessageBox.Show("Ühtegi rida ei uuendatud. Palun kontrollige primaarvõtit või piiranguid.", "Hoiatus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Viga andmebaasi uuendamisel: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AndmeTabel1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                string päring = $"DELETE FROM {rippmenüüTabel.SelectedItem} WHERE ID = @ID";

                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    using SqlCommand kustutaKäsk = new SqlCommand(päring, ühendus);
                    kustutaKäsk.Parameters.AddWithValue("@ID", e.Row.Cells["ID"].Value);

                    kustutaKäsk.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Viga rea kustutamisel: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void AvaSisselogimiseVorm()
        {
            Hide();
            Globaalsed.sisselogimineVorm?.Show();
        }

        public void UuendaAndmed()
        {
            nupp99.Text = Globaalsed.kasutajaNimi;

            if (Globaalsed.kasutajaTüüp != "Admin" && vooluPaigutusVasak.Controls.Contains(nupp50))
            {
                vooluPaigutusVasak.Controls.Remove(nupp50);

                if (vooluPaigutus1.Controls.Contains(andmeTabel1))
                {
                    vooluPaigutus1.Controls.Clear();
                    LaeSeansid();
                }
            }
            else if (Globaalsed.kasutajaTüüp == "Admin" && !vooluPaigutusVasak.Controls.Contains(nupp50))
            {
                vooluPaigutusVasak.Controls.Add(nupp50);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}