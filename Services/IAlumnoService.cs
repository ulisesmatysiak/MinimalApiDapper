using MinimalApiDapper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalApiDapper.Services
{
    public interface IAlumnoService
    {
        Task<IEnumerable<Alumno>> GetAlumnosAsync();
        Task<Alumno> GetAlumnoByCodigoAsync(int codigo);
        Task<bool> AddAlumnoAsync(Alumno alumno);
        Task<bool> UpdateAlumnoAsync(int codigo, Alumno alumno);
        Task<bool> DeleteAlumnoAsync(int codigo);
    }
}
