using Application.DTOs.FuncionarioDTOs;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var funcionario = await _funcionarioService.Create(funcionarioDTO);

                if(funcionario.IsValid)
                    return Created($"v1/funcionario/buscar/{funcionario.Data.Id}", funcionario.Data);
                else
                    return BadRequest(funcionario.Errors);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possivel adicionar dados");
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

                if(funcionario.IsValid)
                    return Ok(funcionario.Data);
                else
                    return BadRequest(funcionario.Errors);
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }

        [HttpDelete("remover/{id:guid}")]
        public async Task<IActionResult> RemoverFuncionario([FromRoute] Guid id)
        {
            try
            {
                var funcionario = await _funcionarioService.Delete(id);

                if(!funcionario.IsValid)
                    return NotFound(funcionario.Errors);

                return Ok(funcionario.Data);           
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possivel remover dados");
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }

        [HttpGet("buscarTodos")]
        public async Task<IActionResult> BuscarFuncionarios()
        {
            try
            {
                var funcionario = await _funcionarioService.GetAllFuncionarios();

                if (funcionario.IsValid)
                    return Ok(funcionario.Data);
                else
                    return BadRequest(funcionario.Errors);
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPut("atualizar/{id:guid}")]
        public async Task<IActionResult> AtualizarFuncionario([FromRoute] Guid id, [FromBody] AtualizarFuncionarioDTO funcionarioDTO)
        {
            try
            {
                var funcionario = await _funcionarioService.Update(funcionarioDTO, id);

                if(!funcionario.IsValid)
                    return BadRequest(funcionario.Errors);

                return Ok(funcionario.Data);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possivel atualizar dados");
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }

        [HttpGet("proximo/{id:guid}")]
        public async Task<IActionResult> ChamarProximoPaciente(Guid id)
        {
            //try
            //{
                var paciente = await _funcionarioService.ChamarProximo(id);

                if(!paciente.IsValid)
                    return BadRequest(paciente.Errors);

                return Ok(paciente.Data);
            //}
            //catch
            //{
            //    return StatusCode(500, "internal server error");
            //}
        }




    }
}
