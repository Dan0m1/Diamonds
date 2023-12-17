namespace Diamonds;

class Cursor
{
    public static int X { get; set; } 
    public static int Y { get; set; }

    public static ConsoleColor Color
    {
        get
        {
            if (_isChosen) return ConsoleColor.DarkGray;
            return ConsoleColor.DarkCyan;
        }
    }
    public static bool _isChosen = false;

    public Cursor(int x, int y)
    {
        X = x;
        Y = y;
    }
}