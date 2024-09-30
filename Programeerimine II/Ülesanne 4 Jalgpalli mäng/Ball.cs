namespace Football;

public class Ball
{
    public int X { get; private set; }
    public int Y { get; private set; }

    private int _vx, _vy;
    private int _previousX;
    private int _previousY;

    private Game _game;

    public Ball(int x, int y, Game game)
    {
        _game = game;
        X = x;
        Y = y;
    }

    public void SetSpeed(int vx, int vy)
    {
        _vx = vx;
        _vy = vy;
    }

    public void Move()
    {
        _previousX = X;
        _previousY = Y;

        int newX = X + _vx;
        int newY = Y + _vy;
        if (_game.Stadium.IsIn(newX, newY))
        {
            X = newX;
            Y = newY;
        }
        else
        {
            _vx = 0;
            _vy = 0;
        }
    }

    public void Draw(char Char, Stadium stadium)
    {
        if (_previousX >= 0 && _previousX < stadium.Width && 
            _previousY >= 0 && _previousY < stadium.Height)
        {
            Console.SetCursorPosition(_previousX, _previousY);
            Console.Write(' ');
        }
    
        if (X >= 0 && X < stadium.Width && 
            Y >= 0 && Y < stadium.Height)
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Char);
        }
    }
}
