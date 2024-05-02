using Application.DTOs.PacienteDTOs;
using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProntoSocorro.Controllers
{

    [ApiController]
    [Route("v1/paciente/")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        /// <summary>
        /// Adiciona paciente no sistema. (Recepção,Admin)
        /// </summary>
        /// <param name="pacienteDTO"></param>
        /// <returns></returns>
        [Authorize(Roles = "Recepcao,Admin")]
        [HttpPost("criar")]
        public async Task<IActionResult> CriarPaciente(PacienteDTO pacienteDTO)
        {
            try
            {
                var paciente = await _pacienteService.CreatePaciente(pacienteDTO);

                if (!paciente.IsValid)
                    return BadRequest(paciente.Errors);

                return Created($"v1/funcionario/buscar/{paciente.Data.Id}", paciente.Data);
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

        [Authorize]
        [HttpGet("buscarTodos")]
        public async Task<IActionResult> BuscarTodosPacientes()
        {
            try
            {
                var pacientes = await _pacienteService.GetAllPacientes();

                if (!pacientes.IsValid)
                    return BadRequest(pacientes.Errors);

                return Ok(pacientes.Data);
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }

        [Authorize]
        [HttpGet("buscar/{id:guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            try
            {
                var paciente = await _pacienteService.GetById(id);

                if (!paciente.IsValid)
                    return NotFound(paciente.Errors);

                return Ok(paciente.Data);
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }

        /// <summary>
        /// Atualiza os dados do paciente. (admin / recepção)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pacienteDTO"></param>
        /// <returns></returns>
        [Authorize(Roles ="Admin,Recepcao")]
        [HttpPut("atualizar/{id:guid}")]
        public async Task<IActionResult> AtualizarDadosPaciente(Guid id,UpdatePacienteDTO pacienteDTO)
        {
            try
            {
                var paciente = await _pacienteService.UpdatePaciente(pacienteDTO,id);

                if(!paciente.IsValid)
                    return BadRequest(paciente.Errors);

                return Ok(paciente.Data);
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
        /// (admin)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Clinico,Recepcao")]
        [HttpDelete("remover/{id:guid}")]
        public async Task<IActionResult> RemoverPaciente([FromRoute]Guid id)
        {
            try
            {
                var paciente = await _pacienteService.RemoverPaciente(id);

                if(!paciente.IsValid)
                    return NotFound(paciente.Errors);

                return Ok(paciente.Data);

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

        [Authorize]
        [HttpGet("buscarPorNome/{nome}")]
        public async Task<IActionResult> BuscarPorNome([FromRoute] string nome)
        {
            try
            {
                var paciente = await _pacienteService.GetByName(nome);

                if (!paciente.IsValid)
                    return NotFound(paciente.Errors);

                return Ok(paciente.Data);
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }

        [Authorize]
        [HttpGet("buscarPorDocumento/{documento}")]
        public async Task<IActionResult> BuscarPorDocumento([FromRoute] string documento)
        {
            try
            {
                var paciente = await _pacienteService.GetByDocument(documento);

                if (!paciente.IsValid)
                    return NotFound(paciente.Errors);

                return Ok(paciente.Data);
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }




    }
}
