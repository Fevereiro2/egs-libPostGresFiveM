using Npgsql;
using System;

namespace FiveMPostgresLib
{
    public class PostgresConnector
    {
        private readonly string _connectionString;

        public PostgresConnector(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteQuery(string query)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar a query: " + ex.Message);
            }
        }

        public NpgsqlDataReader ExecuteReader(string query)
        {
            try
            {
                var connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                var command = new NpgsqlCommand(query, connection);
                return command.ExecuteReader(); // Note que a conexão deve ser fechada manualmente após a leitura
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar a query: " + ex.Message);
                return null;
            }
        }
    }
}
