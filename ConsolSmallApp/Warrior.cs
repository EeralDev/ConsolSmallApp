using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolSmallApp
{
    internal class Warrior
    {
        //Attribut et getter setter

        private Random _random = new Random();

        private int _lifePoints;
        public int LifePoints
        {
            get { return _lifePoints; }
            set { _lifePoints = value; }
        }
        
        private int _manaPoints;
        public int ManaPoints
        {
            get { return _manaPoints; }
            set { _manaPoints = value; }
        }

        private int _attack;

        public int Attack 
        {
            get { return _attack; } 
            set { _attack = value; } 
        }

        private int _defense;
        public int Defense
        {
            get { return _defense; }
            set { _defense = value; }
        }

        //Constructeur
        public Warrior() 
        {
            string input;

            Console.WriteLine("Voulez-vous que votre guerrier soit offensif ou defensif ? ");
            input = Console.ReadLine();

            while (input.ToUpper() != "OFFENSIF" && input.ToUpper() != "DEFENSIF") 
            {
                Console.WriteLine($"La commande {input} n'est pas correct. Veuillez répondre \"offensif\" ou \"defensif\"");
                input = Console.ReadLine();
            }

            if (input.ToUpper() != "OFFENSIF") 
            {
                this._attack = 75;
                this._defense = 25;
            }
            else
            {
                this._attack = 25;
                this._defense = 75;
            }

            Console.WriteLine("Voulez-vous que votre guerrier soit magique ou physique ? ");
            input = Console.ReadLine();

            while (input.ToUpper() != "MAGIQUE" && input.ToUpper() != "PHYSIQUE")
            {
                Console.WriteLine($"La commande {input} n'est pas correct. Veuillez répondre \"magique\" ou \"physique\"");
                input = Console.ReadLine();
            }

            if (input.ToUpper() != "Magique")
            {
                this._lifePoints = 25;
                this._manaPoints = 75;
            }
            else
            {
                this._lifePoints = 75;
                this._manaPoints = 25;
            }
        }

        public Warrior(int lifePoints, int manaPoints, int attack, int defense)
        {
            this._lifePoints = lifePoints;
            this._manaPoints = manaPoints;
            this._attack = attack;
            this._defense = defense;
        }

        public int physicalMove() 
        { 
            return this._attack / 4 + this._random.Next(0, 10);
        }

        public int magicMove()
        {
            this._manaPoints = this._manaPoints <= 0 ? 0 :  this._manaPoints - this._attack / this._random.Next(1, 5);
            return this._attack / 3 + this._manaPoints / 5 + this._random.Next(0, 5);
        }

        public void focusEnergy() 
        {
            this._attack = this._attack + this._random.Next(0, 10);
        }

        public void receiveDamage(int damage)
        {
            this._lifePoints = damage - this._defense / 2 <= 0 ? this._lifePoints - 20 : this._lifePoints - (damage - this._defense / 2);
        }

    }
}   
