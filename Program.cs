using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{

    class Program
    {
        public static int boardLength;
        public static char[,] boardArray;
        public static char playLetter;
        public static string username;
        //if user want to play saved game
        public static bool oldGame = false;
        //if user choose exit the game without saving the game
        public static bool gameIsOver = false;
        public static Gamer player_, computer_;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Yellow;
            #region choose game type (new or saved)
            while (true)
            {
                Console.WriteLine("Press \"y\" for new game, press \"k\" for saved game:");
                string choice = Console.ReadLine();
                if (choice == "y")
                {
                    break;
                }
                else if (choice == "k")
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader("oldgame.txt"))
                        {
                            username = reader.ReadLine();
                            playLetter = Convert.ToChar(reader.ReadLine());

                            player_ = new Gamer(true, playLetter, username);

                            if (player_.GetCharacter() == 'x')
                                computer_ = new Gamer(false, 'O', "Computer");
                            else
                                computer_ = new Gamer(false, 'X', "Computer");

                            boardLength = Convert.ToInt32(reader.ReadLine());
                            boardArray = new char[boardLength, boardLength];

                            for (int i = 0; i < boardLength; i++)
                            {
                                for (int j = 0; j < boardLength; j++)
                                {
                                    boardArray[i, j] = Convert.ToChar(reader.ReadLine());
                                }
                            }

                            oldGame = true;
                            break;
                        }

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("There is not saved game.");
                    }
                }
                else
                {
                    Console.WriteLine("You pressed wrong key,would you like to try again");
                }
            }
            #endregion

            Gamer player = new Gamer();
            Gamer computer = new Gamer();

            if (!oldGame)
            {
                Console.WriteLine("Your name please:");
                username = Console.ReadLine();

                #region game board settings
                while (true)
                {
                    Console.WriteLine("Game board length value (Please do not enter except 3,5,7) :");
                    string s = Console.ReadLine();
                    if (s == "3" || s == "5" || s == "7")
                    {
                        boardLength = Convert.ToInt32(s);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Bad letter has been chosen, please try again!");
                    }
                }
                #endregion

                boardArray = new char[boardLength, boardLength];

                for (int i = 0; i < boardLength; i++)
                {
                    for (int j = 0; j < boardLength; j++)
                    {
                        boardArray[i, j] = ' ';
                    }
                }

                #region choose game character (x,o) and create player classes(player and computer)
                while (true)
                {
                    Console.WriteLine("Please choose your letter to play, press X or O \n(If don't, please press C)");
                    string s = Console.ReadLine();
                    playLetter = Convert.ToChar(s);
                    if (s == "X" || s == "x")
                    {
                        player_ = new Gamer(true, playLetter, username);
                        computer_ = new Gamer(false, 'O', "Computer");
                        break;
                    }
                    else if (s == "O" || s == "o")
                    {
                        player_ = new Gamer(true, playLetter, username);
                        computer_ = new Gamer(false, 'X', "Computer");
                        break;
                    }
                    else if (s == "C" || s == "c")
                    {
                        player_ = new Gamer(true, username);
                        computer_ = new Gamer(false, "Computer");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Bad letter has been chosen, please try again!");
                    }
                }
                #endregion
            }

            else
            {
                Console.WriteLine("Welcome again " + username + " the game has continued successfully");
            }

            GameBoard board = new GameBoard(boardArray);
            player = player_;
            computer = computer_;

            #region the game has been started
            while (true)
            {
                board.PrintGameBoard();

                while (true)
                {
                    string s = player.GetGamerMove();
                    MoveTypes moveTypes = player.CheckGamerMove(s, boardArray);
                    if (moveTypes == MoveTypes.Correct)
                    {
                        board.PrintGameBoard();
                        break;
                    }
                    else if (moveTypes == MoveTypes.Exit)
                    {
                        Console.WriteLine("you have chosen the quit.Press k if you would like to save the game.");
                        string sss = Console.ReadLine();
                        if (sss == "K" || sss == "k")
                        {
                            SaveTheGame(boardArray, username, boardLength, player.GetCharacter());
                        }

                        gameIsOver = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(moveTypes.GetDescription());
                    }
                }

                if (gameIsOver)
                {
                    break;
                }

                if (board.Winner(player.GetCharacter()))
                {
                    Console.WriteLine(player.GetUsername() + " won");
                    DeleteSavedGame();
                    break;
                }

                if (board.CheckDraw())
                {
                    Console.WriteLine("Game in Draw");
                    DeleteSavedGame();
                    break;
                }

                computer.GenerateComputerMove(boardArray);

                if (board.Winner(computer.GetCharacter()))
                {
                    Console.WriteLine(computer.GetUsername() + " won");
                    DeleteSavedGame();
                    break;
                }

                if (board.CheckDraw())
                {
                    Console.WriteLine("Game in Draw");
                    DeleteSavedGame();
                    break;
                }
            }
            #endregion

            //the game is over
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        //save game .txt file
        static void SaveTheGame(char[,] array, string username, int boardLength, char character)
        {
            using (StreamWriter file = new StreamWriter("oldgame.txt"))
            {
                file.WriteLine(username);
                file.WriteLine(character);
                file.WriteLine(boardLength);
                int limit = (int)Math.Sqrt(array.Length);
                for (int i = 0; i < limit; i++)
                {
                    for (int j = 0; j < limit; j++)
                    {
                        file.WriteLine(array[i, j]);
                    }
                }
            }

            Console.WriteLine("The game has saved");
        }

        static void DeleteSavedGame()
        {
            if (File.Exists("oldgame.txt"))
            {
                File.Delete("oldgame.txt");
            }
        }
    }
}
