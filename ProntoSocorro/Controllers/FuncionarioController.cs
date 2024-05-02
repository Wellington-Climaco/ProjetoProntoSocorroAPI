using Application.DTOs.FuncionarioDTOs;
using Application.Interface;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Registra novo funcionário no sistema (somente admin)
        /// </summary>
        /// <param name="funcionarioDTO"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("criar")]
        public async Task<IActionResult> CriarFuncionario([FromBody] FuncionarioDTO funcionarioDTO)
        {
            try
            {
                var funcionario = await _funcionarioService.Create(funcionarioDTO);

                if (funcionario.IsValid)
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
                return StatusCode(500, "internal server error");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("buscar/{id:guid}")]
        public async Task<IActionResult> BuscarFuncionarioPorId([FromRoute] Guid id)
        {
            try
            {
                var funcionario = await _funcionarioService.GetFuncionarioById(id);

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
        /// <summary>
        /// Remove funcionário. (somente admin)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("remover/{id:guid}")]
        public async Task<IActionResult> RemoverFuncionario([FromRoute] Guid id)
        {
            try
            {
                var funcionario = await _funcionarioService.Delete(id);

                if (!funcionario.IsValid)
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

        [Authorize]
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

        [Authorize]
        [HttpPut("atualizar/{id:guid}")]
        public async Task<IActionResult> AtualizarFuncionario([FromRoute] Guid id, [FromBody] AtualizarFuncionarioDTO funcionarioDTO)
        {
            try
            {
                var funcionario = await _funcionarioService.Update(funcionarioDTO, id);

                if (!funcionario.IsValid)
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

        /// <summary>
        /// Chama o proximo paciente da fila
        /// </summary>
        /// <param name="id"></param>
        [Authorize]
        [HttpGet("proximo/{id:guid}")]
        public async Task<IActionResult> ChamarProximoPaciente(Guid id)
        {
            try
            {
                var paciente = await _funcionarioService.ChamarProximo(id);

                if (!paciente.IsValid)
                    return BadRequest(paciente.Errors);

                return Ok(paciente.Data);
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }

        /// <summary>
        /// Dispensa paciente quando atendimento dele acabou
        /// </summary>
        /// <param name="idPaciente">id do paciente que esta sendo dispensado</param>
        /// <param name="idFuncionario"> id do funcionário que esta dispensando</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Clinico,Recepcao")]
        [HttpDelete("dispensar/{idPaciente:guid}/{idFuncionario:guid}")]
        public async Task<IActionResult> DispensarPaciente(Guid idPaciente, Guid idFuncionario)
        {
            try
            {
                var resultado = await _funcionarioService.DispensarPaciente(idPaciente, idFuncionario);

                if (!resultado.IsValid)
                    return BadRequest(resultado.Errors);

                return Ok(resultado.Data);
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

        /// <summary>
        /// Encaminha paciente pro especialista necessario, triagem/recepção/clinico...
        /// </summary>
        /// <param name="idPaciente">id do paciente que esta sendo encaminhado</param>
        /// <param name="area">área que o paciente será encaminhado, triagem = 1 /recepção = 2/clinico = 3 /ortopedia = 4/enfermaria = 5... </param>
        /// <param name="idFuncionario">id do funcionário que esta encaminhando</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Encaminhar/{idPaciente:guid}/{idFuncionario:guid}")]
        public async Task<IActionResult> EncaminharPaciente(Guid idPaciente, int area,Guid idFuncionario)
        {
            try
            {
                var resultado = await _funcionarioService.EncaminharPaciente(idPaciente,idFuncionario ,area);

                if (!resultado.IsValid)
                    return BadRequest(resultado.Errors);

                return Ok(resultado.Data);
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

        /// <summary>
        /// Muda a área do funcionário (somente admin), triagem = 1 /recepção = 2/clinico = 3 /ortopedia = 4/enfermaria = 5
        /// </summary>
        /// <param name="atualizarArea"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("atualizar/area")]
        public async Task<IActionResult> MudarAreaDoFuncionario([FromBody] AtualizarAreaDTO atualizarArea)
        {
            try
            {
                var funcionario = await _funcionarioService.MudarAreaDoFuncionario(atualizarArea.email, atualizarArea.area);

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
    }
}
