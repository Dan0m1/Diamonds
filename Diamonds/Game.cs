using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamonds
{
    class Game
    {
        private Board _gameBoard = new Board(8, 8);
        public void Print()
        {
            _gameBoard.Print();
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
            _gameBoard.Set(8,8);
            Update();
            return true;
        }

        public void Update()
        {
            Console.Clear();
            _gameBoard.Update();
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
                }
            }
        }
    }
}