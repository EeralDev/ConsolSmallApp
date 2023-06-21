using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsolSmallApp
{
    internal class ScoreTable
    {
        private string _gameName;
        public string GameName 
        { 
            get { return this._gameName;} 
            set { this._gameName = value; } 
        }
        
        private List<KeyValuePair<string, int>> _scores;

        public List<KeyValuePair<string, int>> Scores
        {
            get { return this._scores; }
            set { this._scores = value; }
        }

        private bool _isDesc;
        public bool IsDesc 
        { 
            get { return this._isDesc; } 
            set { this._isDesc = value; } 
        }

        public ScoreTable(string gameName, bool isDesc ) 
        {
            this._gameName=gameName;
            this._isDesc=isDesc;

            this._scores = new List<KeyValuePair<string, int>> 
            { 
                new KeyValuePair<string, int>("Ochato", this._isDesc ? 0 : 999999999) 
            };
        } 

        public void addScore (string name,int score)
        {
            this._scores.Add(new KeyValuePair<string, int>(name, score));

            if (this._isDesc ) 
            {
                this._scores = this._scores.OrderByDescending(item => item.Value).ToList<KeyValuePair<string,int>>();
            }
            else
            {
                this._scores = this._scores.OrderBy(item => item.Value).ToList<KeyValuePair<string, int>>();
            }

            if (this._scores.Count > 10 ) 
            {
                this._scores.RemoveRange(10,this._scores.Count - 10);
            }
            

        }
        public void displayScore ()
        {
            int rank = 1;
            foreach (KeyValuePair<string,int> item in this._scores)
            {
                Console.WriteLine($"{rank} : {item.Key}/{item.Value}");
                ++rank;
            }
        }

        public void displayPlayerScore (string playerName)
        {
            List<KeyValuePair<string,int>> playerScore = 
               (from item 
                in this._scores 
                where item.Key == playerName 
                select item).ToList<KeyValuePair<string, int>>();
            if (playerScore.Count > 0 ) 
            {
                foreach (KeyValuePair<string, int> item in playerScore)
                {
                    Console.WriteLine($"{item.Key}/{item.Value}");
                }
            }
            else 
            {
                Console.WriteLine($"Aucun score n'est enregistrer pour le joueur {playerName}");
            }
            
        }
    }
}
