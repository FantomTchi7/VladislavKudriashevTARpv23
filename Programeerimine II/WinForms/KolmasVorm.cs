using Timer = System.Windows.Forms.Timer;

namespace WinForms
{
    public partial class KolmasVorm : Form
    {
        private Label timeLabel;
        private Label timeLeftLabel;
        private Button startButton;
        private Button restartButton;
        private Button giveupButton;
        private Timer timer1 = new Timer();

        // Create a Random object called randomizer 
        // to generate random numbers.
        Random randomizer = new Random();

        int timeLeft;

        private List<ArithmeticProblem> problems = new List<ArithmeticProblem>();

        public KolmasVorm(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;

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

            startButton = new Button();
            startButton.Text = "Start the quiz";
            startButton.Font = new Font(startButton.Font.FontFamily, 14f);
            startButton.AutoSize = true;
            startButton.TabIndex = 0;
            startButton.Location = new Point(90, 275);
            startButton.Click += startButton_Click;

            restartButton = new Button();
            restartButton.Text = "Restart the quiz";
            restartButton.Font = new Font(restartButton.Font.FontFamily, 14f);
            restartButton.AutoSize = true;
            restartButton.TabIndex = 5;
            restartButton.Location = new Point(230, 275);
            restartButton.Click += startButton_Click;

            giveupButton = new Button();
            giveupButton.Text = "Give up";
            giveupButton.Font = new Font(restartButton.Font.FontFamily, 14f);
            giveupButton.AutoSize = true;
            giveupButton.TabIndex = 6;
            giveupButton.Location = new Point(183, 323);
            giveupButton.Click += giveupButton_Click;

            InitializeArithmeticProblems();

            InitializeControls();
        }
        private void InitializeArithmeticProblems()
        {
            int[] yPositions = { 75, 125, 175, 225 };
            int[] tabIndices = { 1, 2, 3, 4 };

            for (int i = 0; i < 4; i++)
            {
                var problem = new ArithmeticProblem(yPositions[i], tabIndices[i], answer_Enter);
                problems.Add(problem);
            }
        }
        private bool CheckTheAnswer()
        {
            foreach (var problem in problems)
            {
                if (!problem.IsCorrect())
                    return false;
            }
            return true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("You got all the answers right!", "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft--;
                timeLabel.Text = $"{timeLeft} seconds";
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                foreach (var problem in problems)
                {
                    problem.SetCorrectAnswer();
                }
                startButton.Enabled = true;
            }
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }
        private void giveupButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            timer1.Stop();
            foreach (var problem in problems)
            {
                problem.SetCorrectAnswer();
            }
        }
        public void StartTheQuiz()
        {
            // 1 = addition, 2 = subtraction, 3 = multiplication, 4 = division
            List<int> operationTypes = new List<int>() { 1, 2, 3, 4 };

            Shuffle(operationTypes);

            for (int i = 0; i < problems.Count; i++)
            {
                problems[i].AssignOperation(operationTypes[i], randomizer);
            }

            timeLeft = 30;
            timer1.Start();
        }
        private void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = randomizer.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private void answer_Enter(object sender, EventArgs e)
        {
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
            Controls.Add(startButton);
            Controls.Add(restartButton);
            Controls.Add(giveupButton);

            foreach (var problem in problems)
            {
                Controls.Add(problem.LeftOperandLabel);
                Controls.Add(problem.OperatorLabel);
                Controls.Add(problem.RightOperandLabel);
                Controls.Add(problem.EqualsLabel);
                Controls.Add(problem.AnswerBox);
            }
        }
    }
    public class ArithmeticProblem
    {
        public Label LeftOperandLabel { get; private set; }
        public Label OperatorLabel { get; private set; }
        public Label RightOperandLabel { get; private set; }
        public Label EqualsLabel { get; private set; }
        public NumericUpDown AnswerBox { get; private set; }

        public int OperationType { get; set; }

        public int Operand1 { get; set; }
        public int Operand2 { get; set; }

        public ArithmeticProblem(int yPosition, int tabIndex, EventHandler enterHandler)
        {
            LeftOperandLabel = new Label();
            LeftOperandLabel.Text = "?";
            LeftOperandLabel.AutoSize = false;
            LeftOperandLabel.Width = 60;
            LeftOperandLabel.Height = 50;
            LeftOperandLabel.Font = new Font(LeftOperandLabel.Font.FontFamily, 18f);
            LeftOperandLabel.TextAlign = ContentAlignment.MiddleCenter;
            LeftOperandLabel.Location = new Point(50, yPosition);

            OperatorLabel = new Label();
            OperatorLabel.Text = "?";
            OperatorLabel.AutoSize = false;
            OperatorLabel.Width = 60;
            OperatorLabel.Height = 50;
            OperatorLabel.Font = new Font(OperatorLabel.Font.FontFamily, 18f);
            OperatorLabel.TextAlign = ContentAlignment.MiddleCenter;
            OperatorLabel.Location = new Point(125, yPosition);

            RightOperandLabel = new Label();
            RightOperandLabel.Text = "?";
            RightOperandLabel.AutoSize = false;
            RightOperandLabel.Width = 60;
            RightOperandLabel.Height = 50;
            RightOperandLabel.Font = new Font(RightOperandLabel.Font.FontFamily, 18f);
            RightOperandLabel.TextAlign = ContentAlignment.MiddleCenter;
            RightOperandLabel.Location = new Point(200, yPosition);

            EqualsLabel = new Label();
            EqualsLabel.Text = "=";
            EqualsLabel.AutoSize = false;
            EqualsLabel.Width = 60;
            EqualsLabel.Height = 50;
            EqualsLabel.Font = new Font(EqualsLabel.Font.FontFamily, 18f);
            EqualsLabel.TextAlign = ContentAlignment.MiddleCenter;
            EqualsLabel.Location = new Point(275, yPosition);

            AnswerBox = new NumericUpDown();
            AnswerBox.Font = new Font(AnswerBox.Font.FontFamily, 18f);
            AnswerBox.Width = 100;
            AnswerBox.Location = new Point(350, yPosition);
            AnswerBox.TabIndex = tabIndex;
            AnswerBox.Enter += enterHandler;
        }
        public void AssignOperation(int operationType, Random randomizer)
        {
            OperationType = operationType;
            switch (operationType)
            {
                case 1: // Addition
                    Operand1 = randomizer.Next(51);
                    Operand2 = randomizer.Next(51);
                    OperatorLabel.Text = "+";
                    break;
                case 2: // Subtraction
                    Operand1 = randomizer.Next(1, 101);
                    Operand2 = randomizer.Next(1, Operand1);
                    OperatorLabel.Text = "-";
                    break;
                case 3: // Multiplication
                    Operand1 = randomizer.Next(2, 11);
                    Operand2 = randomizer.Next(2, 11);
                    OperatorLabel.Text = "×";
                    break;
                case 4: // Division
                    Operand2 = randomizer.Next(2, 11);
                    int tempQuotient = randomizer.Next(2, 11);
                    Operand1 = Operand2 * tempQuotient;
                    OperatorLabel.Text = "÷";
                    break;
            }
            LeftOperandLabel.Text = Operand1.ToString();
            RightOperandLabel.Text = Operand2.ToString();
            AnswerBox.Value = 0;
        }
        public bool IsCorrect()
        {
            switch (OperationType)
            {
                case 1:
                    return (Operand1 + Operand2 == AnswerBox.Value);
                case 2:
                    return (Operand1 - Operand2 == AnswerBox.Value);
                case 3:
                    return (Operand1 * Operand2 == AnswerBox.Value);
                case 4:
                    return (Operand1 / Operand2 == AnswerBox.Value);
                default:
                    return false;
            }
        }
        public void SetCorrectAnswer()
        {
            switch (OperationType)
            {
                case 1:
                    AnswerBox.Value = Operand1 + Operand2;
                    break;
                case 2:
                    AnswerBox.Value = Operand1 - Operand2;
                    break;
                case 3:
                    AnswerBox.Value = Operand1 * Operand2;
                    break;
                case 4:
                    AnswerBox.Value = Operand1 / Operand2;
                    break;
            }
        }
    }
}