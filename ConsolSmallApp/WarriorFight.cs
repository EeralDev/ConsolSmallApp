using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsolSmallApp
{
    internal class WarriorFight
    {
        private Random _random = new Random();

        private Warrior _opponent;

        private Warrior _hero;

        private int _playerInput = -1 ;

        public WarriorFight ()
        {
            _opponent = new Warrior(this._random.Next(25,70), this._random.Next(25, 70), this._random.Next(25, 70), this._random.Next(25, 70));
            _hero = new Warrior();
        }

        public int startGame ()
        {
            do
            {
                Console.WriteLine("Choisissez L'action à faire : ");
                Console.WriteLine("1/ Attaque physique");
                Console.WriteLine("2/ Attaque magique");
                Console.WriteLine("3/ Concentration");
                Console.WriteLine("4/ Analyser la situation");
                if (Int32.TryParse(Console.ReadLine(), out this._playerInput) && this._playerInput < 5 && this._playerInput > 0)
                {
                    switch (this._playerInput)
                    {
                        case 1:
                            this._opponent.receiveDamage(this._hero.physicalMove());
                            break;
                        case 2:
                            this._opponent.receiveDamage(this._hero.magicMove());
                            break;
                        case 3:
                            this._hero.focusEnergy();
                            break;
                        case 4:
                            Console.WriteLine("................");
                            Console.WriteLine("Héro :");
                            Console.WriteLine($"L'attaque du Héro est {this._hero.Attack}.");
                            Console.WriteLine($"La défense du Héro est {this._hero.Defense}.");
                            Console.WriteLine($"Le Héro a {this._hero.LifePoints} points de vie.");
                            Console.WriteLine($"Le Héro a {this._hero.ManaPoints} points de mana.");
                            Console.WriteLine("................");
                            Console.WriteLine("Adversaire :");
                            Console.WriteLine($"L'attaque de l'adversaire est {this._opponent.Attack}.");
                            Console.WriteLine($"La défense de l'adversaire est {this._opponent.Defense}.");
                            Console.WriteLine($"L'adversaire à {this._opponent.LifePoints} points de vie.");
                            Console.WriteLine($"L'adversaire à {this._opponent.ManaPoints} points de mana.");
                            Console.WriteLine("................");
                            break;
                    }
                    switch (this._random.Next(1, 6))
                    {
                        case 1:
                            Console.WriteLine("Votre adversaire fait une attaque physique !!! ");
                            this._hero.receiveDamage(this._opponent.physicalMove());
                            break;
                        case 2:
                            Console.WriteLine("Votre adversaire fait une attaque magique !!! ");
                            this._hero.receiveDamage(this._opponent.magicMove());
                            break;
                        case 3:
                            Console.WriteLine("Votre adversaire se concentre.");
                            this._opponent.focusEnergy();
                            break;
                        default:
                            Console.WriteLine("Votre adversaire s'emmèle les pinceaux !!! Rien ne se passe.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("La commande n'est pas fonctionnelle.");
                }
            } while (this._hero.LifePoints > 0 && this._opponent.LifePoints > 0);

            Console.WriteLine($"Vous avez finis le combat avec {this._hero.LifePoints} points de vie et votre adversaire avec {this._opponent.LifePoints} points de vie.");
            return this._opponent.LifePoints <= 0 ? 1 : 0;
        }

    }
}
