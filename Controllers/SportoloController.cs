using MySql.Data.MySqlClient;
using WEB_App__MVC_.Models;

namespace WEB_App__MVC_.ViewControllers
{
    public class SportoloController
    {

        static MySqlConnection SQLConnection;

        private static void BuildConnection()
        {
            string connectionString = "Server = localhost;Database=fitness;Uid=root;Password=;";
            SQLConnection = new MySqlConnection(connectionString);
        }

        public List<Sportolo> GetSporterFromDatabase()
        {
            BuildConnection();
            List<Sportolo> sportolok = new();
            try
            {
                SQLConnection.Open();
                string sql = "SELECT * from sportolok";
                MySqlCommand mySqlCommand = new(sql, SQLConnection);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    byte[] photo = null;
                    int imgOrd = mySqlDataReader.GetOrdinal("Photo");
                    photo = (byte[])mySqlDataReader["Photo"];

                    sportolok.Add(new Sportolo()
                    {
                        Id = mySqlDataReader.GetInt32("id"),
                        Name = mySqlDataReader.GetString("name"),
                        DateOfBirth = mySqlDataReader.GetDateTime("birth"),
                        CompetitionNumbers = mySqlDataReader.GetString("competitionNumbers"),
                        Photo = photo
                    }) ;
                }
                SQLConnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt: {0}", ex);
            }
            return sportolok;

        }
    }
}