using System.Text.Json;
using System.IO;
using System;
using Newtonsoft.Json;

namespace ConsolSmallApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            void displayMenu()
            {
                Console.WriteLine("**Menu**");
                Console.WriteLine("1/ Devine le nombre.");
                Console.WriteLine("2/ Pendu.");
                Console.WriteLine("3/ Pierre, Papier, Ciseaux.");
                Console.WriteLine("4/ Combats de Guerrier.");
                Console.WriteLine("10/ Date et heure.");
                Console.WriteLine("0/ Quitter.");
            }

            GlobalScoreTable initGlobalScoreTable() 
            {
                List<KeyValuePair<string,bool>> listOfGames = new List<KeyValuePair<string, bool>>()
                { 
                    new KeyValuePair<string,bool>("Devine nombre", false),
                    new KeyValuePair<string,bool>("Pierre Papier Ciseaux", true),
                    new KeyValuePair<string,bool>("Pendu", true),
                    new KeyValuePair<string, bool>("Combat de guerrier", true)
                };

                GlobalScoreTable currentGlobalScoreTable;

                if (File.Exists("..\\..\\..\\jsonSave\\globalScoreTable.json"))
                {
                    string jsonString = File.ReadAllText("..\\..\\..\\jsonSave\\globalScoreTable.json");
                    try 
                    {
                        currentGlobalScoreTable = JsonConvert.DeserializeObject<GlobalScoreTable>(jsonString);
                        //currentGlobalScoreTable = JsonSerializer.Deserialize<GlobalScoreTable>(jsonString);
                        bool jsonIsCorrect = true;
                        foreach (KeyValuePair<string, bool> item  in listOfGames) 
                        {
                            jsonIsCorrect = jsonIsCorrect && currentGlobalScoreTable.GlobalScoreList.ContainsKey(item.Key);
                        }
                        Console.WriteLine(jsonIsCorrect ? "Récupération du fichier de sauvegarde" : "Fichier de sauvegarde corrompue, création d'un nouveau fichier.");
                        return jsonIsCorrect ? currentGlobalScoreTable : new GlobalScoreTable(listOfGames);
                    }
                    catch 
                    (Exception error) 
                    {
                        Console.WriteLine("Une erreur est survenue un nouveaux fichier de score est générer. Veuillez transmettre ce message au développeur :");
                        Console.WriteLine(error.ToString());
                        return new GlobalScoreTable(listOfGames);
                    }
                }
                else 
                { 
                    return new GlobalScoreTable(listOfGames);
                }
            }

            void saveGlobalScoreTable (GlobalScoreTable globalScoreTable) 
            { 
                string jsonString = JsonConvert.SerializeObject(globalScoreTable);
                //string jsonString = JsonSerializer.Serialize(globalScoreTable);
                File.WriteAllText("..\\..\\..\\jsonSave\\globalScoreTable.json", jsonString);
            }



            int input = -1;
            Console.Title = "Jeux simple et amusant !";
            DateTime startDate= DateTime.Now;

            Player currentplayer = new Player();
            GlobalScoreTable currentGlobalScoreTable = initGlobalScoreTable();
            Console.WriteLine(currentplayer.PlayerName);
            
            currentGlobalScoreTable.displayGlobalScoreTable();
            do {
                while (input == -1)
                {
                    displayMenu();
                    Console.WriteLine("Veuillez entrer votre choix.");
                    if (Int32.TryParse(Console.ReadLine(), out input))
                    {
                        switch (input)
                        {
                            case 0:
                                Console.WriteLine("L'application est en cours de fermeture.");
                                Console.WriteLine($"Vous avez jouer pendant {DateTime.Now.Subtract(startDate)}.");
                                currentplayer.updatePlayTime(DateTime.Now);
                                break;
                            case 1:
                                GuessNumber currentGessNumberParty = new GuessNumber();
                                currentGlobalScoreTable.addScore("Devine nombre", currentplayer.PlayerName, currentGessNumberParty.startGame());
                                input = -1;
                                break;
                            case 2:
                                Pendu currentPenduParty = new Pendu();
                                currentGlobalScoreTable.addScore("Pendu", currentplayer.PlayerName, currentPenduParty.startGame()); 
                                input = -1;
                                break;
                            case 3:
                                RockPaperScissors currentRockPaperScissorsParty = new RockPaperScissors();
                                currentGlobalScoreTable.addScore("Pierre Papier Ciseaux", currentplayer.PlayerName, currentRockPaperScissorsParty.startGame());
                                input = -1;
                                break;
                            case 4:
                                WarriorFight currentWarriorFightParty = new WarriorFight();
                                Console.WriteLine(currentWarriorFightParty.startGame() == 1 ? "Victoire" : "Défaite");
                                input = -1; 
                                break;
                            case 10:
                                Console.WriteLine($"Nous somme le {DateTime.Now.ToString("dd/MMMM/yyyy")} et il est {DateTime.Now.TimeOfDay}");
                                input = -1;
                                break;
                            default:
                                Console.WriteLine("La commande envoyé n'est pas fonctionnelle.");
                                input = -1;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("L'input utilisé est incorrecte.");
                        input = -1;
                    }
                }
            } while (input != 0);
            try 
            {
                Console.WriteLine("Sauvegarde du tableau de score.");
                saveGlobalScoreTable(currentGlobalScoreTable);
            } 
            catch (Exception error) 
            {
                Console.WriteLine("La sauvegarde à échouer veuillez transmettre ce message d'erreur au développeur.");
                Console.WriteLine(error.ToString());
            }
            Console.WriteLine("Appuyer sur une touche pour fermer l'application.");
            Console.ReadKey();

        }
    }
}