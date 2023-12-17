namespace Diamonds
{
    class Board
    {
        private readonly Random _random = new Random();
        private readonly ConsoleColor[] _color = {ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.DarkGreen, ConsoleColor.Magenta, ConsoleColor.DarkYellow};
        private Diamond?[,] _gameBoard;
        public Board(int width, int height)
        {
            _gameBoard = new Diamond[width, height];
            Set(width, height, true);
            Print();
        }

        public void Update()
        {
            Print();
        }

        public void RemoveDiamonds(List<Diamond> diamonds)
        {
            foreach (Diamond diamond in diamonds)
            {
                _gameBoard[diamond.X, diamond.Y] = null;
                Game.Score += 10;
            }
        }
        
        public void SwapDiamonds(int x1, int y1, int x2, int y2)
        {
            Diamond temp = _gameBoard[x1, y1];
            _gameBoard[x1, y1] = _gameBoard[x2, y2];
            _gameBoard[x1, y1].X = x1;
            _gameBoard[x1, y1].Y = y1;
            _gameBoard[x2, y2] = temp;
            _gameBoard[x2, y2].X = x2;
            _gameBoard[x2, y2].Y = y2;

        }

        public void Set(int width, int height, bool init)
        {
            int rnd;
            bool found = false;
        for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    while (!found && _gameBoard[x,y] is null)
                    {
                        rnd = _random.Next(_color.Length);
                        if (CheckLine(x, y, rnd, 1, 0).Count < 2 &&
                            CheckLine(x, y, rnd, 0, 1).Count < 2)
                        {
                            found = true;
                            _gameBoard[x, y] = new Diamond(x, y, _color[rnd], rnd);
                            if(!init) Game.toCheck.Add(x);
                        }
                    }
                    
                    found = false;
                }
            }
        }
        public void Print()
        {
            for (int x = 0; x < _gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < _gameBoard.GetLength(1); y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = _gameBoard[x, y] is null ? ConsoleColor.White : _gameBoard[x, y].Color;
                    if (y == Cursor.Y && x == Cursor.X) Console.BackgroundColor = Cursor.Color;
                    Console.Write(_gameBoard[x, y] is null ? " " : _gameBoard[x, y].Id + 1);
                    Console.ResetColor();
                }
            }
        }

        private List<Diamond> CheckLine(int x, int y, int id, int stepX, int stepY)
        {
            List<Diamond> diamonds = new List<Diamond>();
            for (int i = 1; i < _gameBoard.GetLength(0); i++)
            {
                if (x + i * stepX < _gameBoard.GetLength(0) && 
                    y + i * stepY < _gameBoard.GetLength(1) &&
                    _gameBoard[x + i * stepX, y + i * stepY] != null &&
                    _gameBoard[x + i * stepX, y + i * stepY].Id == id)
                {
                    diamonds.Add(_gameBoard[x + i * stepX, y + i * stepY]);
                }
                else break;
            }
            for (int i = 1; i < _gameBoard.GetLength(0); i++)
            {
                if (x - i * stepX >= 0 && 
                    y - i * stepY >= 0 &&
                    _gameBoard[x - i * stepX, y - i * stepY] != null &&
                    _gameBoard[x - i * stepX, y - i * stepY].Id == id)
                {
                    diamonds.Add(_gameBoard[x - i * stepX, y - i * stepY]);
                }
                else break;
            }
            return diamonds;
        }

        public bool Line(int x, int y)
        {
            List<Diamond> match1 = new List<Diamond>();
            List<Diamond> match = new List<Diamond>();

            if (_gameBoard[x, y] == null) return false;
            
            match = CheckLine(x, y,_gameBoard[x,y].Id ,1, 0);
            if(match.Count > 1) match1.AddRange(match);
            match = CheckLine(x, y,_gameBoard[x,y].Id ,0, 1);
            if(match.Count > 1) match1.AddRange(match);

            match = new List<Diamond>();
            if (match1.Count > 1)
            {
                match.AddRange(match1);
                match.Add(_gameBoard[x, y]);
            }
            if (match.Count < 3) return false;
            RemoveDiamonds(match);
            Update();
            return true;
        } 
        public bool DiamondFall(int width, int height)
        {
            bool hasFallen = false;
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width - 1; y++)
                {
                    if (_gameBoard[x, y] != null && _gameBoard[x, y + 1] == null)
                    {
                        _gameBoard[x, y + 1] = _gameBoard[x, y];
                        _gameBoard[x, y + 1].Y = y + 1;
                        _gameBoard[x, y] = null;
                        hasFallen = true;
                        Game.toCheck.Add(x);
                        break;
                    }
                }
            }

            return hasFallen;
        }
    }
}