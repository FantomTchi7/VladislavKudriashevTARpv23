namespace Football;

public class Game
{
    public Team HomeTeam { get; }
    public Team AwayTeam { get; }
    public Stadium Stadium { get; }
    public Ball Ball { get; private set; }

    public Game(Team homeTeam, Team awayTeam, Stadium stadium)
    {
        HomeTeam = homeTeam;
        homeTeam.Game = this;
        AwayTeam = awayTeam;
        awayTeam.Game = this;
        Stadium = stadium;
    }

    public void Start()
    {
        Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
        HomeTeam.StartGame(Stadium.Width / 2, Stadium.Height);
        AwayTeam.StartGame(Stadium.Width / 2, Stadium.Height);
    }

    public (int, int) GetPositionForTeam(Team team, int x, int y)
    {
        return team == HomeTeam ? (x, y) : (Stadium.Width - x, Stadium.Height - y);
    }

    public (int, int) GetBallPositionForTeam(Team team)
    {
        return GetPositionForTeam(team, Ball.X, Ball.Y);
    }

    public void SetBallSpeedForTeam(Team team, int vx, int vy)
    {
        if (team == HomeTeam)
        {
            Ball.SetSpeed(vx, vy);
        }
        else
        {
            Ball.SetSpeed(-vx, -vy);
        }
    }

    public void Move()
    {
        HomeTeam.Move();
        AwayTeam.Move();
        Ball.Move();
    }

    public void Display()
    {
        var stadium = new char[Stadium.Height, Stadium.Width];

        for (int i = 0; i < Stadium.Height; i++)
        {
            for (int j = 0; j < Stadium.Width; j++)
            {
                stadium[i, j] = ' ';
            }
        }

        stadium[Ball.Y, Ball.X] = 'O'; // Ball represented by 'O'

        foreach (var player in HomeTeam.Players)
        {
            stadium[player.Y, player.X] = 'H'; // Home team players represented by 'H'
        }

        foreach (var player in AwayTeam.Players)
        {
            stadium[player.Y, player.X] = 'A'; // Away team players represented by 'A'
        }

        for (int i = 0; i < Stadium.Height; i++)
        {
            for (int j = 0; j < Stadium.Width; j++)
            {
                Console.Write(stadium[i, j]);
            }
            Console.WriteLine();
        }
    }
}
