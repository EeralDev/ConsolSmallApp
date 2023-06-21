using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolSmallApp
{
    internal class Pendu
    {
        private Random _random = new Random();


        private string[] _wordList =
        {
            "PIERRE",
            "PAPIER",
            "CISEAUX",
            "JEUX",
            "AMUSANT",
            "DROLE",
            "CONSOLE",
            "CINEMA"
        };

        private int _numberOfLife;

        string _wordToGuess;

        StringBuilder _displayWord;

        public Pendu() 
        {
            Console.WriteLine("Veuillez entrer le nombre de vie avec lequelle vous voulez jouer.");
            while (!Int32.TryParse(Console.ReadLine(), out this._numberOfLife))
            {
                Console.WriteLine("La commande entrée est incorrecte. Veuillez entrer un chiffre.");
            };

            this._wordToGuess = this._wordList[this._random.Next(0, this._wordList.Length - 1)];
            this._displayWord = new StringBuilder(new string('*', this._wordToGuess.Length));
        }

        public int startGame() 
        {
            while (this._numberOfLife != 0 && this._displayWord.ToString().Contains('*'))
            {
                Console.WriteLine($"Mot à deviner : {this._displayWord}");
                Console.WriteLine($"Nombre de vie : {this._numberOfLife}");
                Console.WriteLine("Veuillez entrer un charactère.");
                string input = Console.ReadLine().ToUpper();
                if (input.Length == 1)
                {
                    List<int> nuberOfIndex = new List<int>();
                    for (int i = this._wordToGuess.IndexOf(input); i > -1; i = this._wordToGuess.IndexOf(input, i + 1))
                    {
                        nuberOfIndex.Add(i);
                    }
                    if (nuberOfIndex.Count() != 0)
                    {
                        foreach (int index in nuberOfIndex)
                        {
                            this._displayWord[index] = input[0];
                        }
                    }
                    else
                    {
                        this._numberOfLife = --this._numberOfLife;
                    }
                }
                else
                {
                    Console.WriteLine("Veuillez entrer un seul charactère.");
                }
            }

            Console.WriteLine(this._numberOfLife > 0 ? $"Vous avez gagner avec ${this._numberOfLife}. vie restante" : "Vous avez juste perdue en faite ...");

            return this._numberOfLife + this._wordToGuess.Length * 2;
        }
    }
}
