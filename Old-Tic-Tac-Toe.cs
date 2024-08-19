TicTacToe game = new TicTacToe();
game.Start();

class TicTacToe
{
    readonly Players Player1;
    readonly Players Player2;
    private Players CurrentPlayer { get; set; }
    Rules Rules;
    Gamelogic Logic;
    bool Gamestate;

    public TicTacToe()
    {
        Player1 = new Players('X');
        CurrentPlayer = Player1; // BEGIN WITH X 
        Player2 = new Players('O');
        Gamestate = true;
    }

    public void Start()
    {
        Boardmap.BoardLayout(); // To print out the empty Board
        while (Gamestate)
        {
           Gamestate = LoopPlayer();
        }
    }
    bool LoopPlayer()
    {
        Rules = new Rules(CurrentPlayer); //  instantiated in a loop, which might not be the best design choice.but i cant figure out 
        Logic = new Gamelogic(CurrentPlayer, Rules);
        Console.Write($"'{CurrentPlayer.Symbol}' Player Your Turn: ");
        CurrentPlayer.Player = int.Parse(Console.ReadLine()!);
        Console.Clear();
        Logic.Gameloop();

        if (Rules.WinningCondition() == true)
        {
            Console.WriteLine(CurrentPlayer.Symbol + " Won");
            return false;
        }
        if (Rules.Draw() == true)
        {
            return false;
        }
        CurrentPlayer = CurrentPlayer == Player1 ? CurrentPlayer = Player2 : CurrentPlayer = Player1; //Curretn player2 nothing 
        return true;
    }
}
class Players
{
    public int Player { get; set; } 
    public char Symbol { get; private set; }

    public Players(char symbol)
    {
        Symbol = symbol;
    }
}
class Gamelogic
{
    readonly private Players Current;
    readonly private Rules Rules;
    public void Gameloop()
    {
        UpdateTile();
        Boardmap.BoardLayout();
    }
    public void UpdateTile()
    {
        for (; ; )
        {
            Rules.Overlap();
            if (Current.Player == 1) { Boardmap.G = Current.Symbol; return; }
            else if (Current.Player == 2) { Boardmap.H = Current.Symbol; return; }
            else if (Current.Player == 3) { Boardmap.I = Current.Symbol; return; }
            else if (Current.Player == 4) { Boardmap.D = Current.Symbol; return; }
            else if (Current.Player == 5) { Boardmap.E = Current.Symbol; return; }
            else if (Current.Player == 6) { Boardmap.F = Current.Symbol; return; }

            else if (Current.Player == 7) { Boardmap.A = Current.Symbol; return; }
            else if (Current.Player == 8) { Boardmap.B = Current.Symbol; return; }
            else if (Current.Player == 9) { Boardmap.C = Current.Symbol; return; }
            else { Rules.NoSpace("Invalid Input"); }
        }
    }
    public Gamelogic(Players player, Rules rules)
    {
        Current = player;
        Rules = rules;
    }
}

class Rules
{
    public Players Current;
    public static bool WinningCondition()
    {
        if ((Boardmap.A == Boardmap.B && Boardmap.B == Boardmap.C && Boardmap.C != '~') || //Columbs
            (Boardmap.D == Boardmap.E && Boardmap.E == Boardmap.F && Boardmap.F != '~') ||
            (Boardmap.G == Boardmap.H && Boardmap.H == Boardmap.I && Boardmap.I != '~') ||

            (Boardmap.A == Boardmap.D && Boardmap.D == Boardmap.G && Boardmap.G != '~') || //Rows
            (Boardmap.B == Boardmap.E && Boardmap.E == Boardmap.H && Boardmap.H != '~') ||
            (Boardmap.C == Boardmap.F && Boardmap.F == Boardmap.I && Boardmap.I != '~') ||

            (Boardmap.A == Boardmap.E && Boardmap.E == Boardmap.I && Boardmap.I != '~') || //Diagnals
            (Boardmap.C == Boardmap.E && Boardmap.E == Boardmap.G && Boardmap.G != '~'))
        {
            return true;
        }
        return false;
    }
    public void Overlap()
    {
        for (; ; )
        {
            if ((Current.Player == 1 && Boardmap.G != '~') ||
                (Current.Player == 2 && Boardmap.H != '~') ||
                (Current.Player == 3 && Boardmap.I != '~') ||
                (Current.Player == 4 && Boardmap.D != '~') ||
                (Current.Player == 5 && Boardmap.E != '~') ||
                (Current.Player == 6 && Boardmap.F != '~') ||
                (Current.Player == 7 && Boardmap.A != '~') ||
                (Current.Player == 8 && Boardmap.B != '~') ||
                (Current.Player == 9 && Boardmap.C != '~'))
            {
                NoSpace("Space Already Occupied!");
            }
            else { return; }
        }

    }
    public static bool Draw() //draw as static because its same for both players
    {
        if (Boardmap.A != '~' && Boardmap.B != '~' && Boardmap.C != '~' &&
             Boardmap.D != '~' && Boardmap.E != '~' && Boardmap.F != '~' &&
             Boardmap.G != '~' && Boardmap.H != '~' && Boardmap.I != '~')
        {
            Console.WriteLine("The game is Draw!");
            return true; ;
        }
        return false;
    }
    public Rules(Players player)
    {
        Current = player;
    }

    public void NoSpace(string comment)
    {
        Boardmap.BoardLayout(); // Causing the previous Board To Show 
        Console.WriteLine(comment);
        Console.WriteLine("Try Again '" + Current.Symbol + "' Player");
        Current.Player = int.Parse(Console.ReadLine()!);
        Console.Clear();
    }
}

static class Boardmap 
{

    public static char G = '~';// the reason i used static is beacuse A is shared to both players   
    public static char A = '~';
    public static char B = '~';
    public static char C = '~';
    public static char D = '~';
    public static char E = '~';
    public static char F = '~';
    public static char H = '~';
    public static char I = '~';

    public static void BoardLayout()
    {
        char[,] Tile;
        int row;
        int columb;
        void SymbolColor()
        {
            if (Tile[columb, row] == 'X') { Console.ForegroundColor = ConsoleColor.Green; } 

            else if (Tile[columb, row] == 'O') { Console.ForegroundColor = ConsoleColor.Red; }

            else { Console.ForegroundColor = ConsoleColor.White; }
        } // inner method becuase only being used / control by Boardlayout 
        Tile = new char[3, 3]
        {
        { Boardmap.A , Boardmap.B , Boardmap.C },
        { Boardmap.D , Boardmap.E , Boardmap.F },
        { Boardmap.G , Boardmap.H , Boardmap.I },
        };
        {
            for (columb = 0; columb < Tile.GetLength(0); columb++)
            {
                for (row = 0; row < Tile.GetLength(1); row++)
                {
                    SymbolColor();
                    Console.Write(Tile[columb, row]);
                    Console.ForegroundColor = ConsoleColor.White;

                    if (row < Tile.GetLength(1) - 1)
                    {
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();
                if (columb < Tile.GetLength(0) - 1)
                {
                    Console.WriteLine("--+---+--"); // it does this and then leaves line thats why writing console.Writeline Earlier is important 
                }
            }
            Console.WriteLine(); // To leave Space when called
        }
    }
}
