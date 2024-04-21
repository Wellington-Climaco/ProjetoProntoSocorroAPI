using Application.DTOs.AuthenticateDTOs;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProntoSocorro.Controllers
{
    [ApiController]
    [Route("v1/login")]
    public class LoginController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public LoginController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authentication([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var funcionario = await _funcionarioService.AutenticarFuncionario(loginDTO.email, loginDTO.senha);

                if (!funcionario.IsValid)
                    return NotFound(funcionario.Errors);

                return Ok(funcionario);
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        } 
        

    }
}
