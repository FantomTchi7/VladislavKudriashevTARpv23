using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace Создание_класса_Triangle_в_проекте_WindowsForm
{
    public partial class TriangleForm : Form
    {
        private Button Run_button, Export_button, Form_button;
        private ListView listView1;
        private ColumnHeader columnHeader1, columnHeader2;
        private Label lblA, lblB, lblC;
        private TextBox txtA, txtB, txtC;
        private Panel panel, panel1;
        public Triangle triangle;
        private SaveFileDialog saveFileDialog;
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

            Export_button = new Button();
            Export_button.Text = "Экспорт";
            Export_button.BackColor = Color.FromArgb(255, 255, 192);
            Export_button.Font = new Font("Mongolian Baiti", 14);
            Export_button.Cursor = Cursors.Hand;
            Export_button.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            Export_button.FlatAppearance.BorderSize = 5;
            Export_button.FlatStyle = FlatStyle.Flat;
            Export_button.Location = new Point(25, 292);
            Export_button.AutoSize = true;
            Export_button.Click += Export_button_Click;

            Form_button = new Button();
            Form_button.Text = "2 Форма";
            Form_button.BackColor = Color.FromArgb(255, 255, 192);
            Form_button.Font = new Font("Mongolian Baiti", 14);
            Form_button.Cursor = Cursors.Hand;
            Form_button.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            Form_button.FlatAppearance.BorderSize = 5;
            Form_button.FlatStyle = FlatStyle.Flat;
            Form_button.Location = new Point(25, 250);
            Form_button.AutoSize = true;
            Form_button.Click += Form_button_Click;

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

            panel = new Panel();
            panel.Location = new Point(500, 180);
            panel.BackColor = Color.FromArgb(0, 192, 192);
            panel.Padding = new Padding(10);
            panel.Width = 200;
            panel.Height = 175;

            panel1 = new Panel();
            panel1.BackColor = Color.FromArgb(255, 255, 192);
            panel1.Dock = DockStyle.Fill;
            panel1.Paint += new PaintEventHandler(panel1_Paint);

            triangle = new Triangle();

            InitializeControls();
        }
        public class Triangle
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
            listView1.Items.Add("Высота");
            listView1.Items.Add("Периметр");
            listView1.Items.Add("Площадь");
            listView1.Items.Add("Существует?");
            listView1.Items.Add("Спецификатор");
            listView1.Items[0].SubItems.Add(triangle.outputA());
            listView1.Items[1].SubItems.Add(triangle.outputB());
            listView1.Items[2].SubItems.Add(triangle.outputC());
            listView1.Items[3].SubItems.Add(Convert.ToString(triangle.CalculateH()));
            listView1.Items[4].SubItems.Add(Convert.ToString(triangle.Perimeter()));
            listView1.Items[5].SubItems.Add(Convert.ToString(triangle.Surface()));
            if (triangle.ExistTriangle) { listView1.Items[6].SubItems.Add("Существует"); }
            else listView1.Items[6].SubItems.Add("Не существует");
            if (triangle.TriangleType() == "Equilateral triangle")
            {
                listView1.Items[7].SubItems.Add("Равнобедренний треугольник");
            }
            else if (triangle.TriangleType() == "Right-angled triangle")
            {
                listView1.Items[7].SubItems.Add("Прямоугольный треугольник");
            } else if (triangle.TriangleType() == "Scalene triangle")
            {
                listView1.Items[7].SubItems.Add("Разносторонний треугольник");
            }
            panel1.Invalidate();
        }
        private void Export_button_Click(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement xmlTriangle = xml.CreateElement("Triangle");
            xml.AppendChild(xmlTriangle);
            XmlElement A = xml.CreateElement("SideA");
            A.InnerText = triangle.outputA();
            xmlTriangle.AppendChild(A);
            XmlElement B = xml.CreateElement("SideB");
            B.InnerText = triangle.outputB();
            xmlTriangle.AppendChild(B);
            XmlElement C = xml.CreateElement("SideC");
            C.InnerText = triangle.outputC();
            xmlTriangle.AppendChild(C);
            XmlElement Height = xml.CreateElement("Height");
            Height.InnerText = Convert.ToString(triangle.CalculateH());
            xmlTriangle.AppendChild(Height);
            XmlElement Perimeter = xml.CreateElement("Perimeter");
            Perimeter.InnerText = Convert.ToString(triangle.Perimeter());
            xmlTriangle.AppendChild(Perimeter);
            XmlElement Area = xml.CreateElement("Area");
            Area.InnerText = Convert.ToString(triangle.Surface());
            xmlTriangle.AppendChild(Area);
            XmlElement Exist = xml.CreateElement("Exists");
            if (triangle.ExistTriangle)
            {
                Exist.InnerText = "Yes";
            }
            else
            {
                Exist.InnerText = "No";
            }
            xmlTriangle.AppendChild(Exist);
            XmlElement Type = xml.CreateElement("Type");
            Type.InnerText = triangle.TriangleType();
            xmlTriangle.AppendChild(Type);

            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML File|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                xml.Save(saveFileDialog.FileName);
                MessageBox.Show("Экспорт завершен.");
            }
        }
        private void Form_button_Click(object sender, EventArgs e)
        {
            TriangleForm2 triangleForm2 = new TriangleForm2();
            triangleForm2.Show();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double a = triangle.a;
            double b = triangle.b;
            double c = triangle.c;

            double maxSide = Math.Max(a, Math.Max(b, c));
            double scale = Math.Min(panel1.Width, panel1.Height) / maxSide * 0.8;

            a *= scale;
            b *= scale;
            c *= scale;

            PointF pointA = new PointF(0, 0);
            PointF pointB = new PointF(pointA.X + (float)a, pointA.Y);

            double angleC = Math.Acos((a * a + b * b - c * c) / (2 * a * b));
            float xC = (float)(pointA.X + b * Math.Cos(angleC));
            float yC = (float)(pointA.Y - b * Math.Sin(angleC));
            PointF pointC = new PointF(xC, yC);

            Pen pen = new Pen(Color.FromArgb(0, 192, 192), 2);
            Font font = new Font("Mongolian Baiti", 12);

            SizeF textSizeA = g.MeasureString("A", font);
            SizeF textSizeB = g.MeasureString("B", font);
            SizeF textSizeC = g.MeasureString("C", font);

            float minX = Math.Min(pointA.X - textSizeA.Width / 2, Math.Min(pointB.X - textSizeB.Width / 2, pointC.X - textSizeC.Width / 2));
            float maxX = Math.Max(pointA.X + textSizeA.Width / 2, Math.Max(pointB.X + textSizeB.Width / 2, pointC.X + textSizeC.Width / 2));
            float minY = Math.Min(pointA.Y, Math.Min(pointB.Y, pointC.Y - textSizeC.Height));
            float maxY = Math.Max(pointA.Y + textSizeA.Height, Math.Max(pointB.Y + textSizeB.Height, pointC.Y));

            float offsetX = (panel1.Width - (maxX - minX)) / 2 - minX;
            float offsetY = (panel1.Height - (maxY - minY)) / 2 - minY;
            pointA.X += offsetX;
            pointA.Y += offsetY;
            pointB.X += offsetX;
            pointB.Y += offsetY;
            pointC.X += offsetX;
            pointC.Y += offsetY;

            g.DrawString("A", font, Brushes.Black, pointA.X - textSizeA.Width / 2, pointA.Y);
            g.DrawString("B", font, Brushes.Black, pointB.X - textSizeB.Width / 2, pointB.Y);
            g.DrawString("C", font, Brushes.Black, pointC.X - textSizeC.Width / 2, pointC.Y - textSizeC.Height);

            g.DrawLine(pen, pointA, pointB);
            g.DrawLine(pen, pointB, pointC);
            g.DrawLine(pen, pointC, pointA);

            Font sideFont = new Font("Mongolian Baiti", 10);
            string lengthA = Math.Round(triangle.a, 2).ToString();
            string lengthB = Math.Round(triangle.b, 2).ToString();
            string lengthC = Math.Round(triangle.c, 2).ToString();
            textSizeA = g.MeasureString(lengthA, font);
            textSizeB = g.MeasureString(lengthB, font);
            textSizeC = g.MeasureString(lengthC, font);
            g.DrawString(lengthA, sideFont, Brushes.Black, (pointA.X + pointB.X) / 2, (pointA.Y + pointB.Y) / 2);
            g.DrawString(lengthB, sideFont, Brushes.Black, (pointB.X + pointC.X) / 2, (pointB.Y + pointC.Y) / 2);
            g.DrawString(lengthC, sideFont, Brushes.Black, (pointC.X + pointA.X) / 2, (pointC.Y + pointA.Y) / 2);
        }
        private void InitializeControls()
        {
            Controls.Add(Run_button);
            Controls.Add(Form_button);
            Controls.Add(Export_button);
            Controls.Add(listView1);
            Controls.Add(txtA);
            Controls.Add(txtB);
            Controls.Add(txtC);
            Controls.Add(lblA);
            Controls.Add(lblB);
            Controls.Add(lblC);
            Controls.Add(panel);
            panel.Controls.Add(panel1);
        }
    }
}