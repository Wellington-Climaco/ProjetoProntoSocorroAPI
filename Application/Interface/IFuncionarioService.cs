using Application.DTOs;
using Application.DTOs.FuncionarioDTOs;
using Application.DTOs.PacienteDTOs;
using Domain.Enums;
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

        Task<ResultDTO<ListarPacienteDTO>> ChamarProximo(Guid id);

        Task<ResultDTO<string>> DispensarPaciente(Guid id, Guid idFuncionario);

        Task<ResultDTO<string>> EncaminharPaciente(Guid id, Guid idFuncionario, int Area);

        Task<ResultDTO<FuncionarioAuthenticateDTO>> AutenticarFuncionario(string email,string senha);

        Task<ResultDTO<ListarFuncionarioDTO>> MudarAreaDoFuncionario(string email, int area);
    }
}
