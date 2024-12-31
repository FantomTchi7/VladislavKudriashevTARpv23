using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WinForms
{
    public partial class NeljasVorm : Form
    {
        // Use this Random object to choose random icons for the squares
        Random random = new Random();

        // Each of these letters is an interesting icon
        // in the Webdings font,
        // and each icon appears twice in this list
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        // firstClicked points to the first Label control 
        // that the player clicks, but it will be null 
        // if the player hasn't clicked a label yet
        Label firstClicked = null;

        // secondClicked points to the second Label control 
        // that the player clicks
        Label secondClicked = null;

        private Timer timer1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;

        private Timer timer2;
        private Label labelTimer;
        private int timerInt;

        private TableLayoutPanel tableLayoutPanel2;
        private Label memoryLabel;
        public NeljasVorm(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Text = "Matching Game";

            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.BackColor = Color.CornflowerBlue;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

            label1 = new Label();
            label1.BackColor = Color.CornflowerBlue;
            label1.AutoSize = false;
            label1.Dock = DockStyle.Fill;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Font = new Font("Webdings", 48F);
            label1.Text = "c";
            label1.Click += label1_Click;

            label2 = new Label();
            label2.BackColor = Color.CornflowerBlue;
            label2.AutoSize = false;
            label2.Dock = DockStyle.Fill;
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Font = new Font("Webdings", 48F);
            label2.Text = "c";
            label2.Click += label1_Click;

            label3 = new Label();
            label3.BackColor = Color.CornflowerBlue;
            label3.AutoSize = false;
            label3.Dock = DockStyle.Fill;
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Font = new Font("Webdings", 48F);
            label3.Text = "c";
            label3.Click += label1_Click;

            label4 = new Label();
            label4.BackColor = Color.CornflowerBlue;
            label4.AutoSize = false;
            label4.Dock = DockStyle.Fill;
            label4.TextAlign = ContentAlignment.MiddleCenter;
            label4.Font = new Font("Webdings", 48F);
            label4.Text = "c";
            label4.Click += label1_Click;

            label5 = new Label();
            label5.BackColor = Color.CornflowerBlue;
            label5.AutoSize = false;
            label5.Dock = DockStyle.Fill;
            label5.TextAlign = ContentAlignment.MiddleCenter;
            label5.Font = new Font("Webdings", 48F);
            label5.Text = "c";
            label5.Click += label1_Click;

            label6 = new Label();
            label6.BackColor = Color.CornflowerBlue;
            label6.AutoSize = false;
            label6.Dock = DockStyle.Fill;
            label6.TextAlign = ContentAlignment.MiddleCenter;
            label6.Font = new Font("Webdings", 48F);
            label6.Text = "c";
            label6.Click += label1_Click;

            label7 = new Label();
            label7.BackColor = Color.CornflowerBlue;
            label7.AutoSize = false;
            label7.Dock = DockStyle.Fill;
            label7.TextAlign = ContentAlignment.MiddleCenter;
            label7.Font = new Font("Webdings", 48F);
            label7.Text = "c";
            label7.Click += label1_Click;

            label8 = new Label();
            label8.BackColor = Color.CornflowerBlue;
            label8.AutoSize = false;
            label8.Dock = DockStyle.Fill;
            label8.TextAlign = ContentAlignment.MiddleCenter;
            label8.Font = new Font("Webdings", 48F);
            label8.Text = "c";
            label8.Click += label1_Click;

            label9 = new Label();
            label9.BackColor = Color.CornflowerBlue;
            label9.AutoSize = false;
            label9.Dock = DockStyle.Fill;
            label9.TextAlign = ContentAlignment.MiddleCenter;
            label9.Font = new Font("Webdings", 48F);
            label9.Text = "c";
            label9.Click += label1_Click;

            label10 = new Label();
            label10.BackColor = Color.CornflowerBlue;
            label10.AutoSize = false;
            label10.Dock = DockStyle.Fill;
            label10.TextAlign = ContentAlignment.MiddleCenter;
            label10.Font = new Font("Webdings", 48F);
            label10.Text = "c";
            label10.Click += label1_Click;

            label11 = new Label();
            label11.BackColor = Color.CornflowerBlue;
            label11.AutoSize = false;
            label11.Dock = DockStyle.Fill;
            label11.TextAlign = ContentAlignment.MiddleCenter;
            label11.Font = new Font("Webdings", 48F);
            label11.Text = "c";
            label11.Click += label1_Click;

            label12 = new Label();
            label12.BackColor = Color.CornflowerBlue;
            label12.AutoSize = false;
            label12.Dock = DockStyle.Fill;
            label12.TextAlign = ContentAlignment.MiddleCenter;
            label12.Font = new Font("Webdings", 48F);
            label12.Text = "c";
            label12.Click += label1_Click;

            label13 = new Label();
            label13.BackColor = Color.CornflowerBlue;
            label13.AutoSize = false;
            label13.Dock = DockStyle.Fill;
            label13.TextAlign = ContentAlignment.MiddleCenter;
            label13.Font = new Font("Webdings", 48F);
            label13.Text = "c";
            label13.Click += label1_Click;

            label14 = new Label();
            label14.BackColor = Color.CornflowerBlue;
            label14.AutoSize = false;
            label14.Dock = DockStyle.Fill;
            label14.TextAlign = ContentAlignment.MiddleCenter;
            label14.Font = new Font("Webdings", 48F);
            label14.Text = "c";
            label14.Click += label1_Click;

            label15 = new Label();
            label15.BackColor = Color.CornflowerBlue;
            label15.AutoSize = false;
            label15.Dock = DockStyle.Fill;
            label15.TextAlign = ContentAlignment.MiddleCenter;
            label15.Font = new Font("Webdings", 48F);
            label15.Text = "c";
            label15.Click += label1_Click;

            label16 = new Label();
            label16.BackColor = Color.CornflowerBlue;
            label16.AutoSize = false;
            label16.Dock = DockStyle.Fill;
            label16.TextAlign = ContentAlignment.MiddleCenter;
            label16.Font = new Font("Webdings", 48F);
            label16.Text = "c";
            label16.Click += label1_Click;

            timer1 = new Timer();
            timer1.Interval = 750;
            timer1.Tick += timer1_Tick;

            timer2 = new Timer();
            timer2.Interval = 1000;
            timer2.Tick += timer2_Tick;
            timer2.Start();

            labelTimer = new Label();
            labelTimer.AutoSize = true;
            labelTimer.Font = new Font(labelTimer.Font.FontFamily, 18f);
            labelTimer.TextAlign = ContentAlignment.MiddleLeft;
            labelTimer.Anchor = AnchorStyles.Left;

            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel2.BackColor = Color.CornflowerBlue;
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));

            memoryLabel = new Label();
            memoryLabel.AutoSize = true;
            memoryLabel.Font = new Font("Webdings", 18F);
            memoryLabel.TextAlign = ContentAlignment.MiddleRight;
            memoryLabel.Anchor = AnchorStyles.Right;

            InitializeControls();
            AssignIconsToSquares();
        }
        private void AssignIconsToSquares()
        {
            // The TableLayoutPanel has 16 labels,
            // and the icon list has 16 icons,
            // so an icon is pulled at random from the list
            // and added to each label
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            // The timer is only on after two non-matching 
            // icons have been shown to the player, 
            // so ignore any clicks if the timer is running
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If the clicked label is black, the player clicked
                // an icon that's already been revealed --
                // ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // If firstClicked is null, this is the first icon
                // in the pair that the player clicked, 
                // so set firstClicked to the label that the player 
                // clicked, change its color to black, and return
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // If the player gets this far, the timer isn't
                // running and firstClicked isn't null, 
                // so this must be the second icon the player clicked
                // Set its color to black
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Check to see if the player won
                CheckForWinner();

                // If the player clicked two matching icons, keep them 
                // black and reset firstClicked and secondClicked 
                // so the player can click another icon
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                else
                {
                    if (10 == memoryLabel.Text.Length)
                    {
                        memoryLabel.Text = firstClicked.Text + secondClicked.Text;
                    }
                    else
                    {
                        memoryLabel.Text += firstClicked.Text + secondClicked.Text;
                    }
                }

                // If the player gets this far, the player 
                // clicked two different icons, so start the 
                // timer (which will wait three quarters of 
                // a second, and then hide the icons)
                timer1.Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            timer1.Stop();

            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset firstClicked and secondClicked 
            // so the next time a label is
            // clicked, the program knows it's the first click
            firstClicked = null;
            secondClicked = null;
        }
        private void CheckForWinner()
        {
            // Go through all of the labels in the TableLayoutPanel, 
            // checking each one to see if its icon is matched
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // If the loop didn’t return, it didn't find
            // any unmatched icons
            // That means the user won. Show a message and close the form
            timer2.Stop();
            MessageBox.Show($"You matched all the icons in {timerInt} seconds!", "Congratulations");
            Close();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            timerInt++;
            if (timerInt == 1)
            {
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    Label iconLabel = control as Label;
                    if (iconLabel != null)
                    {
                        iconLabel.ForeColor = iconLabel.BackColor;
                    }
                }
            }
            labelTimer.Text = $"Elapsed: {timerInt} seconds";
        }
        private void InitializeControls()
        {
            Controls.Add(tableLayoutPanel2);
            tableLayoutPanel2.Controls.Add(labelTimer, 0, 0);
            tableLayoutPanel2.Controls.Add(memoryLabel, 1, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 1);
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel1, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 2, 0);
            tableLayoutPanel1.Controls.Add(label4, 3, 0);
            tableLayoutPanel1.Controls.Add(label5, 0, 1);
            tableLayoutPanel1.Controls.Add(label6, 1, 1);
            tableLayoutPanel1.Controls.Add(label7, 2, 1);
            tableLayoutPanel1.Controls.Add(label8, 3, 1);
            tableLayoutPanel1.Controls.Add(label9, 0, 2);
            tableLayoutPanel1.Controls.Add(label10, 1, 2);
            tableLayoutPanel1.Controls.Add(label11, 2, 2);
            tableLayoutPanel1.Controls.Add(label12, 3, 2);
            tableLayoutPanel1.Controls.Add(label13, 0, 3);
            tableLayoutPanel1.Controls.Add(label14, 1, 3);
            tableLayoutPanel1.Controls.Add(label15, 2, 3);
            tableLayoutPanel1.Controls.Add(label16, 3, 3);
        }
    }
}
