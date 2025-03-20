using aulaApiTarefas.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


namespace aulaApiTarefas.Data
{
    public class TarefaRepository
    {
        private readonly string _connectionString;

        public TarefaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Tarefa> GetAll()
        {
            List<Tarefa> tarefas = new List<Tarefa>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Tarefas", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tarefas.Add(new Tarefa
                    {
                        Id = reader.GetInt32("Id"),
                        Descricao = reader.GetString("Descricao"),
                        Concluida = reader.GetBoolean("Concluida")
                    });
                }
            }

            return tarefas;
        }

        public void Add(Tarefa tarefa)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO Tarefas (Descricao, Concluida) VALUES (@Descricao, @Concluida)", connection);
                command.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
                command.Parameters.AddWithValue("@Concluida", tarefa.Concluida);
                command.ExecuteNonQuery();
            }
        }

        public void Update(Tarefa tarefa)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE Tarefas SET Descricao = @Descricao, Concluida = @Concluida WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
                command.Parameters.AddWithValue("@Concluida", tarefa.Concluida);
                command.Parameters.AddWithValue("@Id", tarefa.Id);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("DELETE FROM Tarefas WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
