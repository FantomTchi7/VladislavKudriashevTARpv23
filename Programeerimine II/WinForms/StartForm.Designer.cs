namespace WinForms
{
    public partial class MainForm : Form
    {
        private TreeView tree;
        private Button btn;
        private Label lbl;
        private PictureBox pictureBox;
        private CheckBox chk1, chk2;
        private DataGridView dgv;
        private TabControl tabControl;
        private RadioButton radioButton1, radioButton2;
        private HashSet<Control> addedControls = new HashSet<Control>();
        private int pictureIndex = 0;

        public MainForm()
        {
            this.Text = "Vorm elementidega";
            this.Width = 800;
            this.Height = 600;

            tree = new TreeView();
            TreeNode tn = new TreeNode("Elemendid:");
            tn.Nodes.Add(new TreeNode("Nupp"));
            tn.Nodes.Add(new TreeNode("Silt"));
            tn.Nodes.Add(new TreeNode("Pilt"));
            tn.Nodes.Add(new TreeNode("Märkeruut"));
            tn.Nodes.Add(new TreeNode("DataGridView"));
            tn.Nodes.Add(new TreeNode("TabControl"));
            tree.Nodes.Add(tn);
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;
            this.Controls.Add(tree);

            InitializeControls();
        }

        private void InitializeControls()
        {
            lbl = new Label();
            lbl.Text = "Elementide loomine c# abil";
            lbl.Font = new Font("Arial", 32, FontStyle.Bold);
            lbl.Location = new Point(200, 20);
            lbl.AutoSize = true;
            lbl.MouseHover += Lbl_MouseHover;
            lbl.MouseLeave += Lbl_MouseLeave;

            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Location = new Point(200, 80);
            btn.Click += Btn_Click;

            pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile(@"..\..\..\cat.jpg");
            pictureBox.Size = new Size(100, 100);
            pictureBox.Location = new Point(200, 150);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.DoubleClick += Pbox_DoubleClick;

            chk1 = new CheckBox();
            chk1.Text = "Valik 1";
            chk1.Location = new Point(200, 270);
            chk1.CheckedChanged += Chk_CheckedChanged;

            chk2 = new CheckBox();
            chk2.Text = "Valik 2";
            chk2.Location = new Point(200, 300);
            chk2.CheckedChanged += Chk_CheckedChanged;

            dgv = new DataGridView();
            dgv.Location = new Point(200, 350);
            dgv.Size = new Size(400, 200);
            dgv.Columns.Add("name", "Name");
            dgv.Columns.Add("price", "Price");
            dgv.Columns.Add("description", "Description");
            dgv.Columns.Add("calories", "Calories");
            dgv.Rows.Add("Belgian Waffles", "$5.95", "Two of our famous waffles", "650");
            dgv.Rows.Add("Strawberry Belgian Waffles", "$7.95", "Light Belgian waffles", "900");

            tabControl = new TabControl();
            tabControl.Location = new Point(600, 100);
            tabControl.Size = new Size(200, 150);
            var tabPage1 = new TabPage("TTHK");
            var tabPage2 = new TabPage("Teine");
            var tabPage3 = new TabPage("Kolmas");
            tabControl.TabPages.Add(tabPage1);
            tabControl.TabPages.Add(tabPage2);
            tabControl.TabPages.Add(tabPage3);
        }

        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "Nupp":
                    AddControlIfNotExists(btn);
                    break;

                case "Silt":
                    AddControlIfNotExists(lbl);
                    break;

                case "Pilt":
                    AddControlIfNotExists(pictureBox);
                    break;

                case "Märkeruut":
                    AddControlIfNotExists(chk1);
                    AddControlIfNotExists(chk2);
                    break;

                case "DataGridView":
                    AddControlIfNotExists(dgv);
                    break;

                case "TabControl":
                    AddControlIfNotExists(tabControl);
                    break;
            }
        }

        private void AddControlIfNotExists(Control control)
        {
            if (!addedControls.Contains(control))
            {
                this.Controls.Add(control);
                addedControls.Add(control);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kõige lihtsam aken");
        }

        private void Pbox_DoubleClick(object? sender, EventArgs e)
        {
            string[] pildid = { "cat.jpg", "cat2.jpg", "cat3.jpg" };
            pictureIndex = (pictureIndex + 1) % pildid.Length;
            pictureBox.Image = Image.FromFile(@"..\..\..\" + pildid[pictureIndex]);
        }

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            if (chk1.Checked && chk2.Checked)
            {
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pictureBox.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (chk1.Checked)
            {
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pictureBox.BorderStyle = BorderStyle.None;
            }
            else if (chk2.Checked)
            {
                pictureBox.BorderStyle = BorderStyle.Fixed3D;
                lbl.BorderStyle = BorderStyle.None;
            }
            else
            {
                lbl.BorderStyle = BorderStyle.None;
                pictureBox.BorderStyle = BorderStyle.None;
            }
        }

        private void Lbl_MouseHover(object? sender, EventArgs e)
        {
            lbl.Font = new Font("Arial", 32, FontStyle.Underline);
            lbl.ForeColor = Color.FromArgb(70, 50, 150, 200);
        }

        private void Lbl_MouseLeave(object? sender, EventArgs e)
        {
            lbl.Font = new Font("Arial", 32, FontStyle.Bold);
        }
    }
}
