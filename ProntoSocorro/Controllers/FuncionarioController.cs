using Application.DTOs;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ProntoSocorro.Controllers
{
    [ApiController]
    [Route("v1/funcionario/")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarFuncionario([FromBody] FuncionarioDTO funcionarioDTO)
        {
            try
            {
                await _funcionarioService.Create(funcionarioDTO);
                return Ok(funcionarioDTO);
            }
            catch
            {
                return StatusCode(500,"internal server error");
            }            
        }

        [HttpGet("buscar/{id:guid}")]
        public async Task<IActionResult> BuscarFuncionarioPorId([FromRoute] Guid id)
        {            
            try
            {
               var funcionario = await _funcionarioService.GetFuncionarioById(id);
               return Ok(funcionario);
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }


    }
}
