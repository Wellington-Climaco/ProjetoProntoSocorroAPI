using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IFuncionarioService
    {
        Task<ListarFuncionarioDTO> GetFuncionarioById(Guid id);

        Task<IEnumerable<FuncionarioDTO>> GetAllFuncionarios();

        Task Create(FuncionarioDTO funcionarioDTO);

        Task Delete(FuncionarioDTO funcionarioDTO);

        Task Update(FuncionarioDTO funcionarioDTO);

        Task<PacienteDTO> ChamarProximo();
    }
}
