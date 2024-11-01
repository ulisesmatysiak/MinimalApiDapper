using Dapper;
using Microsoft.Data.Sqlite;
using MinimalApiDapper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalApiDapper.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly DbConn _connection;

        public AlumnoService(DbConn connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Alumno>> GetAlumnosAsync()
        {
            using var connection = _connection.GetConnection();
            return await connection.QueryAsync<Alumno>("SELECT * FROM Alumnos");
        }

        public async Task<Alumno> GetAlumnoByCodigoAsync(int codigo)
        {
            using var connection = _connection.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<Alumno>("SELECT * FROM Alumnos WHERE Codigo = @Codigo", new { Codigo = codigo });
        }

        public async Task<bool> AddAlumnoAsync(Alumno alumno)
        {
            using var connection = _connection.GetConnection();
            var result = await connection.ExecuteAsync("INSERT INTO Alumnos (Nombre) VALUES (@Nombre)", alumno);
            return result > 0;
        }

        public async Task<bool> UpdateAlumnoAsync(int codigo, Alumno alumno)
        {
            using var connection = _connection.GetConnection();
            var result = await connection.ExecuteAsync("UPDATE Alumnos SET Nombre = @Nombre WHERE Codigo = @Codigo", new { alumno.Nombre, Codigo = codigo });
            return result > 0;
        }

        public async Task<bool> DeleteAlumnoAsync(int codigo)
        {
            using var connection = _connection.GetConnection();
            var result = await connection.ExecuteAsync("DELETE FROM Alumnos WHERE Codigo = @Codigo", new { Codigo = codigo });
            return result > 0;
        }
    }
}
