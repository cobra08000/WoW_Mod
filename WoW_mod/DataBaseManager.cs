using MySql.Data.MySqlClient;
using System;
using WoW_Mod;

namespace WoW_Mod
{
    public static class DatabaseManager
    {
        private static string connectionString = "Server=sql-7.verygames.net;Port=3306;Database=db878132;Uid=db878132;Pwd=f1mtg3vbq;";

        public static void Initialize()
        {
            CreateTableIfNotExists();
        }

        private static void CreateTableIfNotExists()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "CREATE TABLE IF NOT EXISTS Players (" +
                    "SteamId VARCHAR(17) PRIMARY KEY, " +
                    "Class INT, " +
                    "Level INT, " +
                    "Experience INT, " +
                    "LastIp VARCHAR(15))", connection);
                command.ExecuteNonQuery();
            }
        }

        public static PlayerData LoadPlayer(string steamId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Players WHERE SteamId = @SteamId", connection);
                command.Parameters.AddWithValue("@SteamId", steamId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var playerData = new PlayerData(steamId)
                        {
                            Class = (PlayerData.PlayerClass)reader.GetInt32("Class"),
                            Level = reader.GetInt32("Level"),
                            Experience = reader.GetInt32("Experience")
                        };
                        return playerData;
                    }
                }
            }
            return new PlayerData(steamId);
        }

        public static void SavePlayer(PlayerData player, string ip)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "INSERT INTO Players (SteamId, Class, Level, Experience, LastIp) " +
                    "VALUES (@SteamId, @Class, @Level, @Experience, @LastIp) " +
                    "ON DUPLICATE KEY UPDATE Class = @Class, Level = @Level, Experience = @Experience, LastIp = @LastIp",
                    connection);

                command.Parameters.AddWithValue("@SteamId", player.SteamId);
                command.Parameters.AddWithValue("@Class", (int)player.Class);
                command.Parameters.AddWithValue("@Level", player.Level);
                command.Parameters.AddWithValue("@Experience", player.Experience);
                command.Parameters.AddWithValue("@LastIp", ip);

                command.ExecuteNonQuery();
            }
        }
    }
}
//Traduction:

//Ce gestionnaire gère les interactions avec la base de données MySQL.
//Il crée la table nécessaire si elle n'existe pas.
//Les méthodes LoadPlayer et SavePlayer permettent de charger et sauvegarder les données des joueurs.