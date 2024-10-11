using Timer = System.Windows.Forms.Timer;

namespace WinForms
{
    public partial class KolmasVorm : Form
    {
        private Label timeLabel;
        private Label timeLeftLabel;
        private Label plusLeftLabel;
        private Label plusLeftLabelSecond;
        private Label plusRightLabel;
        private Label plusRightLabelSecond;
        private NumericUpDown sum;
        private Label minusLeftLabel;
        private Label minusLeftLabelSecond;
        private Label minusRightLabel;
        private Label minusRightLabelSecond;
        private NumericUpDown difference;
        private Label timesLeftLabel;
        private Label timesLeftLabelSecond;
        private Label timesRightLabel;
        private Label timesRightLabelSecond;
        private NumericUpDown product;
        private Label dividedLeftLabel;
        private Label dividedLeftLabelSecond;
        private Label dividedRightLabel;
        private Label dividedRightLabelSecond;
        private NumericUpDown quotient;
        private Button startButton;
        private Timer timer1 = new Timer();

        // Create a Random object called randomizer 
        // to generate random numbers.
        Random randomizer = new Random();

        // These integer variables store the numbers 
        // for the addition problem. 
        int addend1;
        int addend2;

        // These integer variables store the numbers 
        // for the subtraction problem. 
        int minuend;
        int subtrahend;

        // These integer variables store the numbers 
        // for the multiplication problem. 
        int multiplicand;
        int multiplier;

        // These integer variables store the numbers 
        // for the division problem. 
        int dividend;
        int divisor;

        // This integer variable keeps track of the 
        // remaining time.
        int timeLeft;

        public KolmasVorm(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);

            timeLabel = new Label();
            timeLabel.AutoSize = false;
            timeLabel.BorderStyle = BorderStyle.FixedSingle;
            timeLabel.Width = 200;
            timeLabel.Height = 30;
            timeLabel.Font = new Font(timeLabel.Font.FontFamily, 15.75f);
            timeLabel.Location = new Point(300, 0);

            timeLeftLabel = new Label();
            timeLeftLabel.Text = "Time Left";
            timeLeftLabel.Font = new Font(timeLeftLabel.Font.FontFamily, 15.75f);
            timeLeftLabel.Location = new Point(190, 0);

            plusLeftLabel = new Label();
            plusLeftLabel.Text = "?";
            plusLeftLabel.AutoSize = false;
            plusLeftLabel.Width = 60;
            plusLeftLabel.Height = 50;
            plusLeftLabel.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            plusLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            plusLeftLabel.Location = new Point(50, 75);

            plusLeftLabelSecond = new Label();
            plusLeftLabelSecond.Text = "+";
            plusLeftLabelSecond.AutoSize = false;
            plusLeftLabelSecond.Width = 60;
            plusLeftLabelSecond.Height = 50;
            plusLeftLabelSecond.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            plusLeftLabelSecond.TextAlign = ContentAlignment.MiddleCenter;
            plusLeftLabelSecond.Location = new Point(125, 75);

            plusRightLabel = new Label();
            plusRightLabel.Text = "?";
            plusRightLabel.AutoSize = false;
            plusRightLabel.Width = 60;
            plusRightLabel.Height = 50;
            plusRightLabel.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            plusRightLabel.TextAlign = ContentAlignment.MiddleCenter;
            plusRightLabel.Location = new Point(200, 75);

            plusRightLabelSecond = new Label();
            plusRightLabelSecond.Text = "=";
            plusRightLabelSecond.AutoSize = false;
            plusRightLabelSecond.Width = 60;
            plusRightLabelSecond.Height = 50;
            plusRightLabelSecond.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            plusRightLabelSecond.TextAlign = ContentAlignment.MiddleCenter;
            plusRightLabelSecond.Location = new Point(275, 75);

            sum = new NumericUpDown();
            sum.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            sum.Width = 100;
            sum.Location = new Point(350, 75);
            sum.TabIndex = 1;
            sum.Enter += answer_Enter;

            minusLeftLabel = new Label();
            minusLeftLabel.Text = "?";
            minusLeftLabel.AutoSize = false;
            minusLeftLabel.Width = 60;
            minusLeftLabel.Height = 50;
            minusLeftLabel.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            minusLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            minusLeftLabel.Location = new Point(50, 125);

            minusLeftLabelSecond = new Label();
            minusLeftLabelSecond.Text = "-";
            minusLeftLabelSecond.AutoSize = false;
            minusLeftLabelSecond.Width = 60;
            minusLeftLabelSecond.Height = 50;
            minusLeftLabelSecond.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            minusLeftLabelSecond.TextAlign = ContentAlignment.MiddleCenter;
            minusLeftLabelSecond.Location = new Point(125, 125);

            minusRightLabel = new Label();
            minusRightLabel.Text = "?";
            minusRightLabel.AutoSize = false;
            minusRightLabel.Width = 60;
            minusRightLabel.Height = 50;
            minusRightLabel.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            minusRightLabel.TextAlign = ContentAlignment.MiddleCenter;
            minusRightLabel.Location = new Point(200, 125);

            minusRightLabelSecond = new Label();
            minusRightLabelSecond.Text = "=";
            minusRightLabelSecond.AutoSize = false;
            minusRightLabelSecond.Width = 60;
            minusRightLabelSecond.Height = 50;
            minusRightLabelSecond.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            minusRightLabelSecond.TextAlign = ContentAlignment.MiddleCenter;
            minusRightLabelSecond.Location = new Point(275, 125);

            difference = new NumericUpDown();
            difference.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            difference.Width = 100;
            difference.Location = new Point(350, 125);
            difference.TabIndex = 2;
            difference.Enter += answer_Enter;

            timesLeftLabel = new Label();
            timesLeftLabel.Text = "?";
            timesLeftLabel.AutoSize = false;
            timesLeftLabel.Width = 60;
            timesLeftLabel.Height = 50;
            timesLeftLabel.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            timesLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            timesLeftLabel.Location = new Point(50, 175);
            
            timesLeftLabelSecond = new Label();
            timesLeftLabelSecond.Text = "×";
            timesLeftLabelSecond.AutoSize = false;
            timesLeftLabelSecond.Width = 60;
            timesLeftLabelSecond.Height = 50;
            timesLeftLabelSecond.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            timesLeftLabelSecond.TextAlign = ContentAlignment.MiddleCenter;
            timesLeftLabelSecond.Location = new Point(125, 175);
            
            timesRightLabel = new Label();
            timesRightLabel.Text = "?";
            timesRightLabel.AutoSize = false;
            timesRightLabel.Width = 60;
            timesRightLabel.Height = 50;
            timesRightLabel.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            timesRightLabel.TextAlign = ContentAlignment.MiddleCenter;
            timesRightLabel.Location = new Point(200, 175);
            
            timesRightLabelSecond = new Label();
            timesRightLabelSecond.Text = "=";
            timesRightLabelSecond.AutoSize = false;
            timesRightLabelSecond.Width = 60;
            timesRightLabelSecond.Height = 50;
            timesRightLabelSecond.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            timesRightLabelSecond.TextAlign = ContentAlignment.MiddleCenter;
            timesRightLabelSecond.Location = new Point(275, 175);

            product = new NumericUpDown();
            product.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            product.Width = 100;
            product.Location = new Point(350, 175);
            product.TabIndex = 3;
            product.Enter += answer_Enter;

            dividedLeftLabel = new Label();
            dividedLeftLabel.Text = "?";
            dividedLeftLabel.AutoSize = false;
            dividedLeftLabel.Width = 60;
            dividedLeftLabel.Height = 50;
            dividedLeftLabel.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            dividedLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            dividedLeftLabel.Location = new Point(50, 225);
            
            dividedLeftLabelSecond = new Label();
            dividedLeftLabelSecond.Text = "÷";
            dividedLeftLabelSecond.AutoSize = false;
            dividedLeftLabelSecond.Width = 60;
            dividedLeftLabelSecond.Height = 50;
            dividedLeftLabelSecond.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            dividedLeftLabelSecond.TextAlign = ContentAlignment.MiddleCenter;
            dividedLeftLabelSecond.Location = new Point(125, 225);
            
            dividedRightLabel = new Label();
            dividedRightLabel.Text = "?";
            dividedRightLabel.AutoSize = false;
            dividedRightLabel.Width = 60;
            dividedRightLabel.Height = 50;
            dividedRightLabel.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            dividedRightLabel.TextAlign = ContentAlignment.MiddleCenter;
            dividedRightLabel.Location = new Point(200, 225);
            
            dividedRightLabelSecond = new Label();
            dividedRightLabelSecond.Text = "=";
            dividedRightLabelSecond.AutoSize = false;
            dividedRightLabelSecond.Width = 60;
            dividedRightLabelSecond.Height = 50;
            dividedRightLabelSecond.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            dividedRightLabelSecond.TextAlign = ContentAlignment.MiddleCenter;
            dividedRightLabelSecond.Location = new Point(275, 225);

            quotient = new NumericUpDown();
            quotient.Font = new Font(plusLeftLabel.Font.FontFamily, 18f);
            quotient.Width = 100;
            quotient.Location = new Point(350, 225);
            quotient.TabIndex = 4;
            quotient.Enter += answer_Enter;

            startButton = new Button();
            startButton.Text = "Start the quiz";
            startButton.Font = new Font(plusLeftLabel.Font.FontFamily, 14f);
            startButton.AutoSize = true;
            startButton.TabIndex = 0;
            startButton.Location = new Point(163, 275);
            startButton.Click += startButton_Click;

            InitializeControls();
        }
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer  
                // and show a MessageBox.
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // If CheckTheAnswer() returns false, keep counting
                // down. Decrease the time left by one second and 
                // display the new time left by updating the 
                // Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }
        public void StartTheQuiz()
        {
            // Fill in the addition problem.
            // Generate two random numbers to add.
            // Store the values in the variables 'addend1' and 'addend2'.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // 'sum' is the name of the NumericUpDown control.
            // This step makes sure its value is zero before
            // adding any values to it.
            sum.Value = 0;

            // Fill in the subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }
        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
        private void InitializeControls()
        {
            Controls.Add(timeLabel);
            Controls.Add(timeLeftLabel);
            Controls.Add(plusLeftLabel);
            Controls.Add(plusLeftLabelSecond);
            Controls.Add(plusRightLabel);
            Controls.Add(plusRightLabelSecond);
            Controls.Add(sum);
            Controls.Add(minusLeftLabel);
            Controls.Add(minusLeftLabelSecond);
            Controls.Add(minusRightLabel);
            Controls.Add(minusRightLabelSecond);
            Controls.Add(difference);
            Controls.Add(timesLeftLabel);
            Controls.Add(timesLeftLabelSecond);
            Controls.Add(timesRightLabel);
            Controls.Add(timesRightLabelSecond);
            Controls.Add(product);
            Controls.Add(dividedLeftLabel);
            Controls.Add(dividedLeftLabelSecond);
            Controls.Add(dividedRightLabel);
            Controls.Add(dividedRightLabelSecond);
            Controls.Add(quotient);
            Controls.Add(startButton);
        }
    }
}