using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.Services.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            try
            {
                var token = await _service.Login(dto);

                if (token == null)
                {
                    return Unauthorized("Credenciales invalidas");
                }

                return Ok(new {token});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }

    }
}
