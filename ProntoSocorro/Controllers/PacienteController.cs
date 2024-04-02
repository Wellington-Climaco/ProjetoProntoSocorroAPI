using Application.DTOs.PacienteDTOs;
using Application.Interface;
using Domain.Entities;
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

        [HttpGet("buscar")]
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
        
        [HttpPut("atualizar/{id:guid}")]
        public async Task<IActionResult> AtualizarPaciente(Guid id,UpdatePacienteDTO pacienteDTO)
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




    }
}
