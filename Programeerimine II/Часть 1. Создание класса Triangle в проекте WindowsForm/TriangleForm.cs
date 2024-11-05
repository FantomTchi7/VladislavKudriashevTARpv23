using System;
using System.Drawing;
using System.Windows.Forms;

namespace Часть_1._Создание_класса_Triangle_в_проекте_WindowsForm
{
    public partial class TriangleForm : Form
    {
        private Button Run_button;
        private ListView listView1;
        private ColumnHeader columnHeader1, columnHeader2;
        private Label lblA, lblB, lblC;
        private TextBox txtA, txtB, txtC;
        private Panel panel1;
        private Triangle triangle;
        public TriangleForm()
        {
            this.Text = "Работа с треугольником";
            this.Width = 800;
            this.Height = 400;
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon("Tools2.ico");

            Run_button = new Button();
            Run_button.Text = "Запуск";
            Run_button.BackColor = Color.FromArgb(255, 255, 192);
            Run_button.Font = new Font("Mongolian Baiti", 28);
            Run_button.Cursor = Cursors.Hand;
            Run_button.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            Run_button.FlatAppearance.BorderSize = 10;
            Run_button.FlatStyle = FlatStyle.Flat;
            Run_button.Location = new Point(500, 50);
            Run_button.Width = 200;
            Run_button.Height = 125;
            Run_button.Click += Run_button_Click;

            columnHeader1 = new ColumnHeader();
            columnHeader1.Text = "Поле";
            columnHeader1.Width = 175;

            columnHeader2 = new ColumnHeader();
            columnHeader2.Text = "Значение";
            columnHeader2.Width = 175;

            listView1 = new ListView();
            listView1.View = View.Details;
            listView1.Width = 350;
            listView1.Height = 225;
            listView1.Location = new Point(25, 10);
            listView1.Columns.Add(columnHeader1);
            listView1.Columns.Add(columnHeader2);

            lblA = new Label();
            lblB = new Label();
            lblC = new Label();
            lblA.Location = new Point(150, 250);
            lblB.Location = new Point(150, 280);
            lblC.Location = new Point(150, 310);
            lblA.Text = "Сторона A:";
            lblB.Text = "Сторона B:";
            lblC.Text = "Сторона C:";

            txtA = new TextBox();
            txtB = new TextBox();
            txtC = new TextBox();
            txtA.Location = new Point(225, 250);
            txtB.Location = new Point(225, 280);
            txtC.Location = new Point(225, 310);

            panel1 = new Panel();
            panel1.Location = new Point(500, 180);
            panel1.Width = 150;
            panel1.Height = 150;
            panel1.BackColor = Color.Black;
            panel1.Paint += new PaintEventHandler(panel1_Paint);

            triangle = new Triangle();

            InitializeControls();
        }
        class Triangle
        {
            public double a;
            public double b;
            public double c;
            public double h;
            public Triangle(double A)
            {
                a = A;
                b = A;
                c = A;
                h = Math.Sqrt(Math.Pow(A, 2) - Math.Pow(A / 2, 2));
            }
            public Triangle(double length, double height, char missingSide)
            {
                if (missingSide.ToString().ToUpper() == "C")
                {
                    a = length;
                    b = height;
                    c = CalculateHypotenuse(a, b);
                } else if (missingSide.ToString().ToUpper() == "B")
                {
                    a = length;
                    c = height;
                    b = CalculateHypotenuse(a, c);
                } else if (missingSide.ToString().ToUpper() == "A")
                {
                    b = length;
                    c = height;
                    a = CalculateHypotenuse(b, c);
                } else
                {
                    a = 0;
                    b = 0;
                    c = 0;
                }
            }
            public Triangle(double A, double B, double C)
            {
                a = A;
                b = B;
                c = C;
                h = CalculateH();
            }
            public Triangle()
            {
                a = 0;
                b = 0;
                c = 0;
                h = 0;
            }
            public string outputA() => a.ToString();
            public string outputB() => b.ToString();
            public string outputC() => c.ToString();
            public string outputH() => h.ToString();
            public double Perimeter() => a + b + c;
            public double Surface()
            {
                double p = (a + b + c) / 2;
                return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            }
            public double GetSetA
            {
                get => a;
                set
                {
                    a = value;
                    h = CalculateH();
                }
            }
            public double GetSetB
            {
                get => b;
                set
                {
                    b = value;
                    h = CalculateH();
                }
            }
            public double GetSetC
            {
                get => c;
                set
                {
                    c = value;
                    h = CalculateH();
                }
            }
            public double GetSetH
            {
                get => h;
                set => h = value;
            }
            public double CalculateA()
            {
                if (h > 0)
                    return (2 * Surface()) / h;
                return 0;
            }
            public double CalculateB()
            {
                if (h > 0)
                    return (2 * Surface()) / h;
                return 0;
            }
            public double CalculateC()
            {
                if (h > 0)
                    return (2 * Surface()) / h;
                return 0;
            }
            public double CalculateH()
            {
                if (a > 0)
                {
                    double area = Surface();
                    return (2 * area) / a;
                }
                return 0;
            }
            public double CalculateHypotenuse(double a, double b)
            {
                return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            }
            public bool ExistTriangle
            {
                get
                {
                    return (a + b > c) && (a + c > b) && (b + c > a);
                }
            }
            public string TriangleType()
            {
                if (!(a + b > c && a + c > b && b + c > a))
                {
                    return "Not a triangle";
                }
                if (a == b && b == c)
                {
                    return "Equilateral triangle";
                }
                double[] sides = { a, b, c };
                Array.Sort(sides);
                if (Math.Abs(Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) - Math.Pow(sides[2], 2)) < 1e-9)
                {
                    return "Right-angled triangle";
                }
                return "Scalene triangle";
            }
        }
        private void Run_button_Click(object sender, EventArgs e)
        {
            double a, b, c;
            bool aFilled = !string.IsNullOrWhiteSpace(txtA.Text);
            bool bFilled = !string.IsNullOrWhiteSpace(txtB.Text);
            bool cFilled = !string.IsNullOrWhiteSpace(txtC.Text);
            double.TryParse(txtA.Text, out a);
            double.TryParse(txtB.Text, out b);
            double.TryParse(txtC.Text, out c);
            
            if (aFilled && bFilled && cFilled)
            {
                triangle = new Triangle(a, b, c);
            } else if (aFilled && bFilled)
            {
                triangle = new Triangle(a, b, 'C');
            } else if (bFilled && cFilled)
            {
                triangle = new Triangle(b, c, 'A');
            } else if (cFilled && aFilled)
            {
                triangle = new Triangle(a, c, 'B');
            } else if (aFilled)
            {
                triangle = new Triangle(a);
            } else if (bFilled)
            {
                triangle = new Triangle(b);
            } else if (cFilled)
            {
                triangle = new Triangle(c);
            }

            listView1.Items.Clear();
            listView1.Items.Add("Сторона a");
            listView1.Items.Add("Сторона b");
            listView1.Items.Add("Сторона c");
            listView1.Items.Add("Периметр");
            listView1.Items.Add("Площадь");
            listView1.Items.Add("Существует?");
            listView1.Items.Add("Спецификатор");
            listView1.Items[0].SubItems.Add(triangle.outputA());
            listView1.Items[1].SubItems.Add(triangle.outputB());
            listView1.Items[2].SubItems.Add(triangle.outputC());
            listView1.Items[3].SubItems.Add(Convert.ToString(triangle.Perimeter()));
            listView1.Items[4].SubItems.Add(Convert.ToString(triangle.Surface()));
            if (triangle.ExistTriangle) { listView1.Items[5].SubItems.Add("Существует"); }
            else listView1.Items[5].SubItems.Add("Не существует");
            panel1.Invalidate();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            PointF[] points;

            if (triangle.TriangleType() == "Equilateral triangle")
            {
                points = new PointF[]
                {
                    new PointF(panel1.Width / 2, (float)(panel1.Height - (Math.Sqrt(3) / 2 * panel1.Height))),
                    new PointF(0, panel1.Height),
                    new PointF(panel1.Width, panel1.Height)
                };
            } else if (triangle.TriangleType() == "Right-angled triangle")
            {
                points = new PointF[]
                {
                    new PointF(0, 0),
                    new PointF(0, panel1.Height),
                    new PointF(panel1.Width, panel1.Width)
                };
            } else if (triangle.TriangleType() == "Scalene triangle")
            {
                points = new PointF[]
                {
                    new Point(0, 0)
                };
            } else
            {
                points = new PointF[]
                {
                    new Point(0, 0)
                };
            }

            g.FillPolygon(Brushes.Blue, points);
        }
        private void InitializeControls()
        {
            Controls.Add(Run_button);
            Controls.Add(listView1);
            Controls.Add(txtA);
            Controls.Add(txtB);
            Controls.Add(txtC);
            Controls.Add(lblA);
            Controls.Add(lblB);
            Controls.Add(lblC);
            Controls.Add(panel1);
        }
    }
}