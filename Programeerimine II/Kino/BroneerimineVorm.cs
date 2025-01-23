using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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

                    SaadaPiletiEmail(valitudIstmed, valitudPiletitüübid, seanssID);
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
                finally
                {
                    uusÜhendus.Close();
                    uusÜhendus.Dispose();
                }
            }
            return hind;
        }

        private void SaadaPiletiEmail(List<Point> istmed, Dictionary<Point, int> piletitüübid, int seanssID)
        {
            try
            {
                string filmNimetus = string.Empty;
                string žanr = string.Empty;
                string rezissöör = string.Empty;
                int filmKestus = 0;
                string vanusepiirang = string.Empty;
                DateTime seansAeg = DateTime.MinValue;
                string saal = string.Empty;
                string keel = string.Empty;
                string kirjeldus = string.Empty;
                byte[] posterAndmed = null;

                decimal koguHind = 0;

                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    using (SqlCommand käsk = new SqlCommand(@"
                        SELECT 
                            f.Nimetus, z.Nimetus as Zanr, r.Taisnimi as Rezissoor, 
                            f.Kestus, v.Nimetus as Vanusepiirang, 
                            s.Aeg as SeansAeg, sl.Nimetus as Saal, k.Nimetus as Keel, 
                            f.Kirjeldus, 
                            f.Poster 
                        FROM Seanssid s
                        JOIN Filmid f ON s.FilmID = f.ID
                        JOIN Rezissoorid r ON f.RezissoorID = r.ID
                        JOIN Zanrid z ON f.ZanrID = z.ID
                        JOIN Vanusepiirangud v ON f.VanusepiirangID = v.ID
                        JOIN Keeled k ON s.KeelID = k.ID
                        JOIN Saalid sl ON s.SaalID = sl.ID
                        WHERE s.ID = @SeanssID", ühendus))
                    {
                        käsk.Parameters.AddWithValue("@SeanssID", seanssID);
                        using (SqlDataReader lugeja = käsk.ExecuteReader())
                        {
                            if (lugeja.Read())
                            {
                                filmNimetus = lugeja["Nimetus"].ToString();
                                žanr = lugeja["Zanr"].ToString();
                                rezissöör = lugeja["Rezissoor"].ToString();
                                filmKestus = (int)lugeja["Kestus"];
                                vanusepiirang = lugeja["Vanusepiirang"].ToString();
                                seansAeg = (DateTime)lugeja["SeansAeg"];
                                saal = lugeja["Saal"].ToString();
                                keel = lugeja["Keel"].ToString();
                                kirjeldus = lugeja["Kirjeldus"].ToString();

                                if (lugeja["Poster"] != DBNull.Value)
                                {
                                    posterAndmed = (byte[])lugeja["Poster"];
                                }
                            }
                        }
                    }
                }

                StringBuilder emailHtml = new StringBuilder();
                emailHtml.AppendLine("<html>");
                emailHtml.AppendLine("<head>");
                emailHtml.AppendLine("<style>");
                emailHtml.AppendLine("body { font-family: Arial, sans-serif; line-height: 1.6; margin: 0; padding: 0; }");
                emailHtml.AppendLine(".ticket-container { display: inline-flex; flex-direction: row; margin: 10px; padding: 0; border: 1px solid black; width: 400px; height: 200px; }");
                emailHtml.AppendLine(".poster { margin-right: 10px; }");
                emailHtml.AppendLine(".poster img { height: 200px; width: 120px; }");
                emailHtml.AppendLine(".details { display: block; justify-content: center; word-wrap: break-word; }");
                emailHtml.AppendLine(".details h3 { margin: 0; font-size: 18px; color: #333; }");
                emailHtml.AppendLine(".details p { margin: 0; font-size: 14px; white-space: normal; }");
                emailHtml.AppendLine("</style>");
                emailHtml.AppendLine("</head>");
                emailHtml.AppendLine("<body>");
                emailHtml.AppendLine("<h2 style='text-align: center;'>Teie kinokülastuse kinnitus</h2>");

                foreach (Point istekoht in istmed)
                {
                    string piletitüüp = string.Empty;
                    decimal hind = 0;

                    using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                    {
                        using (SqlCommand käsk = new SqlCommand("SELECT Tuup, Hind FROM Piletituubid WHERE ID = @ID", ühendus))
                        {
                            käsk.Parameters.AddWithValue("@ID", piletitüübid[istekoht]);
                            using (SqlDataReader lugeja = käsk.ExecuteReader())
                            {
                                if (lugeja.Read())
                                {
                                    piletitüüp = lugeja["Tuup"].ToString();
                                    hind = (decimal)lugeja["Hind"];
                                    koguHind += hind;
                                }
                            }
                        }
                    }

                    emailHtml.AppendLine("<div class='ticket-container'>");

                    emailHtml.AppendLine("<div class='poster'>");
                    emailHtml.AppendLine($"<img src=\"cid:posterImage\" alt='Poster'>");
                    emailHtml.AppendLine("</div>");

                    emailHtml.AppendLine("<div class='details'>");
                    emailHtml.AppendLine($"<h3>{filmNimetus}</h3>");
                    emailHtml.AppendLine($"<p>{kirjeldus}</p><br>");
                    emailHtml.AppendLine($"<p>Žanr: {žanr} ({vanusepiirang})</p>");
                    emailHtml.AppendLine($"<p>Keel: {keel}</p>");
                    emailHtml.AppendLine($"<p>Saal: {saal} ({istekoht.X + 1} rida, {istekoht.Y + 1} koht)</p>");
                    emailHtml.AppendLine($"<p>Aeg: {seansAeg:HH:mm}-{seansAeg.Add(new TimeSpan(0, filmKestus, 0)):HH:mm} (Kestus {filmKestus} min)</p>");
                    emailHtml.AppendLine($"<p>Piletitüüp: {piletitüüp} ({hind}€)</p>");
                    emailHtml.AppendLine("</div>");

                    emailHtml.AppendLine("</div>");
                }

                emailHtml.AppendLine("<h3 style='text-align: center;'>Kokku Hind: " + koguHind.ToString() + "€</h3>");
                emailHtml.AppendLine("<p style='text-align: center;'>Meeldivat filmielamust!</p>");
                emailHtml.AppendLine("<p style='text-align: center; color: #888;'>NB! See on automaatselt genereeritud kiri, palun ärge vastake sellele.</p>");
                emailHtml.AppendLine("</body>");
                emailHtml.AppendLine("</html>");

                MailMessage email = new MailMessage();
                email.From = new MailAddress("othermodstactics@gmail.com", "Kino");
                email.To.Add(Globaalsed.kasutajaEmail);
                email.Subject = $"Kinokülastuse kinnitus - {filmNimetus}";

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailHtml.ToString(), null, MediaTypeNames.Text.Html);

                if (posterAndmed != null)
                {
                    LinkedResource posterResource = new LinkedResource(new MemoryStream(posterAndmed), MediaTypeNames.Image.Jpeg)
                    {
                        ContentId = "posterImage"
                    };
                    htmlView.LinkedResources.Add(posterResource);
                }

                email.AlternateViews.Add(htmlView);

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("othermodstactics@gmail.com", "uzct ocxw uyte abyr"),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Timeout = 5000
                };

                smtp.Send(email);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga e-maili saatmisel: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}