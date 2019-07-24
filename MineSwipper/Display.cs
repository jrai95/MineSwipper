using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSwipper
{
    class Display
    {
        private GameBoard gameBoard;

        public Display(GameBoard gameBoard)
        {
            PrintHeader();
            this.gameBoard = gameBoard;
        }

        private void PrintGameBoard()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("  1\t2\t3\t4\t5\t6\t7\t8\t9\t10");

            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"{i + 1} ");
                Console.ForegroundColor = ConsoleColor.White;

                for (int j = 0; j < 10; j++)
                {
                    if (gameBoard.gameBoard[i, j] == "X")
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (gameBoard.gameBoard[i, j] == "0")
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    else if (gameBoard.gameBoard[i, j] != "*")
                        Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write($"{gameBoard.gameBoard[i, j]}\t");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write("\n\n");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Play()
        {
            Console.WriteLine("\tWelcome to MineSwipper!\n\tTo Play, enter a selected coordinate in the format \"x,y\"\n");
            Boolean gameOver = false;

            while (!gameOver)
            {
                gameOver = GuessCoordinate();

                if (gameOver)
                {
                    Console.Clear();
                    PrintError("Game Over! You hit a mine!");
                    PrintGameBoard();
                    break;
                }
                else if (gameBoard.CheckVictory())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Congratulations! You have swept all the mines!");
                    break;
                }

                Console.Clear();
                PrintHeader();
            }

        }

        private Boolean GuessCoordinate()
        {
            Boolean validCoord = false;
            int xCoord = -1;
            int yCoord = -1;

            while (!validCoord)
            {
                PrintGameBoard();
                Console.Write("Sweep co-ordinate: ");
                String coord = Console.ReadLine();
                validCoord = HashCoord(coord, out xCoord, out yCoord);

                if (!validCoord)
                {
                    PrintHeader();
                    PrintError("Error! The entered co-ordinate was not valid!");
                }
            }

            return gameBoard.PerformSweep(xCoord, yCoord);
        }

        private Boolean HashCoord(string coord, out int x, out int y)
        {
            string[] coordSplit = coord.Split(',');
            x = 0;
            y = 0;

            if (coordSplit.Count() != 2)
                return false;

            if (!int.TryParse(coordSplit[0], out x))
                return false;

            if (!int.TryParse(coordSplit[1], out y))
                return false;

            if (x < 0 || x > 10 || y < 0 || y > 10)
                return false;

            return true;
        }

        private void PrintError(string error)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void PrintHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(@"
             __  __  ____  _  _  ____  ___  _    _  ____  ____  ____  ____  ____ 
            (  \/  )(_  _)( \( )( ___)/ __)( \/\/ )(_  _)(  _ \(  _ \( ___)(  _ \
             )    (  _)(_  )  (  )__) \__ \ )    (  _)(_  )___/ )___/ )__)  )   /
            (_/\/\_)(____)(_)\_)(____)(___/(__/\__)(____)(__)  (__)  (____)(_)\_)
            ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(""); // Resets the caret
        }
    }
}
