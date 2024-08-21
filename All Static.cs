TicTacToe game = new TicTacToe();
game.Start();

class TicTacToe
{
    readonly Players Player1;
    readonly Players Player2;
    public static Players CurrentPlayer { get; set; }
    bool Gamestate;

    public TicTacToe()
    {
        CurrentPlayer = Player1 = new Players('X');  // BEGIN WITH X 
        Player2 = new Players('O');
        Gamestate = true;
    }

    public void Start()
    {
        Boardmap.BoardLayout(); // To print out the empty Board
        while (Gamestate)//-----------------------------------------------------------RESPONCIBLE FOR LOOPING 
        {
            Gamestate = LoopPlayer(TicTacToe.CurrentPlayer);
        }
        TicTacToe.CurrentPlayer = Player2;
    }
    bool LoopPlayer(Players Current)
    {
        Console.Write($"'{Current.Symbol}' Player Your Turn: ");
        Current.Player = int.Parse(Console.ReadLine()!);
        Console.Clear();
        Gamelogic.Gameloop();

        if (Rules.WinningCondition() == true)
        {
            Console.WriteLine(Current.Symbol + " Won");
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
    public int Player { get; set; }  // Read ( return ) player 1 and write ( = value) Player 1  --------------------------------------------------REMOVE THIS WHEN GIVING ON DISCORD
    public char Symbol { get; private set; }

    public Players(char symbol)
    {
        Symbol = symbol;
    }
}
static class Gamelogic
{
    public static void Gameloop()
    {
        UpdateTile(TicTacToe.CurrentPlayer);
        Boardmap.BoardLayout();
    }
    public static void UpdateTile(Players Current)
    {
        for (; ; )
        {
            Rules.Overlap(TicTacToe.CurrentPlayer);
            if (Current.Player == 1) { Boardmap.G = Current.Symbol; return; }
            else if (Current.Player == 2) { Boardmap.H = Current.Symbol; return; }
            else if (Current.Player == 3) { Boardmap.I = Current.Symbol; return; }
            else if (Current.Player == 4) { Boardmap.D = Current.Symbol; return; }
            else if (Current.Player == 5) { Boardmap.E = Current.Symbol; return; }
            else if (Current.Player == 6) { Boardmap.F = Current.Symbol; return; }

            else if (Current.Player == 7) { Boardmap.A = Current.Symbol; return; }
            else if (Current.Player == 8) { Boardmap.B = Current.Symbol; return; }
            else if (Current.Player == 9) { Boardmap.C = Current.Symbol; return; }
            else { Rules.NoSpace("Invalid Input", TicTacToe.CurrentPlayer); }
        }
    }
}
static class Rules
{
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
    public static void Overlap(Players Current)
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
                NoSpace("Space Already Occupied!", TicTacToe.CurrentPlayer);
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
    public static void NoSpace(string comment, Players Current)
    {
        Boardmap.BoardLayout(); // Causing the previous Board To Show 
        Console.WriteLine(comment);
        Console.WriteLine("Try Again '" + Current.Symbol + "' Player");
        Current.Player = int.Parse(Console.ReadLine()!);
        Console.Clear();
    }
}

static class Boardmap //just figured out the class could be made static too( Not intentiallly thought of doing so)
{

    public static char G = '~';// the reason i used static is beacuse A is shared to both players and effect both gameplay ifone fills it   
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
            if (Tile[columb, row] == 'X') { Console.ForegroundColor = ConsoleColor.Green; } // Cannot use PLayer.Symbol as The Method is Static

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
