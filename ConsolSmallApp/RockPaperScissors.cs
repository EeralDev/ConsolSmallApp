using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolSmallApp
{
    internal class RockPaperScissors
    {
        private Random _random = new Random();

        private string[] _weapon =
        {
            "pierre",
            "papier",
            "ciseaux"                
        };

        private string _playerInput = "-1";

        private string _computerInput = "-1";

        private int _numberOfGame;

        private int _gameWin;

        private int _gameLost;

        public RockPaperScissors() 
        {
            Console.WriteLine("Veuillez entrer le nombre maximum de partie possible. Si le nombre entier est pair une partie seras ajouter pour éviter l'égalité.");
            while (!Int32.TryParse(Console.ReadLine(), out this._numberOfGame))
            { 
                Console.WriteLine("La commande entrée est incorrecte. Veuillez entrer un chiffre.");
            };   
            this._numberOfGame = this._numberOfGame % 2 == 0 ? ++this._numberOfGame : this._numberOfGame ;
            this._gameWin = 0;
            this._gameLost = 0;
        }

        public int startGame() 
        {
            this._gameWin = 0;
            this._gameLost = 0;

            Console.WriteLine($"La partie seras jouer en best of {this._numberOfGame}");

            while (this._gameWin < (this._numberOfGame / 2 + 1) && this._gameLost < (this._numberOfGame / 2 + 1))
            {
                Console.WriteLine("Veuillez entrer Pierre, Papier, ou Ciseaux.");
                this._playerInput = Console.ReadLine().ToLower();
                this._computerInput = this._weapon[this._random.Next(0, 3)];
                Console.WriteLine($"L'ordinateur à jouer {this._computerInput}.");
                switch (this._playerInput)
                {
                    case "pierre":
                        switch (this._computerInput)
                        {
                            case "pierre":
                                Console.WriteLine("Personne ne gagne !");
                                break;
                            case "papier":
                                Console.WriteLine("Vous perdez !");
                                this._gameLost++;
                                break;
                            case "ciseaux":
                                Console.WriteLine("Vous gagnez !");
                                this._gameWin++;
                                break;
                        }
                        break;
                    case "papier":
                        switch (this._computerInput)
                        {
                            case "pierre":
                                Console.WriteLine("Vous gagnez !");
                                this._gameWin++;
                                break;
                            case "papier":
                                Console.WriteLine("Personne ne gagne !");
                                break;
                            case "ciseaux":
                                Console.WriteLine("Vous perdez !");
                                this._gameLost++;
                                break;
                        }
                        break;
                    case "ciseaux":
                        switch (this._computerInput)
                        {
                            case "pierre":
                                Console.WriteLine("Vous perdez !");
                                this._gameLost++;
                                break;
                            case "papier":
                                Console.WriteLine("Vous gagnez !");
                                this._gameWin++;
                                break;
                            case "ciseaux":
                                Console.WriteLine("Personne ne gagne !");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("La commande est incorrecte. Rien ne se passe");
                        break;
                };
            }
            if (this._gameWin > this._gameLost)
            {
                Console.WriteLine("Bravo vous avez gagné !!!");
                return this._gameWin * 3 - this._gameLost;
            }
            else
            {
                Console.WriteLine("Dommage vous avez perdu !!!");
                return 0;
            }

            
        }
    }
}
