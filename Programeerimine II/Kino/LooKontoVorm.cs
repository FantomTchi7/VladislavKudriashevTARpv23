using Microsoft.Data.SqlClient;

namespace Kino
{
    public partial class LooKontoVorm : Form
    {
        private TableLayoutPanel tabeliPaigutus1 = new TableLayoutPanel();
        private Label silt1 = new Label { Text = "Nimi:", AutoSize = true };
        private Label silt2 = new Label { Text = "Email:", AutoSize = true };
        private Label silt3 = new Label { Text = "Parool:", AutoSize = true };
        private Label silt4 = new Label { Text = "Sisselogimine", AutoSize = true };
        private TextBox tekstikast1 = new TextBox();
        private TextBox tekstikast2 = new TextBox();
        private TextBox tekstikast3 = new TextBox { PasswordChar = '*' };
        private Button nupp1 = new Button { Text = "Loo konto" };

        public LooKontoVorm()
        {
            InitializeComponent();
            suuruseMuutus();
            this.Text = "Loo konto";
            this.MinimumSize = new Size(275, 175);
            nupp1.Click += nupp1_Click;
            silt4.Click += silt4_Click;
            silt4.MouseHover += silt4_MouseHover;
            silt4.MouseLeave += silt4_MouseLeave;

            tabeliPaigutus1.Controls.Add(silt1, 0, 0);
            tabeliPaigutus1.Controls.Add(tekstikast1, 1, 0);
            tabeliPaigutus1.Controls.Add(silt2, 0, 1);
            tabeliPaigutus1.Controls.Add(tekstikast2, 1, 1);
            tabeliPaigutus1.Controls.Add(silt3, 0, 2);
            tabeliPaigutus1.Controls.Add(tekstikast3, 1, 2);
            tabeliPaigutus1.Controls.Add(silt4, 0, 3);
            tabeliPaigutus1.Controls.Add(nupp1, 1, 3);
            tabeliPaigutus1.AutoSize = true;
            this.Controls.Add(tabeliPaigutus1);
            this.ClientSizeChanged += (sender, e) => suuruseMuutus();
        }

        private void nupp1_Click(object? sender, EventArgs e)
        {
            string nimi = tekstikast1.Text;
            string email = tekstikast2.Text;
            string parool = tekstikast3.Text;

            try
            {
                using (SqlConnection ühendus = Globaalsed.SaaÜhendus())
                {
                    SqlCommand käsk = new SqlCommand(
                        "INSERT INTO Kontod(Huudnimi, Email, Parool, Tuup) VALUES (@Nimi, @Email, @Parool, @Tuup)",
                        ühendus);
                    käsk.Parameters.AddWithValue("@Nimi", nimi);
                    käsk.Parameters.AddWithValue("@Email", email);
                    käsk.Parameters.AddWithValue("@Parool", parool);
                    käsk.Parameters.AddWithValue("@Tuup", "Kasutaja");

                    käsk.ExecuteNonQuery();
                    this.Hide();
                    Globaalsed.sisselogimineVorm.Show();
                }
            }
            catch (SqlException sqlE)
            {
                MessageBox.Show($"Viga andmebaasiga ühenduse loomisel: {sqlE.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void silt4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Globaalsed.sisselogimineVorm.Show();
        }

        private void silt4_MouseHover(object sender, EventArgs e)
        {
            silt4.Font = new Font(silt4.Font, FontStyle.Underline);
            silt4.ForeColor = Color.Blue;
        }

        private void silt4_MouseLeave(object sender, EventArgs e)
        {
            silt4.Font = new Font(silt4.Font, FontStyle.Regular);
            silt4.ForeColor = Color.Black;
        }

        private void suuruseMuutus()
        {
            tabeliPaigutus1.Left = (this.Width - tabeliPaigutus1.Width) / 2;
            tabeliPaigutus1.Top = (this.Height - tabeliPaigutus1.Height) / 2;
        }
    }
}