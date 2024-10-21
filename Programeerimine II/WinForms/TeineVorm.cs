namespace WinForms
{
    public partial class TeineVorm : Form
    {
        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private Button showButton;
        private OpenFileDialog openFileDialog1;

        private MenuStrip menuStrip1;
        private ToolStripMenuItem stretchMenuItem;
        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem editMenuItem;
        private ToolStripMenuItem viewMenuItem;

        private Button rotateButton;
        private SaveFileDialog saveFileDialog1;
        private string currentImagePath;

        private ToolStripMenuItem themeMenuItem;
        public TeineVorm(int w, int h)
        {
            this.Height = 375;
            this.Width = 575;

            menuStrip1 = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem("File");
            ToolStripMenuItem openMenuItem = new ToolStripMenuItem("Open picture...");
            ToolStripMenuItem saveMenuItem = new ToolStripMenuItem("Save as...");

            openMenuItem.Click += showButton_Click;
            saveMenuItem.Click += saveButton_Click;
            fileMenuItem.DropDownItems.Add(openMenuItem);
            fileMenuItem.DropDownItems.Add(saveMenuItem);
            menuStrip1.Items.Add(fileMenuItem);

            editMenuItem = new ToolStripMenuItem("Edit");
            stretchMenuItem = new ToolStripMenuItem("Stretch picture");
            ToolStripMenuItem rotateLeftMenuItem = new ToolStripMenuItem("Rotate 90 to the left");
            ToolStripMenuItem rotateRightMenuItem = new ToolStripMenuItem("Rotate 90 to the right");
            ToolStripMenuItem resetMenuItem = new ToolStripMenuItem("Reset picture");

            stretchMenuItem.CheckOnClick = true;
            stretchMenuItem.CheckedChanged += checkBox1_CheckedChanged;
            rotateLeftMenuItem.Click += (sender, e) => rotateButton_Click("left", sender, e);
            rotateRightMenuItem.Click += (sender, e) => rotateButton_Click("right", sender, e);
            resetMenuItem.Click += resetButton_Click;

            editMenuItem.DropDownItems.Add(stretchMenuItem);
            editMenuItem.DropDownItems.Add(rotateLeftMenuItem);
            editMenuItem.DropDownItems.Add(rotateRightMenuItem);
            editMenuItem.DropDownItems.Add(resetMenuItem);
            menuStrip1.Items.Add(editMenuItem);

            viewMenuItem = new ToolStripMenuItem("View");
            themeMenuItem = new ToolStripMenuItem("Black theme");
            themeMenuItem.CheckOnClick = true;
            themeMenuItem.CheckedChanged += themeMenuItem_CheckedChanged;
            viewMenuItem.DropDownItems.Add(themeMenuItem);
            menuStrip1.Items.Add(viewMenuItem);

            Controls.Add(menuStrip1);

            pictureBox1 = new PictureBox();
            pictureBox1.Dock = DockStyle.Fill;

            openFileDialog1 = new OpenFileDialog
            {
                Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*",
                Title = "Select picture file"
            };

            saveFileDialog1 = new SaveFileDialog
            {
                Filter = "PNG Files (*.png)|*.png",
                Title = "Save picture file"
            };

            this.BackColor = Color.White;
            menuStrip1.BackColor = Color.White;
            menuStrip1.ForeColor = Color.Black;

            foreach (ToolStripItem item in menuStrip1.Items)
            {
                item.BackColor = Color.White;
                item.ForeColor = Color.Black;
            }

            pictureBox1.BackColor = Color.White;
            pictureBox1.ForeColor = Color.Black;

            InitializeControls();
        }
        private void InitializeControls()
        {
            Controls.Add(menuStrip1);
            Controls.Add(pictureBox1);
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                currentImagePath = openFileDialog1.FileName;
                pictureBox1.Load(currentImagePath);
                this.Text = System.IO.Path.GetFileName(currentImagePath);
                adjustPictureBoxSize();
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }
            else
            {
                MessageBox.Show("No image to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = stretchMenuItem.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.Normal;
        }
        private void rotateButton_Click(string side, object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (side == "left")
                {
                    pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else if (side == "right")
                {
                    pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                pictureBox1.Refresh();
                adjustPictureBoxSize();
            }
            else
            {
                MessageBox.Show("No image to rotate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void adjustPictureBoxSize()
        {
            if (pictureBox1.Image != null)
            {
                this.Width = pictureBox1.Image.Size.Width + 16;
                this.Height = pictureBox1.Image.Size.Height + menuStrip1.Height + 15;
            }
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            if (currentImagePath != null)
            {
                pictureBox1.Load(currentImagePath);
                adjustPictureBoxSize();
            }
            else
            {
                MessageBox.Show("No image loaded to reset.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void themeMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ApplyTheme(themeMenuItem.Checked);
        }

        private void ApplyTheme(bool isDarkTheme)
        {
            if (isDarkTheme)
            {
                this.BackColor = Color.FromArgb(45, 45, 48);
                menuStrip1.BackColor = Color.FromArgb(45, 45, 48);
                menuStrip1.ForeColor = Color.White;

                foreach (ToolStripItem item in menuStrip1.Items)
                {
                    ApplyItemTheme(item, Color.FromArgb(45, 45, 48), Color.White);
                }

                pictureBox1.BackColor = Color.FromArgb(45, 45, 48);
                pictureBox1.ForeColor = Color.White;
            }
            else
            {
                this.BackColor = Color.White;
                menuStrip1.BackColor = Color.White;
                menuStrip1.ForeColor = Color.Black;

                foreach (ToolStripItem item in menuStrip1.Items)
                {
                    ApplyItemTheme(item, Color.White, Color.Black);
                }

                pictureBox1.BackColor = Color.White;
                pictureBox1.ForeColor = Color.Black;
            }
        }
        private void ApplyItemTheme(ToolStripItem item, Color backColor, Color foreColor)
        {
            item.BackColor = backColor;
            item.ForeColor = foreColor;

            if (item is ToolStripDropDownItem dropDownItem)
            {
                foreach (ToolStripItem subItem in dropDownItem.DropDownItems)
                {
                    ApplyItemTheme(subItem, backColor, foreColor);
                }
            }
        }
    }
}