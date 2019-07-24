using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSwipper
{
    class GameBoard
    {
        public string[,] gameBoard
        {
            get;
            set;
        }
            
        string[,] mineLocations = new string[10, 10];

        public GameBoard()
        {
            gameBoard = new string[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    gameBoard[i, j] = "*";
                    mineLocations[i, j] = "*";
                }
            }

            PlaceMines(10);
        }

        private void PlaceMines(int numberOfMines)
        {
            Random positionGenerator = new Random();

            for (int i = 0; i < numberOfMines; i++)
            {
                Boolean minePlaced = false;

                while (minePlaced == false)
                {
                    int xCoord = positionGenerator.Next(0, 9);
                    int yCoord = positionGenerator.Next(0, 9);

                    if (mineLocations[xCoord, yCoord] == "*")
                    {
                        mineLocations[xCoord, yCoord] = "X";
                        minePlaced = true;
                    }
                }

            }
        }

        public Boolean PerformSweep(int xCoord, int yCoord)
        {
            Boolean gameOver = false;
            xCoord--;
            yCoord--;

            if (mineLocations[yCoord, xCoord] == "X")
            {
                gameBoard[yCoord, xCoord] = "X";
                SetBoardToGameOver();
                gameOver = true;
            }

            gameBoard[yCoord, xCoord] = SweepSurroundings(yCoord, xCoord).ToString(); 
            return gameOver;
        }

        private void SetBoardToGameOver()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (mineLocations[i, j] == "X")
                        gameBoard[i, j] = "X";
                }
            }
        }

        private int SweepSurroundings(int xCoord, int yCoord)
        {
            int proximityCounter = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int xCheckCoord = xCoord + i;
                    int yCheckCoord = yCoord + j;

                    if (xCheckCoord >= 0 && xCheckCoord <= 9 && yCheckCoord >= 0 && yCheckCoord <= 9)
                    {
                        if (mineLocations[xCheckCoord, yCheckCoord] == "X")
                            proximityCounter++;
                    }
                }
            }

            return proximityCounter;
        }

        public Boolean CheckVictory()
        {
            Boolean victory = true;

            for (int i = 0; i < 10; i++)
            {
                for (int j =0; j <10; j++)
                {
                    if (gameBoard[i, j] == "*")
                        victory = false;
                }
            }

            return victory;
        }
    }
}
