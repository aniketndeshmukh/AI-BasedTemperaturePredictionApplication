using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Configuration;

namespace AI_BasedTemperaturePredictionApplication.Models
    {
        public class TemperatureRepository
        {
            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            public IEnumerable<CityTemperature> GetTemperatureData()
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    return connection.Query<CityTemperature>("SELECT * FROM CityTemperature ORDER BY RecordedDate DESC").ToList();
                }
            }

            public void SaveTemperature(CityTemperature data)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO CityTemperature (CityName, Temperature, RecordedDate) VALUES (@CityName, @Temperature, @RecordedDate)";
                    connection.Execute(query, data);
                }
            }
        }
    }

  