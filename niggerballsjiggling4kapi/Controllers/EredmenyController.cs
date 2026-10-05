using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MySqlConnector;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using niggerballsjiggling4kapi.Models;
namespace niggerballsjiggling4kapi.Controllers
{
    [Route("eredmeny")]
    [ApiController]
    public class EredmenyController
    {
        public string connectionString = "server=localhost;user=root;password=;database=sportolo13b";

        [HttpGet("all")]
        public object getAlleredmeny()
        {
            List<Models.Eredmeny> eredmenyList = new List<Models.Eredmeny>();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM eredmeny";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Eredmeny eredmeny = new Models.Eredmeny();
                            {
                                eredmeny.Id = reader.GetInt32("id");
                                eredmeny.Competition = reader.GetString("competition");
                                eredmeny.Description = reader.GetString("description");
                                eredmeny.ResultTime = reader.GetDateTime("result_time");
                                eredmeny.UpdateTime = reader.GetDateTime("update_time");
                                eredmeny.SportoloId = reader.GetInt32("sportolo_id");
                            }
                            ;
                            eredmenyList.Add(eredmeny);
                        }
                    }
                }
            }
            return eredmenyList;      
        }

        [HttpGet("byid")]
        public object getEredmenyById(int id)
        {
            Models.Eredmeny eredmeny = new Models.Eredmeny();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM eredmeny WHERE id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            eredmeny.Id = reader.GetInt32("id");
                            eredmeny.Competition = reader.GetString("competition");
                            eredmeny.Description = reader.GetString("description");
                            eredmeny.ResultTime = reader.GetDateTime("result_time");
                            eredmeny.UpdateTime = reader.GetDateTime("update_time");
                            eredmeny.SportoloId = reader.GetInt32("sportolo_id");
                        }
                    }
                }
            }
            return eredmeny;
        }

        [HttpPost("add")]
        public object addEredmeny(Models.Eredmeny eredmeny)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO eredmeny (competition, description, result_time, update_time, sportolo_id) VALUES (@competition, @description, @result_time, @update_time, @sportolo_id)";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@competition", eredmeny.Competition);
                    command.Parameters.AddWithValue("@description", eredmeny.Description);
                    command.Parameters.AddWithValue("@result_time", DateTime.Now);
                    command.Parameters.AddWithValue("@update_time", DateTime.Now);
                    command.Parameters.AddWithValue("@sportolo_id", eredmeny.SportoloId);
                    command.ExecuteNonQuery();

                    connection.Close();
                    return new { message = "sikeres felvetel", result = eredmeny };
                }
            }
        }
        [HttpPut("update")]
        public object updateEredmeny(Models.Eredmeny eredmeny)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE eredmeny SET competition = @competition, description = @description, update_time = @update_time, sportolo_id = @sportolo_id WHERE id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@competition", eredmeny.Competition);
                    command.Parameters.AddWithValue("@description", eredmeny.Description);
                    var updateTime = DateTime.Now;
                    command.Parameters.AddWithValue("@update_time", updateTime);
                    command.Parameters.AddWithValue("@sportolo_id", eredmeny.SportoloId);
                    command.Parameters.AddWithValue("@id", eredmeny.Id);
                    command.ExecuteNonQuery();

                    connection.Close();
                    var updated = new Models.Eredmeny
                    {
                        Id = eredmeny.Id,
                        Competition = eredmeny.Competition,
                        Description = eredmeny.Description,
                        ResultTime = eredmeny.ResultTime,
                        UpdateTime = updateTime,
                        SportoloId = eredmeny.SportoloId
                    };
                    return new { message = "sikeres frissites", result = updated };
                }
            }
        }

        [HttpDelete("delete")]
        public object deleteEredmeny(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM eredmeny WHERE id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return new { message = "sikeres torles" };
                }
            }
        }
    }
}
