using System.Windows.Forms;

namespace ProductsWinForms
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void buttonApply_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
