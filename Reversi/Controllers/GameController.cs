using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Controllers
{
    public class GameController
    {
        public int[,] field { get; set; }

        public void doTurn(int x, int y, int turn)
        {
            if (InField(x, y))
            {
                if (field[x, y] == 0)
                {
                    field[x, y] = turn;
                    
                }
                else
                {
                    throw new TurnException();
                }
            }
            else
            {
                throw new TurnException();
            }
        }

        public void fillNewGame()
        {
            //8x8 field
            field = new int[8, 8];

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    //maakt veld met standaard stenen
                    if (i == 3 && j == 3 || i == 4 && j == 4)
                    {
                        field[i, j] = 1;
                    }
                    else if (i == 4 && j == 3 || i == 3 && j == 4)
                    {

                        field[i, j] = 2;
                    }
                    else
                    {
                        field[i, j] = 0;
                    }
                }
            }
        }

        public bool InField(int a, int b)
        {
            return a >= 0 && a < field.GetLength(0) && b >= 0 && b < field.GetLength(1);
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    output += field[i, j].ToString();
                }
                output += "\n";
            }
            Console.WriteLine(output);
            return output;
            //return "<empty>";
        }

        public class TurnException : Exception
        {
            public TurnException() : this("Fout bij uitvoeren van zet")
            {

            }
            public TurnException(string e) : base(e)
            {

            }
        }

    }
}
