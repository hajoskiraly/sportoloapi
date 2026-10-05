using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using MySqlConnector;
using niggerballsjiggling4kapi.Models;
using niggerballsjiggling4kapi.Models.DTOs;

namespace niggerballsjiggling4kapi.Controllers
{
    public class specialController
    {
        public string ConnectionString = "server=localhost;user=root;password=;database=sportolo13b";

        [HttpGet("GetNameEmail")]
        public object getNameEmail(int id)
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = @"SELECT name, email FROM sportolo WHERE id = @id";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();

            reader.Read();

            var sportolo = new Sportolo

            { name = reader.GetString("name"), email = reader.GetString("email") };

            return new { message = "sikeres talalat", sportolo = sportolo };
        }

        [HttpGet("ByNameInfos")]

        public object getByNameInfos(Infos infos)
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = @"SELECT sportolo.name, eredmeny.Competition, eredmeny.Description FROM sportolo JOIN eredmeny ON sportolo.id = eredmeny.SportoloId WHERE sportolo.name = @name";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@name", infos.name);
            var reader = cmd.ExecuteReader();
            reader.Read();
            var genyo = new Infos
            {
                name = reader.GetString("name"),
                Competition = reader.GetString("Competition"),
                Description = reader.GetString("Description")
            };
            return new { message = "sikeres talalat", sportolo = genyo };
        }

        [HttpGet("allEredmeny")]
        public object CountEredmeny()
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = @"SELECT COUNT(*) AS EredmenyCount FROM eredmeny";

            var cmd = new MySqlCommand(sql, connector);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            connector.Close();
            return new { message = "sikeres lekerdezes", result = count };
        }

        [HttpGet("sportoloHowManyEredmeny")]
        public object CountEredmenyBySportolo(int id)
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = @"SELECT COUNT(*) AS EredmenyCount FROM eredmeny WHERE SportoloId = @id";

            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@id", id);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            connector.Close();
            return new { message = "sikeres lekerdezes", result = count };
        }
    }
}
