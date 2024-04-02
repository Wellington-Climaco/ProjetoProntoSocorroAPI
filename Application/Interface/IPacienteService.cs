using Application.DTOs;
using Application.DTOs.PacienteDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IPacienteService
    {
        Task<ResultDTO<ListarPacienteDTO>> CreatePaciente(PacienteDTO pacienteDTO);

        Task<ResultDTO<IEnumerable<ListarPacienteDTO>>> GetAllPacientes();

        Task<ResultDTO<ListarPacienteDTO>> GetById(Guid id);

        Task<ResultDTO<UpdatePacienteDTO>> UpdatePaciente(UpdatePacienteDTO pacienteDTO,Guid id);

        Task<ResultDTO<string>> RemoverPaciente(Guid id);

    }
}
