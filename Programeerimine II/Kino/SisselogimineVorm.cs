using Microsoft.Data.SqlClient;

namespace Kino
{
    public partial class SisselogimineVorm : Form
    {
        private TableLayoutPanel tabeliPaigutus1 = new TableLayoutPanel();
        private Label silt1 = new Label { Text = "Nimi või email:", AutoSize = true };
        private Label silt2 = new Label { Text = "Parool:", AutoSize = true };
        private TextBox tekstikast1 = new TextBox();
        private TextBox tekstikast2 = new TextBox { PasswordChar = '*' };
        private Button nupp1 = new Button { Text = "Logi sisse" };

        public SisselogimineVorm()
        {
            InitializeComponent();
            suuruseMuutus();
            this.Text = "Sisselogimine";
            this.MinimumSize = new Size(275, 175);
            nupp1.Click += nupp1_Click;

            tabeliPaigutus1.Controls.Add(silt1, 0, 0);
            tabeliPaigutus1.Controls.Add(tekstikast1, 1, 0);
            tabeliPaigutus1.Controls.Add(silt2, 0, 1);
            tabeliPaigutus1.Controls.Add(tekstikast2, 1, 1);
            tabeliPaigutus1.Controls.Add(nupp1, 0, 2);
            tabeliPaigutus1.SetColumnSpan(nupp1, 2);
            tabeliPaigutus1.AutoSize = true;
            this.Controls.Add(tabeliPaigutus1);
            this.ClientSizeChanged += (sender, e) => suuruseMuutus();
        }

        private void suuruseMuutus()
        {
            tabeliPaigutus1.Left = (this.Width - tabeliPaigutus1.Width) / 2;
            tabeliPaigutus1.Top = (this.Height - tabeliPaigutus1.Height) / 2;
        }

        private void nupp1_Click(object sender, EventArgs e)
        {
            string nimiVõiEmail = tekstikast1.Text;
            string parool = tekstikast2.Text;

            try
            {
                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    SqlCommand käsk = new SqlCommand(
                        "SELECT ID, Huudnimi, Tuup FROM Kontod WHERE (Huudnimi = @nimiVoiEmail OR Email = @nimiVoiEmail) AND Parool = @parool",
                        ühendus);
                    käsk.Parameters.AddWithValue("@nimiVoiEmail", nimiVõiEmail);
                    käsk.Parameters.AddWithValue("@parool", parool);

                    using (SqlDataReader lugeja = käsk.ExecuteReader())
                    {
                        if (lugeja.Read())
                        {
                            Globaalsed.kasutajaID = (int)lugeja["ID"];
                            Globaalsed.kasutajaNimi = lugeja["Huudnimi"].ToString();
                            Globaalsed.kasutajaTüüp = lugeja["Tuup"].ToString();

                            this.Hide();
                            Globaalsed.vaatamineVorm.UuendaAndmed();
                            Globaalsed.vaatamineVorm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Vale kasutajanimi/email või parool.", "Sisselogimine ebaõnnestus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException sqlE)
            {
                MessageBox.Show($"Viga andmebaasiga ühenduse loomisel: {sqlE.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}