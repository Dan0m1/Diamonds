using Diamonds;

namespace Diamonds
{
    class Program
    {
        private const int consoleBoardWidth = 8;
        private const int consoleBoardHight = 8;
        public static bool gameOver = false;
        

        static void Main()
        {
          //  Console.SetWindowSize(consoleBoardWidth, consoleBoardHight);
         //   Console.SetBufferSize(consoleBoardWidth, consoleBoardHight);

            Console.CursorVisible = false;
            Game game = new Game();
            game.Print();
            MainLoop(game);
        }

        static void MainLoop(Game game)
         {
             while(!gameOver)
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
                 }
                 game.Update();
             }
         }
    } 
}