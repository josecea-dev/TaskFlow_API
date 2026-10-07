using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.DTO;
using TaskFlow_API.Services.Proyectos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly IProyectoService _service;

        public ProyectoController(IProyectoService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var get = await _service.Get();

                return Ok(get);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {mensaje = ex.Message});
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProyectoDTO dto)
        {
            try
            {
                var post = await _service.Post(dto);

                if (post == null)
                {
                    return NotFound($"El ID de usuario: {dto.Usuario_Id} no existe.");
                }
                return Ok(post);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProyectoDTO dto)
        {
            try
            {
                var put = await _service.Put(id, dto);

                if (put == null)
                {
                    return NotFound($"Verifique si el ID del usuario: {dto.Usuario_Id} realmente existe o si el ID del proyecto existe: {id}");
                }

                return Ok(put);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var delete = await _service.Delete(id);

                if (delete == null)
                {
                    return NotFound($"El ID de proyecto: {id} no existe");
                }
                return Ok(delete);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }
    }
}
