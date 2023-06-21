using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ConsolSmallApp
{
    internal class Player
    {
        // Attribut et Getter Setter

        private string _playerName;
        public string PlayerName
        { 
            get { return _playerName; } 
            set { _playerName = value; }
        }

        private string _password;

        private DateTime _loginDate;
        public DateTime LoginDate 
        { get 
            { return _loginDate; } 
            set { _loginDate = value; }        
        }

        private bool _isGuessed;
        public bool IsGuessed
        {
            get { return _isGuessed; }
            set { _isGuessed = value; }
        }


        //// Constructeur 
        
        public Player() 
        {
            int userInput = -1;
            string dBPath = "C:\\Users\\Ilyes\\Documents\\Projet\\.Net\\C#\\ConsolSmallApp\\ConsolSmallApp\\DB\\DBPlayer.sqlite";

            if (!File.Exists(dBPath)) 
            { 
                createPlayerDB(dBPath);
            }

            do
            {
                Console.WriteLine("1/ Se connecter");
                Console.WriteLine("2/ S'inscrir");
                Console.WriteLine("3/ Jouer en tant qu'inviter");
                if (Int32.TryParse(Console.ReadLine(), out userInput)) 
                { 
                    switch (userInput) 
                    {
                        case 1:
                            Console.WriteLine("Veuillez entrez votre pseudo.");
                            string userName = Console.ReadLine();
                            Console.WriteLine("Veuillez entrez votre mot pas si secret.");
                            string userPassword = Console.ReadLine();
                            if (checkPassword(userName,userPassword) )
                            {
                                Console.WriteLine("Vous êtes connecter");

                            }
                            else 
                            {
                                Console.WriteLine("La connection à échouer");
                                userInput = -1;
                            }
                            break;
                        case 2:
                            Console.WriteLine("Veuillez entrez votre pseudo.");
                            string newPlayerName = Console.ReadLine();
                            Console.WriteLine("Veuillez entrez votre mot pas si secret.");
                            string newPassword = Console.ReadLine();
                            if (nameExistInDB(newPlayerName)) 
                            {
                                Console.WriteLine("Ce Pseudo existe déjà vous ne pouvez pas l'utiliser.");
                                userInput = - 1;
                            }
                            else 
                            {
                                insertPlayerDB(newPlayerName, newPassword);
                                Console.WriteLine("Création d'un nouvel utilisateur.");
                                Console.WriteLine("Veuillez vous connecter une première fois.");
                                userInput = -1;
                            }
                            break; 
                        case 3:
                            Console.WriteLine("Veuillez renseigner votre pseudo :");
                            this._playerName = Console.ReadLine();
                            this._password = "none";
                            this._loginDate = DateTime.Now;
                            this._isGuessed = true;
                            break;
                        default:
                            Console.WriteLine("La commande envoyé n'est pas fonctionnel.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("L'input utilisé est incorrect.");
                    userInput = -1;
                }

            } while (userInput == -1);
        }

        private void createPlayerDB(string path) 
        { 
            SQLiteConnection.CreateFile(path);
            SQLiteConnection dBPlayerConnection = new SQLiteConnection("Data Source=C:\\Users\\Ilyes\\Documents\\Projet\\.Net\\C#\\ConsolSmallApp\\ConsolSmallApp\\DB\\DBPlayer.sqlite;Version=3;");

            dBPlayerConnection.Open();

            string sqlCreatRequest = "create table users (userName varchar(20), password varchar(20), gameTime int)";
            SQLiteCommand createDBPlayerCommand = new SQLiteCommand(sqlCreatRequest,dBPlayerConnection);

            createDBPlayerCommand.ExecuteNonQuery();

            dBPlayerConnection.Close();
        }

        private void insertPlayerDB (string playerName, string password) 
        {
            SQLiteConnection dBPlayerConnection = new SQLiteConnection("Data Source=C:\\Users\\Ilyes\\Documents\\Projet\\.Net\\C#\\ConsolSmallApp\\ConsolSmallApp\\DB\\DBPlayer.sqlite;Version=3;");
            dBPlayerConnection.Open();
            string sqlInsertRequest = $"INSERT INTO users (userName, password, gameTime) VALUES ('{playerName}','{password}','0')";
            SQLiteCommand insertDBPlayerCommand = new SQLiteCommand (sqlInsertRequest,dBPlayerConnection);
            insertDBPlayerCommand.ExecuteNonQuery();

            dBPlayerConnection.Close();
        }

        private bool nameExistInDB (string playerName)
        {
            SQLiteConnection dBPlayerConnection = new SQLiteConnection("Data Source=C:\\Users\\Ilyes\\Documents\\Projet\\.Net\\C#\\ConsolSmallApp\\ConsolSmallApp\\DB\\DBPlayer.sqlite;Version=3;");
            dBPlayerConnection.Open();

            string sqlSelectRequest = $"SELECT * FROM users WHERE userName = '{playerName}'";

            SQLiteCommand selectDBPlayerRequest = new SQLiteCommand (sqlSelectRequest,dBPlayerConnection);

            SQLiteDataReader dataReader = selectDBPlayerRequest.ExecuteReader();

            bool result = dataReader.HasRows;

            dataReader.Close();
            dBPlayerConnection.Close();

            return result;
        }

        private bool checkPassword(string playerName, string password)
        {
            string dBPassword;

            SQLiteConnection dBPlayerConnection = new SQLiteConnection("Data Source=C:\\Users\\Ilyes\\Documents\\Projet\\.Net\\C#\\ConsolSmallApp\\ConsolSmallApp\\DB\\DBPlayer.sqlite;Version=3;");
            dBPlayerConnection.Open();

            string sqlSelectRequest = $"SELECT * FROM users WHERE userName = '{playerName}'";

            SQLiteCommand selectDBPlayerRequest = new SQLiteCommand(sqlSelectRequest, dBPlayerConnection);

            SQLiteDataReader dataReader = selectDBPlayerRequest.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Read();
                dBPassword = dataReader.GetString(1);
                if (dBPassword != null && dBPassword == password) 
                {
                    this._isGuessed = true;
                    this._playerName = dataReader.GetString(0);
                    this._loginDate = DateTime.Now;
                    this._password = dBPassword;
                }
                else 
                { 
                    return false;
                }
            }
            else
            {
                return false;
            }

            dataReader.Close();
            dBPlayerConnection.Close();

            return true;
        }

        public void updatePlayTime(DateTime logoutDate) 
        {
            SQLiteConnection dBPlayerConnection = new SQLiteConnection("Data Source=C:\\Users\\Ilyes\\Documents\\Projet\\.Net\\C#\\ConsolSmallApp\\ConsolSmallApp\\DB\\DBPlayer.sqlite;Version=3;");
            dBPlayerConnection.Open();

            string sqlUpdateGameTimeRequest = $"UPDATE users SET gameTime = gameTime + {((int)(logoutDate - this._loginDate).TotalSeconds)} WHERE userName = '{this._playerName}'";

            SQLiteCommand updateGameTimeCommand = new SQLiteCommand(sqlUpdateGameTimeRequest, dBPlayerConnection);

            updateGameTimeCommand.ExecuteNonQuery();

            dBPlayerConnection.Close();

            Console.WriteLine($"Une durée de {(int)(logoutDate - this._loginDate).TotalSeconds} en seconde à été ajouté !");
        }

    }
}
