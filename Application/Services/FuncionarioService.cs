using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using Domain.Enums;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public Task<PacienteDTO> ChamarProximo()
        {
            throw new NotImplementedException();
        }

        public async Task Create(FuncionarioDTO funcionarioDTO)
        {
            await _funcionarioRepository.Create(new Funcionario(funcionarioDTO.Nome, funcionarioDTO.Sobrenome,(EArea)funcionarioDTO.Area));            
        }

        public Task Delete(FuncionarioDTO funcionarioDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FuncionarioDTO>> GetAllFuncionarios()
        {
            throw new NotImplementedException();
        }

        public async Task<ListarFuncionarioDTO> GetFuncionarioById(Guid id)
        {
            var funcionario = await _funcionarioRepository.GetFuncionario(id);
            return new ListarFuncionarioDTO(funcionario.Id,funcionario.Nome,funcionario.Sobrenome,funcionario.Area);
        }

        public Task Update(FuncionarioDTO funcionarioDTO)
        {
            throw new NotImplementedException();
        }
    }
}
