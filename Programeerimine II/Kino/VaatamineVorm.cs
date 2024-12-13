using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kino
{
    public partial class VaatamineVorm : Form
    {
        Label label1 = new Label
        {
            Location = new Point(25, 25)
        };
        public VaatamineVorm(string kasutajaTuup)
        {
            label1.Text = kasutajaTuup;
            InitializeComponent();

            this.Controls.Add(label1);
        }
    }
}
