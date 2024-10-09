namespace WinForms
{
    public partial class TeineVorm : Form
    {
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button closeButton;
        private Button backgroundButton;
        private Button clearButton;
        private Button showButton;
        private OpenFileDialog openFileDialog1;
        private ColorDialog colorDialog1;
        public TeineVorm(int w, int h)
        {
            this.Width = w;
            this.Height = h;

            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));


            pictureBox1 = new PictureBox();
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;

            checkBox1 = new CheckBox();
            checkBox1.Text = "Stretch";
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;

            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;

            closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.AutoSize = true;
            closeButton.Click += closeButton_Click;
            backgroundButton = new Button();
            backgroundButton.Text = "Set the background color";
            backgroundButton.AutoSize = true;
            backgroundButton.Click += backgroundButton_Click;
            clearButton = new Button();
            clearButton.Text = "Clear the picture";
            clearButton.AutoSize = true;
            clearButton.Click += clearButton_Click;
            showButton = new Button();
            showButton.Text = "Show a picture";
            showButton.AutoSize = true;
            showButton.Click += showButton_Click;

            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openFileDialog1.Title = "Select a picture file";

            colorDialog1 = new ColorDialog();

            InitializeControls();
        }
        private void InitializeControls()
        {
            Controls.Add(tableLayoutPanel1);
            
            flowLayoutPanel1.Controls.Add(showButton);
            flowLayoutPanel1.Controls.Add(clearButton);
            flowLayoutPanel1.Controls.Add(backgroundButton);
            flowLayoutPanel1.Controls.Add(closeButton);

            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.SetColumnSpan(pictureBox1, 2);
            tableLayoutPanel1.Controls.Add(checkBox1, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 1);
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            // Show the Open File dialog. If the user clicks OK, load the
            // picture that the user chose.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            // Clear the picture.
            pictureBox1.Image = null;
        }
        private void backgroundButton_Click(object sender, EventArgs e)
        {
            // Show the color dialog box. If the user clicks OK, change the
            // PictureBox control's background to the color the user chose.
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pictureBox1.BackColor = colorDialog1.Color;
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // If the user selects the Stretch check box, 
            // change the PictureBox's
            // SizeMode property to "Stretch". If the user clears 
            // the check box, change it to "Normal".
            if (checkBox1.Checked)
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}