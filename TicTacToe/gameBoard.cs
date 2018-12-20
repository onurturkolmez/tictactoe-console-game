using System;

namespace TicTacToe
{
    class GameBoard
    {
        public char[,] boardArray;
        public GameBoard()
        {
            int boardLength = (int)Math.Sqrt(boardArray.Length);
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    boardArray[i, j] = ' ';
                }
            }
        }

        public GameBoard(char[,] gameBoard)
        {
            boardArray = gameBoard;
        }

        public char[,] GetGameBoard()
        {
            return boardArray;
        }

        public void PrintGameBoard()
        {
            double boardLength = Math.Sqrt(boardArray.Length);

            for (int k = 0; k < boardLength; k++)
            {
                Console.Write("_______");
            }
            Console.Write("\n");

            for (int i = 0; i < boardLength; i++)
            {

                for (int j = 0; j < boardLength; j++)
                {
                    bool shift = true;
                    string s = "";

                    if (boardArray[i, j] == ' ')
                    {
                        if ((j + 1) + (boardLength * i) > 9)
                        {
                            shift = false;
                        }

                        s = Convert.ToString((j + 1) + (boardLength * i));
                    }

                    else
                        s = Convert.ToString(boardArray[i, j]);

                    Console.Write("  ");
                    if (shift)
                        Console.Write(" ");

                    Console.Write(s + "  |");
                }

                Console.Write("\n");
                for (int k = 0; k < boardLength; k++)
                {
                    Console.Write("______|");
                }
                Console.Write("\n");
            }
        }

        public bool Winner(char character)
        {
            int boardLength = (int)Math.Sqrt(boardArray.Length);

            //check vertical equality
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength - 2; j++)
                {
                    if (boardArray[i, j] == boardArray[i, j + 1] && boardArray[i, j] == boardArray[i, j + 2] && boardArray[i, j] == character)
                    {
                        return true;
                    }
                }
            }

            //check horizontal equality
            for (int i = 0; i < boardLength - 2; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    if (boardArray[i, j] == boardArray[i + 1, j] && boardArray[i, j] == boardArray[i + 2, j] && boardArray[i, j] == character)
                    {
                        return true;
                    }
                }
            }

            // \\left cross curves equality
            for (int i = 0; i < boardLength - 2; i++)
            {
                for (int j = 0; j < boardLength - 2; j++)
                {
                    //middle area
                    if (boardArray[i, j] == boardArray[i + 1, j + 1] && boardArray[i, j] == boardArray[i + 2, j + 2] && boardArray[i, j] == character)
                    {
                        return true;
                    }

                    //bottom cross area
                    if (i + 3 < boardLength)
                    {
                        if (boardArray[i + 1, j] == boardArray[i + 2, j + 1] && boardArray[i + 1, j] == boardArray[i + 3, j + 2] && boardArray[i + 1, j] == character)
                        {
                            return true;
                        }
                    }

                    //top cross area
                    if (j + 3 < boardLength)
                    {
                        if (boardArray[i, j + 1] == boardArray[i + 1, j + 2] && boardArray[i, j + 1] == boardArray[i + 2, j + 3] && boardArray[i, j + 1] == character)
                        {
                            return true;
                        }
                    }
                }
            }

            // //right cross curves equality
            for (int i = 0; i < boardLength - 2; i++)
            {
                for (int j = boardLength - 1; j > 0; j--)
                {
                    //middle and bottom areas
                    if (j - 2 >= 0)
                    {
                        if (boardArray[i, j] == boardArray[i + 1, j - 1] && boardArray[i, j] == boardArray[i + 2, j - 2] && boardArray[i, j] == character)
                        {
                            return true;
                        }
                    }

                    //top areas
                    if (j - 3 >= 0)
                    {
                        if (boardArray[i, j - 1] == boardArray[i + 1, j - 2] && boardArray[i, j - 1] == boardArray[i + 2, j - 3] && boardArray[i, j - 1] == character)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //if draw return true ,else return false
        //if there is even an unplayed coordinate, it means the game is still going on.
        //if all area has been got played ,it means the game in draw
        public bool CheckDraw()
        {
            double boardLength = Math.Sqrt(boardArray.Length);

            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    if (boardArray[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
