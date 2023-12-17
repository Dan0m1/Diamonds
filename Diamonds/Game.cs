namespace Diamonds
{
    class Game
    {
        private Board _gameBoard = new Board(8, 8);
        public static List<int> toCheck = new List<int>();
        public int MovesLeft { get; set; }
        public static int Score { get; set; }

        public Game(int moves = 30)
        {
            MovesLeft = moves;
            Score = 0;
        }
   
        private void EndMove()
        {
            bool fallen = true;
            foreach (int x in toCheck)
            {
                for (int y = 0; y < 8; y++)
                {
                    _gameBoard.Line(x,y);
                }
            }
            toCheck.Clear();
            while (fallen)
            {
                fallen = _gameBoard.DiamondFall(8, 8);
                Thread.Sleep(200);
                Update();
            }
            _gameBoard.Set(8,8, false);
        }

        public bool MakeMove(int xChange, int yChange)
        {
            bool fallen = true;
            _gameBoard.SwapDiamonds(Cursor.X-xChange,Cursor.Y-yChange, Cursor.X, Cursor.Y);
            Cursor._isChosen = false;
            Update();
            
            Thread.Sleep(1000);
            if (!_gameBoard.Line(Cursor.X-xChange,Cursor.Y-yChange) & !_gameBoard.Line(Cursor.X, Cursor.Y))
            {
                _gameBoard.SwapDiamonds(Cursor.X-xChange,Cursor.Y-yChange, Cursor.X, Cursor.Y);
                return false;
            }
            while (fallen)
            {
                fallen = _gameBoard.DiamondFall(8, 8);
                Thread.Sleep(200);
                Update();
            }

            if (!fallen)
            {
                _gameBoard.Set(8,8,false);
                EndMove();
            }

            while (toCheck.Count > 0)
            {
                EndMove();
            }
            Update();
            return true;
        }

        public void Update()
        {
            Console.Clear();
            _gameBoard.Update();
            Console.SetCursorPosition(15,2);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("MOVES LEFT: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{MovesLeft}");
            Console.SetCursorPosition(15, 4);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("SCORE: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Score}");
            Console.SetCursorPosition(0,0);
            Console.ResetColor();
        }

        public void GameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(0,i);
                Console.Write("\u2588");
                Console.SetCursorPosition(23,i);
                Console.Write("\u2588");
            }

            for (int i = 1; i < 23; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("\u2580");
                Console.SetCursorPosition(i, 8);
                Console.Write("\u2584");
            }
            Console.SetCursorPosition(7,3);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Game Over!");
            Console.SetCursorPosition(5,5);
            Console.Write("Your score: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Score}");
            Console.ReadKey();
        }

        public void MoveCursor(int xChange, int yChange)
        {
            if (Cursor.X == 0 && xChange == -1) ;
            else if (Cursor.Y == 0 && yChange == -1) ;
            else if (Cursor.X == 7 && xChange == 1) ;
            else if (Cursor.Y == 7 && yChange == 1) ;
            else
            {
                Cursor.X += xChange;
                Cursor.Y += yChange;
                if (Cursor._isChosen)
                {
                    if (!MakeMove(xChange, yChange))
                    {
                        Cursor.X -= xChange;
                        Cursor.Y -= yChange;
                    }
                    else MovesLeft--;
                }
            }
        }
    }
}