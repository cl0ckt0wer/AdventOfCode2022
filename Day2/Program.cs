// See https://aka.ms/new-console-template for more information
var input = File.ReadAllLines("input.txt");

var opponentLookup = new Dictionary<string, RPS>();
var playerLookup = new Dictionary<string, RPS>();
var playerWinstate = new Dictionary<string, WinState>();
opponentLookup.Add("A", RPS.Rock);
opponentLookup.Add("B", RPS.Paper);
opponentLookup.Add("C", RPS.Scissors);
playerLookup.Add("X", RPS.Rock);
playerLookup.Add("Y", RPS.Paper);
playerLookup.Add("Z", RPS.Scissors);
playerWinstate.Add("X", WinState.Lose);
playerWinstate.Add("Y", WinState.Draw);
playerWinstate.Add("Z", WinState.Win);

var result = input.Select(x => GetScore(x, opponentLookup, playerLookup)).Sum();
var result2 = input.Select(x => GetScore2(x, opponentLookup, playerWinstate)).Sum();

static int GetScore(string x, Dictionary<string, RPS> opponentLookup, Dictionary<string, RPS> playerLookup)
{
    var game = x.Split(" ");
    bool draw = false;
    bool win = false;
    //Console.WriteLine($"Results {opponentLookup[game[0]]} ,{playerLookup[game[1]]}");

    if (playerLookup[game[1]] != opponentLookup[game[0]])
    {
        if (playerLookup[game[1]] == RPS.Rock && opponentLookup[game[0]] == RPS.Scissors)
            win = true;
        else if (playerLookup[game[1]] == RPS.Paper && opponentLookup[game[0]] == RPS.Rock)
            win = true;
        else if (playerLookup[game[1]] == RPS.Scissors && opponentLookup[game[0]] == RPS.Paper)
            win = true;
    }
    else if (playerLookup[game[1]] == opponentLookup[game[0]])
    {
        //Console.WriteLine($"Draw {game[0]} {game[1]}");
        draw = true;
    }

    var score = 0;
    if (win)
    {
        score = (int)playerLookup[game[1]] + 6;
    }
    else if (draw)
    {
        score = (int)playerLookup[game[1]] + 3;
    }
    else
    {
        score = (int)playerLookup[game[1]];
    }
    //Console.WriteLine($"Oppenent played {game[0]} and I played {game[1]} and I scored {score}");
    return score;
}
static int GetScore2(string x, Dictionary<string, RPS> opponentLookup, Dictionary<string, WinState> playerLookup)
{
    var score = 0;
    var game = x.Split(" ");
    var opponent = opponentLookup[game[0]];
    var winState = playerLookup[game[1]];
   
    RPS player;
    if (winState == WinState.Win)
    {
        player = GetWinningRPS(opponent);
        score = 6 + (int)player;
        
    }
    else if (winState == WinState.Lose)
    {
        player = GetLosingRPS(opponent);
        score = (int)player;
    }
    else
    {
        player = opponent;
        score = 3 + (int)player;
    }
    return score;
}
static RPS GetWinningRPS(RPS opponent)
{
    switch (opponent)
    {
        case RPS.Rock:
            return RPS.Paper;
        case RPS.Paper:
            return RPS.Scissors;
        case RPS.Scissors:
            return RPS.Rock;
        default:
            throw new Exception("Invalid RPS");
    }

}
static RPS GetLosingRPS(RPS opponent)
{
    switch (opponent)
    {
        case RPS.Rock:
            return RPS.Scissors;
        case RPS.Paper:
            return RPS.Rock;
        case RPS.Scissors:
            return RPS.Paper;
        default:
            throw new Exception("Invalid RPS");
    }

}
Console.WriteLine($"Result: {result}");
Console.WriteLine($"Result2: {result2}");
public enum RPS
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}
public enum WinState
{
    Win,
    Lose,
    Draw
}