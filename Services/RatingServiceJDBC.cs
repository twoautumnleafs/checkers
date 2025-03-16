using Npgsql;
using Gamestudio.Models;
using System;
using System.Collections.Generic;

namespace Gamestudio.Services
{
    public class RatingServiceJDBC : IRatingService
    {
        private string _connectionString;

        public RatingServiceJDBC(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddRating(Rating rating)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = "INSERT INTO ratings (game_id, player_name, rating, date) VALUES (@gameId, @playerName, @rating, @date)";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("gameId", rating.GameId);
                    cmd.Parameters.AddWithValue("playerName", rating.PlayerName);
                    cmd.Parameters.AddWithValue("rating", rating.RatingValue);
                    cmd.Parameters.AddWithValue("date", rating.Date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Rating> GetRatingsForGame(int gameId)
        {
            var ratings = new List<Rating>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM ratings WHERE game_id = @gameId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("gameId", gameId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ratings.Add(new Rating
                            {
                                Id = reader.GetInt32(0),
                                GameId = reader.GetInt32(1),
                                PlayerName = reader.GetString(2),
                                RatingValue = reader.GetInt32(3),
                                Date = reader.GetDateTime(4)
                            });
                        }
                    }
                }
            }

            return ratings;
        }
    }
}
