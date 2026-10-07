using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TaskFlow_API.DTO;
using TaskFlow_API.Services.Tareas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _service;

        public TareaController(ITareaService service)
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
        public async Task<IActionResult> Post([FromBody] TareaDTO dto)
        {
            try
            {
                var post = await _service.Post(dto);

                if (post == null)
                {
                    return NotFound($"Revise si el ID del proyecto: {dto.Id_Proyecto} o el ID de usuario: {dto.Id_Usuario} realmente existen");
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
        public async Task<IActionResult> Put(int id, [FromBody] TareaDTO dto)
        {
            try
            {
                var put = await _service.Put(id, dto);

                if (put == null)
                {
                    return NotFound($"Revise si el ID del proyecto: {dto.Id_Proyecto} o el ID de usuario: {dto.Id_Usuario} realmente existen");
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
                    return NotFound();
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
