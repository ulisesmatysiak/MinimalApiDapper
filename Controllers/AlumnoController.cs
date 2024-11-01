using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MinimalApiDapper.Models;
using MinimalApiDapper.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalApiDapper.Controllers
{
    [ApiController]
    [Route("alumnos")]
    public class AlumnoController : Controller
    {
        private readonly IAlumnoService _alumnoService;
        private readonly ILogger<AlumnoService> _logger;

        public AlumnoController(IAlumnoService alumnoService, ILogger<AlumnoService> logger)
        {
            _alumnoService = alumnoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            try
            {
                var alumnos = await _alumnoService.GetAlumnosAsync();
                return Ok(alumnos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Error data");
            }
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<Alumno>> GetAlumno(int codigo)
        {
            try
            {
                var alumno = await _alumnoService.GetAlumnoByCodigoAsync(codigo);
                return alumno is not null ? Ok(alumno) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} alumno: {codigo}");
                return StatusCode(500, "Error data");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAlumno([FromBody] Alumno alumno)
        {
            try
            {
                var result = await _alumnoService.AddAlumnoAsync(alumno);
                return result ? CreatedAtAction(nameof(GetAlumno), new { codigo = alumno.Codigo }, alumno) : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Error create ");
            }
        }

        [HttpDelete("{codigo}")]
        public async Task<ActionResult> DeleteAlumno(int codigo)
        {
            try
            {
                var result = await _alumnoService.DeleteAlumnoAsync(codigo);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} alumno: {codigo}");
                return StatusCode(500, "Error delete");
            }
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult> UpdateAlumno(int codigo, [FromBody] Alumno alumno)
        {
            try
            {
                var result = await _alumnoService.UpdateAlumnoAsync(codigo, alumno);
                return result ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message}  alumno: {codigo}");
                return StatusCode(500, "Error update");
            }
        }
    }
}
