using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Controllers
{
    public class FieldController
    {
        public enum Kleur { Empty, White, Black }
        private Kleur[,] field;

        //maakt veld aan voor reversi 8 bij 8
        public FieldController() : this(8, 8) { }
        public FieldController(int x, int y)
        {
            field = new Kleur[x, y];

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    //maakt leeg veld
                    if (i == 3 && j == 3 || i == 4 && j == 4)
                    {
                        field[i, j] = Kleur.White;
                    }
                    else if (i == 4 && j == 3 || i == 3 && j == 4)
                    {
                        field[i, j] = Kleur.Black;
                    }
                    else
                    {
                        field[i, j] = Kleur.Empty;
                    }
                }
            }
        }

        public void DoTurn(int x, int y , Kleur kleur)
        {
            // 4b
            if (InField(x, y))
            {
                if (field[x, y] == Kleur.Empty)
                {
                    field[x, y] = kleur;
                    //todo logica om stenen tussen de andere stenen veranderen in juiste kleur
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

        public bool InField(int a, int b)
        {
            return a >= 0 && a < field.GetLength(0) && b >= 0 && b < field.GetLength(1);
        }

        public static string fieldToString(Kleur kleur)
        {
            string KleurgString = "";
            switch (kleur)
            {
                case Kleur.Empty:
                    KleurgString = "0";
                    break;
                case Kleur.White:
                    KleurgString = "1";
                    break;
                case Kleur.Black:
                    KleurgString = "2";
                    break;
            }

            return KleurgString;
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    output += fieldToString(field[i, j]);
                }
                output += "\n";
            }
            Console.WriteLine(output);
            return output;
            //return "<empty>";
        }

        public class TurnException : Exception
        {
            public TurnException() : this("Fout bij zetten van zet")
            {

            }
            public TurnException(string e) : base(e)
            {

            }
        }



    }
}
