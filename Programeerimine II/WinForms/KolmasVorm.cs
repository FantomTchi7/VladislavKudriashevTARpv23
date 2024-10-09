namespace WinForms
{
    public partial class KolmasVorm : Form
    {
        private Label timeLabel;
        private Label timeLeft;
        public KolmasVorm(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

            timeLabel = new Label();
            timeLabel.Name = "timeLabel";
            timeLabel.AutoSize = false;
            timeLabel.BorderStyle = BorderStyle.FixedSingle;
            timeLabel.Width = 200;
            timeLabel.Height = 30;
            timeLabel.Font = new Font(timeLabel.Font.FontFamily, 15.75f);
            timeLabel.Location = new Point(300, 0);

            timeLeft = new Label();
            timeLeft.Text = "Time Left";
            timeLeft.Font = new Font(timeLeft.Font.FontFamily, 15.75f);
            timeLeft.Location = new Point(200, 0);

            InitializeControls();
        }
        private void InitializeControls()
        {
            Controls.Add(timeLabel);
            Controls.Add(timeLeft);
        }
    }
}