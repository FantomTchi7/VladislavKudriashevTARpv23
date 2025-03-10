public class TripsTrapsTrull : ContentPage
{
    private Stepper sizeStepper;
    private Stepper playerStepper;
    private Label infoLabel;
    private Grid gameGrid;
    private Label statusLabel;
    private Button[,] gameButtons;
    private int currentPlayerIndex = 0;
    private int currentGridSize = 3;
    private int requiredToWin = 3;
    private string[,] board;
    private int numberOfPlayers = 2;
    private List<string> playerSymbols = new List<string> { "O", "X", "Y", "Z", "A" };

    public TripsTrapsTrull()
    {
        InitializeUI();
        UpdateGameGrid();
    }

    private void InitializeUI()
    {
        sizeStepper = new Stepper
        {
            Minimum = 3,
            Maximum = 7,
            Increment = 2,
            Value = currentGridSize,
            HorizontalOptions = LayoutOptions.Center
        };
        sizeStepper.ValueChanged += OnSizeStepperValueChanged;

        playerStepper = new Stepper
        {
            IsEnabled = false,
            Minimum = 2,
            Maximum = CalculateMaxPlayers(currentGridSize),
            Value = 2,
            Increment = 1,
            HorizontalOptions = LayoutOptions.Center
        };
        playerStepper.ValueChanged += OnPlayerStepperValueChanged;

        infoLabel = new Label
        {
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 10)
        };

        statusLabel = new Label
        {
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 10)
        };

        gameGrid = new Grid
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(20)
        };

        VerticalStackLayout mainLayout = new VerticalStackLayout
        {
            Padding = new Thickness(20),
            Children = { sizeStepper, playerStepper, infoLabel, gameGrid, statusLabel }
        };

        Content = mainLayout;
        UpdateInfoLabel();
        UpdateStatus($"Player {playerSymbols[currentPlayerIndex]}'s turn");
    }

    private int CalculateMaxPlayers(int gridSize)
    {
        if (gridSize == 3) return 2;
        else if (gridSize == 5) return 3;
        else if (gridSize == 7) return 4;
        else return 0;
    }

    private void OnSizeStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        currentGridSize = (int)e.NewValue;

        if (currentGridSize <= 3) playerStepper.IsEnabled = false;
        else playerStepper.IsEnabled = true;

        int newMaxPlayers = CalculateMaxPlayers(currentGridSize);
        playerStepper.Maximum = newMaxPlayers;

        if (playerStepper.Value > newMaxPlayers)
        {
            playerStepper.Value = newMaxPlayers;
        }

        requiredToWin = Math.Max(2, ((currentGridSize - 3) / 2 + 3) - (numberOfPlayers - 2));
        UpdateInfoLabel();
        UpdateGameGrid();
        ResetGame();
    }

    private void OnPlayerStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        numberOfPlayers = (int)e.NewValue;
        requiredToWin = Math.Max(2, ((currentGridSize - 3) / 2 + 3) - (numberOfPlayers - 2));
        currentPlayerIndex = 0;
        UpdateInfoLabel();
        ResetGame();
    }

    private void UpdateInfoLabel()
    {
        infoLabel.Text = $"Grid Size: {currentGridSize}x{currentGridSize} - Players: {numberOfPlayers} - Win with {requiredToWin} in a row";
    }

    private void UpdateGameGrid()
    {
        gameGrid.Children.Clear();
        gameGrid.RowDefinitions.Clear();
        gameGrid.ColumnDefinitions.Clear();

        for (int i = 0; i < currentGridSize; i++)
        {
            gameGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }

        board = new string[currentGridSize, currentGridSize];
        gameButtons = new Button[currentGridSize, currentGridSize];

        for (int row = 0; row < currentGridSize; row++)
        {
            for (int col = 0; col < currentGridSize; col++)
            {
                Button button = new Button
                {
                    FontSize = 30,
                    CornerRadius = 0
                };

                int currentRow = row;
                int currentCol = col;
                button.Clicked += (sender, e) => OnCellClicked(currentRow, currentCol);
                gameGrid.Children.Add(button);
                gameButtons[row, col] = button;
                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);
            }
        }
    }

    private void OnCellClicked(int row, int col)
    {
        if (board[row, col] != null) return;

        string currentPlayerSymbol = playerSymbols[currentPlayerIndex];
        board[row, col] = currentPlayerSymbol;
        gameButtons[row, col].Text = currentPlayerSymbol;

        if (CheckWin(currentPlayerSymbol))
        {
            UpdateStatus($"Player {currentPlayerSymbol} wins!");
            DisableAllButtons();
            return;
        }

        if (CheckDraw())
        {
            UpdateStatus("It's a draw!");
            DisableAllButtons();
            return;
        }

        currentPlayerIndex = (currentPlayerIndex + 1) % numberOfPlayers;
        UpdateStatus($"Player {playerSymbols[currentPlayerIndex]}'s turn");
    }

    private bool CheckWin(string player)
    {
        for (int row = 0; row < currentGridSize; row++)
        {
            for (int col = 0; col <= currentGridSize - requiredToWin; col++)
            {
                bool win = true;
                for (int k = 0; k < requiredToWin; k++)
                {
                    if (board[row, col + k] != player)
                    {
                        win = false;
                        break;
                    }
                }
                if (win) return true;
            }
        }

        for (int col = 0; col < currentGridSize; col++)
        {
            for (int row = 0; row <= currentGridSize - requiredToWin; row++)
            {
                bool win = true;
                for (int k = 0; k < requiredToWin; k++)
                {
                    if (board[row + k, col] != player)
                    {
                        win = false;
                        break;
                    }
                }
                if (win) return true;
            }
        }

        for (int row = 0; row <= currentGridSize - requiredToWin; row++)
        {
            for (int col = 0; col <= currentGridSize - requiredToWin; col++)
            {
                bool win = true;
                for (int k = 0; k < requiredToWin; k++)
                {
                    if (board[row + k, col + k] != player)
                    {
                        win = false;
                        break;
                    }
                }
                if (win) return true;
            }
        }

        for (int row = 0; row <= currentGridSize - requiredToWin; row++)
        {
            for (int col = requiredToWin - 1; col < currentGridSize; col++)
            {
                bool win = true;
                for (int k = 0; k < requiredToWin; k++)
                {
                    if (board[row + k, col - k] != player)
                    {
                        win = false;
                        break;
                    }
                }
                if (win) return true;
            }
        }

        return false;
    }

    private bool CheckDraw()
    {
        for (int row = 0; row < currentGridSize; row++)
        {
            for (int col = 0; col < currentGridSize; col++)
            {
                if (board[row, col] == null) return false;
            }
        }
        return true;
    }

    private void DisableAllButtons()
    {
        foreach (Button button in gameButtons)
        {
            button.IsEnabled = false;
        }
    }

    private void ResetGame()
    {
        currentPlayerIndex = 0;
        for (int row = 0; row < currentGridSize; row++)
        {
            for (int col = 0; col < currentGridSize; col++)
            {
                board[row, col] = null;
                gameButtons[row, col].Text = "";
                gameButtons[row, col].IsEnabled = true;
            }
        }
        UpdateStatus($"Player {playerSymbols[currentPlayerIndex]}'s turn");
    }

    private void UpdateStatus(string message)
    {
        statusLabel.Text = message;
    }
}