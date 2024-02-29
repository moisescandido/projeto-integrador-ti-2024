using System.Collections;
using System.Data.SQLite;
using Dapper;
using projeto.Models;

namespace projeto.db
{
    public class Consultas
    {
        private static SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source =./ projeto.db; Version=3;");

        public Consultas()
        {
            sqliteConnection.Open();
        }
        public static async Task CriarBanco()
        {
            try
            {
                string query = @"
                CREATE TABLE IF NOT EXISTS clima(
                temperatura DECIMAL(2,2),
                umidade DECIMAL(2,2),
                data VARCHAR(20)
                );";
                await sqliteConnection.QueryAsync(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqliteConnection.Close();
            }
        }
        public async Task SetData(float temperatura, float umidade)
        {
            try
            {
                Clima clima = new Clima
                {
                    Temperatura = temperatura,
                    Umidade = umidade,
                    Data = DateTime.Now
                };

                string query = "INSERT INTO clima(temperatura, umidade, data) VALUES (@Temperatura, @Umidade, @Data)";
                await sqliteConnection.ExecuteAsync(query, clima);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqliteConnection.Close();
            }
        }
        public async Task<float> GetUmidade()
        {
            try
            {
                string query = @"SELECT *
                                FROM clima
                                ORDER BY data DESC
                                LIMIT 1;";
                Clima? clima = await sqliteConnection.QueryFirstOrDefaultAsync<Clima>(query);
                return clima.Umidade;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                sqliteConnection.Close();
            }
        }
        public async Task<float> GetTemperatura()
        {
            try
            {
                string query = @"SELECT *
                                FROM clima
                                ORDER BY data DESC
                                LIMIT 1;";
                Clima? clima = await sqliteConnection.QueryFirstOrDefaultAsync<Clima>(query);

                return clima.Temperatura;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                sqliteConnection.Close();
            }
        }
    }
}