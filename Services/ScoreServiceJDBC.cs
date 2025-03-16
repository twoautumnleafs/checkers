using Npgsql;
using Gamestudio.Models;
using System;
using System.Collections.Generic;

namespace Gamestudio.Services
{
    public class ScoreServiceJDBC : IScoreService
    {
        private string _connectionString = "Host=localhost;Username=postgres;Password=xbox;Database=gamestudio";

        public ScoreServiceJDBC(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddScore(Score score)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = "INSERT INTO scores (player_name, score, date) VALUES (@playerName, @score, @date)";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("playerName", score.PlayerName);
                    cmd.Parameters.AddWithValue("score", score.ScoreValue);
                    cmd.Parameters.AddWithValue("date", score.Date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Score> GetTopScores(int top)
        {
            var scores = new List<Score>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM scores ORDER BY score DESC LIMIT @top";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("top", top);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scores.Add(new Score
                            {
                                Id = reader.GetInt32(0),
                                PlayerName = reader.GetString(1),
                                ScoreValue = reader.GetInt32(2),
                                Date = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }

            return scores;
        }

        public Score GetScoreById(int id)
        {
            Score score = null;

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM scores WHERE id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            score = new Score
                            {
                                Id = reader.GetInt32(0),
                                PlayerName = reader.GetString(1),
                                ScoreValue = reader.GetInt32(2),
                                Date = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }

            return score;
        }
    }
}