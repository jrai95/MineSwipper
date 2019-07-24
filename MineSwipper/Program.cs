using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSwipper
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard gameBoard = new GameBoard();
            Display gameDisplay = new Display(gameBoard);
            gameDisplay.Play();
        }
    }
}
