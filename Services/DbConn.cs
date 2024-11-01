using Dapper;
using Microsoft.Data.Sqlite;
using MinimalApiDapper.Models;
using System.ComponentModel;

namespace MinimalApiDapper.Services
{
    public class DbConn
    {
        private readonly SqliteConnection _connection;

        public DbConn()
        {
            _connection = new SqliteConnection("Data Source=alumnos.db");
            _connection.Open();
        }

        public void Conn()
        {
            _connection.Execute("CREATE TABLE IF NOT EXISTS Alumnos (Codigo INTEGER PRIMARY KEY, Nombre TEXT)");

            var count = _connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Alumnos");

            if (count == 0)
            {
                var alumnos = new[]
            {
                new Alumno { Nombre = "Alumo A" },
                new Alumno { Nombre = "Alumno B" },
                new Alumno { Nombre = "Alumno C" },
                new Alumno { Nombre = "Alumno D" }
            };
                _connection.Execute("INSERT INTO Alumnos (Nombre) VALUES (@Nombre)", alumnos);
            }
        }

        public SqliteConnection GetConnection() => _connection;
    }
}
