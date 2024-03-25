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
        Task<ResultDTO<ListarFuncionarioDTO>> GetFuncionarioById(Guid id);

        Task<ResultDTO<IEnumerable<ListarFuncionarioDTO>>> GetAllFuncionarios();

        Task<ResultDTO<ListarFuncionarioDTO>> Create(FuncionarioDTO funcionarioDTO);

        Task<ResultDTO<string>> Delete(Guid id);

        Task<ResultDTO<AtualizarFuncionarioDTO>> Update(AtualizarFuncionarioDTO funcionarioDTO,Guid id);

        Task<PacienteDTO> ChamarProximo();
    }
}
