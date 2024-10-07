using Microsoft.VisualBasic;
using System.Data;
using System.Windows.Forms;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private TreeView tree;
        private Button btn;
        private Label lbl;
        private PictureBox pictureBox;
        private CheckBox chk1, chk2;
        private TabControl tabControl;
        private RadioButton radioButton1, radioButton2;
        private ListBox listBox;
        private DataGridView dGV;
        private DataSet dS;
        private Button btnAddData;
        private HashSet<Control> addedControls = new HashSet<Control>();
        private int pictureIndex = 0;

        private string[] rbtn_list = { "Esimene", "Teine", "Kolmas" };

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
            tn.Nodes.Add(new TreeNode("M�rkeruut"));
            tn.Nodes.Add(new TreeNode("TabControl"));
            tn.Nodes.Add(new TreeNode("ListBox"));
            tn.Nodes.Add(new TreeNode("DataGridView"));
            tn.Nodes.Add(new TreeNode("Dialogue"));
            tn.Nodes.Add(new TreeNode("Add Plant"));
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

            tabControl = new TabControl();
            tabControl.Location = new Point(600, 100);
            tabControl.Size = new Size(200, 150);
            var tabPage1 = new TabPage("Esimene");
            var tabPage2 = new TabPage("Teine");
            var tabPage3 = new TabPage("Kolmas");
            tabControl.TabPages.Add(tabPage1);
            tabControl.TabPages.Add(tabPage2);
            tabControl.TabPages.Add(tabPage3);

            listBox = new ListBox();
            foreach (string item in rbtn_list)
            {
                listBox.Items.Add(item);
            }
            listBox.Location = new Point(400, 80);
            listBox.SelectedIndexChanged += Lb_SelectedIndexChanged;

            dS = new DataSet("XML file");
            dS.ReadXml(@"..\..\..\plant_catalog.xml");
            dGV = new DataGridView();
            dGV.Location = new Point(500, 400);
            dGV.DataSource = dS;
            dGV.DataMember = "PLANT";
            dGV.Click += DGV_Click;

            btnAddData = new Button() { Text = "Sisesta taime", Location = new Point(200, 530) };
            btnAddData.Click += BtnAddData_Click;
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

                case "M�rkeruut":
                    AddControlIfNotExists(chk1);
                    AddControlIfNotExists(chk2);
                    break;

                case "TabControl":
                    AddControlIfNotExists(tabControl);
                    break;

                case "ListBox":
                    AddControlIfNotExists(listBox);
                    break;

                case "DataGridView":
                    AddControlIfNotExists(dGV);
                    break;

                case "Dialogue":
                    DG_Show();
                    break;

                case "Add Plant":
                    AddControlIfNotExists(btnAddData);
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
            MessageBox.Show("K�ige lihtsam aken");
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

        private void Lb_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (listBox.SelectedIndex)
            {
                case 0: tree.BackColor = Color.Red; break;
                case 1: tree.BackColor = Color.Chocolate; break;
                case 2: tree.BackColor = Color.Purple; break;
                case 3: tree.BackColor = Color.PaleTurquoise; break;
            }
        }

        private void DGV_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(dGV.SelectedCells[0].Value.ToString());
        }

        private void DG_Show()
        {
            MessageBox.Show("Dialogue", "This is a simple window");
            var vastus = MessageBox.Show("Insert data", "Do you want to use the InputBox?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (vastus == DialogResult.Yes)
            {
                string text = Interaction.InputBox("Write something here", "Data insertion");
                MessageBox.Show("You wrote: " + text, text);
            }
        }

        private void BtnAddData_Click(object sender, EventArgs e)
        {
            DataRow newRow = dS.Tables["PLANT"].NewRow();
            newRow["COMMON"] = CheckEmpty(Interaction.InputBox("Insert COMMON"));
            newRow["BOTANICAL"] = CheckEmpty(Interaction.InputBox("Insert BOTANICAL"));
            newRow["ZONE"] = CheckEmpty(Interaction.InputBox("Insert ZONE"));
            newRow["LIGHT"] = CheckEmpty(Interaction.InputBox("Insert LIGHT"));
            newRow["PRICE"] = CheckEmpty(Interaction.InputBox("Insert PRICE"));
            newRow["AVAILABILITY"] = CheckEmpty(Interaction.InputBox("Insert AVAILABILITY"));

            dS.Tables["PLANT"].Rows.Add(newRow);

            dS.WriteXml(@"..\..\..\plant_catalog.xml");

            MessageBox.Show("New plant added successfully!");
        }

        private string? CheckEmpty(string text)
        {
            if (text == "" || text == null)
            {
                MessageBox.Show("Viga, pole sisestanud andmed");
                return null;
            } else {
                return text;
            }
        }
    }
}