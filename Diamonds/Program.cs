namespace Diamonds
{
    class Program
    {
        private static bool quit = false;

        static void Main()
        {
            Console.CursorVisible = false;
            Game game = new Game();
            game.Update();
            MainLoop(game);
        }

        static void MainLoop(Game game)
         {
             while(!quit && game.MovesLeft > 0)
             {
                 ConsoleKey key = Console.ReadKey(true).Key;
                 if(key == ConsoleKey.Enter) Cursor._isChosen = !Cursor._isChosen;
                 else switch(key)
                 {
                     case ConsoleKey.DownArrow: game.MoveCursor(0, 1);
                         break;
                     case ConsoleKey.UpArrow: game.MoveCursor(0, -1);
                         break;
                     case ConsoleKey.RightArrow: game.MoveCursor(1, 0);
                         break;
                     case ConsoleKey.LeftArrow: game.MoveCursor(-1, 0);
                         break;
                     case ConsoleKey.Q : quit = true;
                         break;
                 }
                 game.Update();
             }
             Thread.Sleep(2500);
             game.GameOver();
         }
    } 
}