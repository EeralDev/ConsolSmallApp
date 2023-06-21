using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolSmallApp
{
    internal class GuessNumber
    {
        private Random _random = new Random();

        private int _inputPlayer = -1;

        private int _attemptsNumber = 0;

        private int _numberToFind;

        public GuessNumber()
        {
            this._numberToFind = this._random.Next(1, 101);
        }

        public int startGame()
        {
            Console.WriteLine("Dans ce jeux vous devez deviner le nombre auquel la machine pense.");

            while (this._numberToFind != this._inputPlayer)
            {
                Console.WriteLine("Veuillez entrer un nombre.");
                if (Int32.TryParse(Console.ReadLine(), out this._inputPlayer))
                {
                    if (this._numberToFind == this._inputPlayer)
                    {
                        this._attemptsNumber++;
                        Console.WriteLine($"Vous avez réussi à trouver la valeur en {this._attemptsNumber} coup(s).");
                    }
                    else
                    {
                        this._attemptsNumber++;
                        Console.WriteLine(this._numberToFind > this._inputPlayer ?
                            "Le nombre a trouvé est plus grand." : "Le nombre a trouvé est plus petit.");
                    }

                }
                else
                {
                    Console.WriteLine("Vous devez entrer un nombre.");
                }
            }
            return this._attemptsNumber;
        }
    }
}

