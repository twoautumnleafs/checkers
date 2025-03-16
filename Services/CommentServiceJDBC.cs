using Npgsql;
using Gamestudio.Models;
using System;
using System.Collections.Generic;

namespace Gamestudio.Services
{
    public class CommentServiceJDBC : ICommentService
    {
        private string _connectionString;

        public CommentServiceJDBC(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddComment(Comment comment)
        {
            if (string.IsNullOrEmpty(comment.Text))
            {
                throw new ArgumentException("Comment text cannot be null or empty");
            }

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = "INSERT INTO comments (game_id, player_name, comment, date) VALUES (@gameId, @playerName, @comment, @date)";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("gameId", comment.GameId);
                    cmd.Parameters.AddWithValue("playerName", comment.PlayerName);
                    cmd.Parameters.AddWithValue("comment", comment.Text);
                    cmd.Parameters.AddWithValue("date", comment.Date);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<Comment> GetCommentsForGame(int gameId)
        {
            var comments = new List<Comment>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM comments WHERE game_id = @gameId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("gameId", gameId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comments.Add(new Comment
                            {
                                Id = reader.GetInt32(0),
                                GameId = reader.GetInt32(1),
                                PlayerName = reader.GetString(2),
                                Text = reader.GetString(3),
                                Date = reader.GetDateTime(4)
                            });
                        }
                    }
                }
            }

            return comments;
        }
    }
}
