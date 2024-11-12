using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using static Создание_класса_Triangle_в_проекте_WindowsForm.TriangleForm;

namespace Создание_класса_Triangle_в_проекте_WindowsForm
{
    public partial class TriangleForm2 : Form
    {
        private Button Run_button, Import_button;
        private ListView listView1;
        private ColumnHeader columnHeader1, columnHeader2;
        private Panel panel, panel1;
        public Triangle triangle;

        public TriangleForm2()
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

            Import_button = new Button();
            Import_button.Text = "Импорт";
            Import_button.BackColor = Color.FromArgb(255, 255, 192);
            Import_button.Font = new Font("Mongolian Baiti", 14);
            Import_button.Cursor = Cursors.Hand;
            Import_button.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            Import_button.FlatAppearance.BorderSize = 5;
            Import_button.FlatStyle = FlatStyle.Flat;
            Import_button.Location = new Point(25, 292);
            Import_button.AutoSize = true;
            Import_button.Click += Import_button_Click;

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

            InitializeControls();
        }
        private void Import_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML File|*.xml";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(openFileDialog.FileName);
                XmlNode xmlTriangle = xml.SelectSingleNode("/Triangle");

                if (xmlTriangle != null)
                {
                    double sideA = Convert.ToDouble(xmlTriangle.SelectSingleNode("SideA").InnerText);
                    double sideB = Convert.ToDouble(xmlTriangle.SelectSingleNode("SideB").InnerText);
                    double sideC = Convert.ToDouble(xmlTriangle.SelectSingleNode("SideC").InnerText);
                    double height = Convert.ToDouble(xmlTriangle.SelectSingleNode("Height").InnerText);
                    double perimeter = Convert.ToDouble(xmlTriangle.SelectSingleNode("Perimeter").InnerText);
                    double area = Convert.ToDouble(xmlTriangle.SelectSingleNode("Area").InnerText);
                    bool exists = xmlTriangle.SelectSingleNode("Exists").InnerText == "Yes";
                    string type = xmlTriangle.SelectSingleNode("Type").InnerText;
                    MessageBox.Show("Импорт завершен.");
                }
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
            }
            else if (aFilled && bFilled)
            {
                triangle = new Triangle(a, b, 'C');
            }
            else if (bFilled && cFilled)
            {
                triangle = new Triangle(b, c, 'A');
            }
            else if (cFilled && aFilled)
            {
                triangle = new Triangle(a, c, 'B');
            }
            else if (aFilled)
            {
                triangle = new Triangle(a);
            }
            else if (bFilled)
            {
                triangle = new Triangle(b);
            }
            else if (cFilled)
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
            }
            else if (triangle.TriangleType() == "Scalene triangle")
            {
                listView1.Items[7].SubItems.Add("Разносторонний треугольник");
            }
            panel1.Invalidate();
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
            Controls.Add(Import_button);
            Controls.Add(listView1);
            Controls.Add(panel);
            panel.Controls.Add(panel1);
        }
    }
}