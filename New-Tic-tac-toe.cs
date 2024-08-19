TicTacToe game = new TicTacToe();
game.Start();
class TicTacToe
{
    readonly Players Player1;
    readonly Players Player2;
    Gamelogic Logic;
    Players currentPlayer;
    readonly Boardmap Boardmap;
    bool Gamestate;

    public TicTacToe()
    {
        Boardmap = new Boardmap();
        Player1 = new Players('X');
        currentPlayer = Player1; // To begin with 'X'
        Player2 = new Players('O');
        Gamestate = true;
    }

    public void Start()
    {
        Boardmap.BoardLayout(); // To print out the empty Board
        while (Gamestate)
        {
            Gamestate = PlayerLoop();
        }
    }

    bool PlayerLoop()
    {
        Logic = new Gamelogic(currentPlayer, Boardmap); // //  instantiated in a loop, which might not be the best design choice.but i cant figure out anything else 
        Console.Write($"'{currentPlayer.Symbol}' Player Your Turn: ");
        currentPlayer.Player = int.Parse(Console.ReadLine()!);
        Console.Clear();
        Logic.Gameloop();
        if (Rules.WinningCondition(Boardmap) == true)
        {
            Console.WriteLine(currentPlayer.Symbol + " Won");
            return false;
        }
        if (Rules.Draw(Boardmap) == true)
        {
            return false;
        }
        currentPlayer = currentPlayer == Player1 ? currentPlayer = Player2 : currentPlayer = Player1; // player2 nothing happenes 
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
    readonly Boardmap Boardmap;
    readonly private Players Current;
    public void Gameloop()
    {
        UpdateTile();
        Boardmap.BoardLayout();
    }
    public void UpdateTile()
    {
        for (; ; )
        {
            Rules.Overlap(Current, Boardmap);
            if (Current.Player == 1) { Boardmap.G = Current.Symbol; return; }
            else if (Current.Player == 2) { Boardmap.H = Current.Symbol; return; }
            else if (Current.Player == 3) { Boardmap.I = Current.Symbol; return; }
            else if (Current.Player == 4) { Boardmap.D = Current.Symbol; return; }
            else if (Current.Player == 5) { Boardmap.E = Current.Symbol; return; }
            else if (Current.Player == 6) { Boardmap.F = Current.Symbol; return; }

            else if (Current.Player == 7) { Boardmap.A = Current.Symbol; return; }
            else if (Current.Player == 8) { Boardmap.B = Current.Symbol; return; }
            else if (Current.Player == 9) { Boardmap.C = Current.Symbol; return; }
            else { Rules.NoSpace("Invalid Input", Boardmap, Current); }
        }
    }
    public Gamelogic(Players player, Boardmap boardmap)
    {
        Current = player;
        Boardmap = boardmap;
    }
}

static class Rules
{
    public static bool WinningCondition(Boardmap Boardmap)
    {
        if ((Boardmap.A == Boardmap.B && Boardmap.B == Boardmap.C && Boardmap.C != '~') || //Columb static its same for both as one instance is being shared instead of entire member of class 
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
    public static void Overlap(Players Current, Boardmap Boardmap)
    {
        for (; ; )
        {
            if ((Current.Player == 1 && Boardmap.G != '~') || // Current would be different for different instances if in multiple board running simentaniulsy 
                (Current.Player == 2 && Boardmap.H != '~') ||
                (Current.Player == 3 && Boardmap.I != '~') ||
                (Current.Player == 4 && Boardmap.D != '~') ||
                (Current.Player == 5 && Boardmap.E != '~') ||
                (Current.Player == 6 && Boardmap.F != '~') ||
                (Current.Player == 7 && Boardmap.A != '~') ||
                (Current.Player == 8 && Boardmap.B != '~') ||
                (Current.Player == 9 && Boardmap.C != '~'))
            {
                NoSpace("Space Already Occupied!", Boardmap, Current);
            }
            else { return; }
        }

    }
    public static bool Draw(Boardmap Boardmap) 
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
    public static void NoSpace(string comment, Boardmap boardmap, Players current)
    {
        boardmap.BoardLayout(); // Causing the previous Board To Show 
        Console.WriteLine(comment);
        Console.WriteLine("Try Again '" + current.Symbol + "' Player");
        current.Player = int.Parse(Console.ReadLine()!);
        Console.Clear();
    }
}

class Boardmap 
{

    public char G = '~';
    public char A = '~';
    public char B = '~';
    public char C = '~';
    public char D = '~';
    public char E = '~';
    public char F = '~';
    public char H = '~';
    public char I = '~';

    public void BoardLayout()
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
        { this.A , this.B , this.C },
        { this.D , this.E , this.F },
        { this.G , this.H , this.I },
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
