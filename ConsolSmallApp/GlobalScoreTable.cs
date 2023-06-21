using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolSmallApp
{
    internal class GlobalScoreTable
    {
        private Dictionary<string, ScoreTable> _globalScoreList = new Dictionary<string, ScoreTable>();
        public Dictionary<string, ScoreTable> GlobalScoreList 
        {
            get { return _globalScoreList; }
        }

        [JsonConstructor]
        private GlobalScoreTable() 
        { }

        public  GlobalScoreTable(List<KeyValuePair<string,bool>> listOfGame) 
        {
            foreach (KeyValuePair<string,bool> item in listOfGame) 
            {
                this._globalScoreList.Add(item.Key, new ScoreTable(item.Key, item.Value));
            }
        }

        public void displayGlobalScoreTable() 
        { 
            foreach ( KeyValuePair<string,ScoreTable> item in this._globalScoreList) 
            { 
                Console.WriteLine($"**************{item.Key}**************");
                item.Value.displayScore();
            }
        }

        public void displayGlobalPlayerScoreTable(string playerName) 
        { 
            foreach (KeyValuePair<string,ScoreTable> item in this._globalScoreList)
            {
                Console.WriteLine($"**************{item.Key}**************");
                item.Value.displayPlayerScore(playerName);
            }
        }

        public void addScore(string gameName, string playerName, int score)
        {
            try 
            { 
                this._globalScoreList[gameName].addScore(playerName,score);
            }
            catch (Exception error) 
            {
                Console.WriteLine ("Votre scorte n'a pas pu être auvegarder.");
                Console.WriteLine ($"Une erreur envisagé mais indésirable est survenue. Veuillez transmettre ce code d'erreur au développeur : {error.ToString()}");
            }
        }
    }
}
