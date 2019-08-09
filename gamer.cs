using System;

namespace TicTacToe
{
    class Gamer
    {
        private bool IsHuman { get; set; }
        private char Character { get; set; }
        private string Username { get; set; }

        public Gamer()
        {
            IsHuman = true;
            Character = 'X';
        }

        public Gamer(bool checkIfHuman,string username)
        {
            IsHuman = checkIfHuman;
            if (checkIfHuman)
            {
                Character = 'X';
            }
            else
            {
                Character = 'O';
            }
            this.Username = username;
        }

        public Gamer(bool checkIfHuman, char kr,string username)
        {
            IsHuman = checkIfHuman;
            Character = kr;
            this.Username = username;
        }

        public string GetUsername()
        {
            return Username;
        }

        public char GetCharacter()
        {
            return Character;
        }

        public bool GetUserType()
        {
            return IsHuman;
        }

        public string GetGamerMove()
        {
            Console.WriteLine("Enter your move (Press e to exit) :");
            return Console.ReadLine();
        }

        //check gamer wants to go for exit
        //check invalid letter or number
        //check it already has chosen coordinate
        public MoveTypes CheckGamerMove(string move, char[,] array)
        {
            //if user wanna exit the game,return exit situation
            if (move == "E" || move == "e")
            {
                return MoveTypes.Exit;
            }

            //check is it number ,if is not return error
            int y;
            try
            {
                y = Convert.ToInt32(move);
            }
            catch (Exception)
            {
                return MoveTypes.InvalidMove;
            }

            //check is it over limit, if is not return error
            if (Convert.ToInt32(move) > array.Length)
            {
                return MoveTypes.InvalidMove;
            }

            //check is it empty location, if is not return error,if it is return true
            int[] x = FindIndexFromMove(move, array);

            if (array[x[0], x[1]] == ' ')
            {
                array[x[0], x[1]] = Character;
                return MoveTypes.Correct;
            }
            else
            {
                return MoveTypes.InvalidMove;
            }
        }

        //this function is for show numbers like arrays location on console screen 
        //for example array[0,0] => 1 array[0,1] => 2 array[0,2] => 3 array[1,0] => 4
        //if your "move" parameter is 3, the function is going to return 0,2
        //if your "move" parameter is 3, the function is going to return 1,0
        public int[] FindIndexFromMove(string move, char[,] array)
        {
            double boardLength = Math.Sqrt(array.Length);
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    if (Convert.ToString((j + 1) + (boardLength * i)) == move)
                    {
                        return new int[2] { i, j };
                    }
                }
            }
            return null;
        }

        //this function is to generate empty move
        public void GenerateComputerMove(char[,] array)
        {
            int arrayLength = array.Length;
            Random random = new Random();

            while (true)
            {
                int guess = random.Next(1, arrayLength);
                if (CheckGamerMove(Convert.ToString(guess), array) == MoveTypes.Correct)
                {
                    Console.WriteLine("Computer has played for " + guess.ToString());
                    break;
                }
            }
        }
    }
}
