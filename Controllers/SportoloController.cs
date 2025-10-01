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

                int idOrd = -1, nameOrd = -1, dobOrd = -1, compOrd = -1, photoOrd = -1;
                if (mySqlDataReader.FieldCount > 0)
                {
                    int GetOrdinalSafe(MySqlDataReader reader, string oszlopNev)
                    {
                        try
                        {
                            return reader.GetOrdinal(oszlopNev);
                        } catch (IndexOutOfRangeException)
                        {
                            return -1;
                        }
                    }

                    idOrd = GetOrdinalSafe(mySqlDataReader, "Id");
                    nameOrd = GetOrdinalSafe(mySqlDataReader, "Name");
                    dobOrd = GetOrdinalSafe(mySqlDataReader, "DateOfBirth");
                    compOrd = GetOrdinalSafe(mySqlDataReader, "CompetitionNumbers");
                    photoOrd = GetOrdinalSafe(mySqlDataReader, "Photo");
                }

                while (mySqlDataReader.Read())
                {
                    int id = 0;
                    if (idOrd >= 0 && !mySqlDataReader.IsDBNull(idOrd)) id = mySqlDataReader.GetInt32(idOrd);

                    string name = string.Empty;
                    if (nameOrd >= 0 && !mySqlDataReader.IsDBNull(nameOrd)) name = mySqlDataReader.GetString(nameOrd);

                    DateTime dob = DateTime.MinValue;
                    if (dobOrd >= 0 && !mySqlDataReader.IsDBNull(dobOrd)) dob = mySqlDataReader.GetDateTime(dobOrd);

                    string comp = string.Empty;
                    if (compOrd >= 0 && !mySqlDataReader.IsDBNull(compOrd)) comp = mySqlDataReader.GetString(compOrd);

                    byte[] photo = null;
                    if (photoOrd >= 0) photo = (byte[])mySqlDataReader.GetValue(photoOrd);

                    sportolok.Add(new Sportolo()
                    {
                        Id = id,
                        Name = name,
                        DateOfBirth = dob,
                        CompetitionNumbers = comp,
                        Photo = photo
                    });
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