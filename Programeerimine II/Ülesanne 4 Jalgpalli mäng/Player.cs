namespace Football;

public class Player
{
    public string Name { get; }
    public int X { get; private set; }
    public int Y { get; private set; }
    private int _vx, _vy;
    private int _previousX;
    private int _previousY;
    public Team? Team { get; set; } = null;

    private const int MaxSpeed = 1;
    private const int MaxKickSpeed = 2;
    private const int BallKickDistance = 1;

    private Random _random = new Random();

    public Player(string name)
    {
        Name = name;
    }

    public Player(string name, int x, int y)
    {
        Name = name;
        X = x;
        Y = y;
    }

    public Player(string name, int x, int y, Team team)
    {
        Name = name;
        X = x;
        Y = y;
        Team = team;
    }

    public void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public (int, int) GetAbsolutePosition()
    {
        return Team!.Game.GetPositionForTeam(Team, X, Y);
    }

    public int GetDistanceToBall()
    {
        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        return Math.Abs(dx) + Math.Abs(dy);
    }

    public void MoveTowardsBall()
    {
        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;

        _vx = dx == 0 ? 0 : dx / Math.Abs(dx); // Moves one unit towards ball
        _vy = dy == 0 ? 0 : dy / Math.Abs(dy);
    }

    public void Move()
    {
        _previousX = X;
        _previousY = Y;

        if (Team.GetClosestPlayerToBall() != this)
        {
            _vx = 0;
            _vy = 0;
        }

        if (GetDistanceToBall() <= BallKickDistance)
        {
            Team.SetBallSpeed(_random.Next(-MaxKickSpeed, MaxKickSpeed + 1), _random.Next(-MaxKickSpeed, MaxKickSpeed + 1));
        }

        var newX = X + _vx;
        var newY = Y + _vy;
        var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);
        if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
        {
            X = newX;
            Y = newY;
        }
        else
        {
            _vx = _vy = 0;
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
