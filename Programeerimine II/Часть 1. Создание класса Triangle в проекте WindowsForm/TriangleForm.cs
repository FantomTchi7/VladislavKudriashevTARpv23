using System;
using System.Drawing;
using System.Windows.Forms;

namespace Часть_1._Создание_класса_Triangle_в_проекте_WindowsForm
{
    public partial class TriangleForm : Form
    {
        private Button button1;
        public TriangleForm()
        {
            this.Text = "Работа с треугольником";
            this.Width = 800;
            this.Height = 400;
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon("Tools2.ico");

            button1 = new Button();
            button1.Text = "Запуск";
            button1.BackColor = Color.FromArgb(255, 255, 192);
            button1.Font = new Font("Mongolian Baiti", 28);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            button1.FlatAppearance.BorderSize = 10;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(500, 50);
            button1.Height = 125;
            button1.Width = 200;

            InitializeControls();
        }
        class Triangle
        {
            public double a;
            public double b;
            public double c;
            public double h;
            public Triangle(double A, double B, double C)
            {
                a = A;
                b = B;
                c = C;
                h = CalculateH();
            }
            public Triangle(double baseLength, double height, bool isBaseHeight)
            {
                if (isBaseHeight)
                {
                    a = baseLength;
                    h = height;
                    double area = (a * h) / 2;
                    c = Math.Sqrt(Math.Pow(a / 2, 2) + Math.Pow(h, 2));
                    b = c;
                }
                else
                {
                    a = 0;
                    b = 0;
                    c = 0;
                    h = height;
                }
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
            public bool ExistTriangle
            {
                get
                {
                    return (a + b > c) && (a + c > b) && (b + c > a);
                }
            }
        }
        private void InitializeControls()
        {
            Controls.Add(button1);
        }
    }
}