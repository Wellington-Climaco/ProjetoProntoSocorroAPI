using Application.DTOs;
using Application.DTOs.FuncionarioDTOs;
using Application.DTOs.PacienteDTOs;
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

        private int pacienteNaoPreferencial = 0;
        private int pacientePreferencial = 0;

        public async Task<ResultDTO<ListarPacienteDTO>> ChamarProximo(Guid id)
        {
            var funcionario = await _funcionarioRepository.GetFuncionario(id);

            if(pacienteNaoPreferencial <= 2)
            {
                pacienteNaoPreferencial++;
                var paciente = await _funcionarioRepository.ChamarProximoNãoPreferencial(funcionario);

                if (paciente == null)
                    return new ResultDTO<ListarPacienteDTO>("fila vazia");
                
                var pacienteToDTO = new ListarPacienteDTO(paciente.Id,paciente.Nome,paciente.Sobrenome, paciente.Documento,
                    paciente.DataNascimento, paciente.Preferencial.ToString(), paciente.StatusAtendimento.ToString(), paciente.Datacriacao);
                return new ResultDTO<ListarPacienteDTO>(pacienteToDTO);
            }
            else
            {
                pacienteNaoPreferencial = 0;
                var paciente = await _funcionarioRepository.ChamarProximoPreferencial(funcionario.Area);

                if (paciente == null)
                    return new ResultDTO<ListarPacienteDTO>("fila vazia");

                var pacienteToDTO = new ListarPacienteDTO(paciente.Id, paciente.Nome, paciente.Sobrenome, paciente.Documento,
                    paciente.DataNascimento, paciente.Preferencial.ToString(), paciente.StatusAtendimento.ToString(), paciente.Datacriacao);
                return new ResultDTO<ListarPacienteDTO>(pacienteToDTO);
            }


            
        }

        public async Task<ResultDTO<ListarFuncionarioDTO>> Create(FuncionarioDTO funcionarioDTO)
        {
            var funcionario = new Funcionario(funcionarioDTO.Nome, funcionarioDTO.Sobrenome, (EArea)funcionarioDTO.Area);
            var resultValidate = funcionario.Validates();              

            if(resultValidate.isValid)
            {
                await _funcionarioRepository.Create(funcionario);
                var result = new ListarFuncionarioDTO(funcionario.Id, funcionario.Nome, funcionario.Sobrenome, funcionario.Area, funcionario.Datacriacao);
                return new ResultDTO<ListarFuncionarioDTO>(result);           
            }

            return new ResultDTO<ListarFuncionarioDTO>(resultValidate.errors);
                              
        }

        public async Task<ResultDTO<string>> Delete(Guid id)
        {
            var funcionario = await _funcionarioRepository.GetFuncionario(id);

            if (funcionario == null) return new ResultDTO<string>("Funcionário não encontrado");

            await _funcionarioRepository.Remove(funcionario);
            return new ResultDTO<string>("Removido com sucesso!!");

        }

        public async Task<ResultDTO<IEnumerable<ListarFuncionarioDTO>>> GetAllFuncionarios()
        {
            var funcionarios = await _funcionarioRepository.GetAll();
            if(funcionarios == null) return new ResultDTO<IEnumerable<ListarFuncionarioDTO>>("Não existem funcionários");
           
            var funcionarioDTO = new List<ListarFuncionarioDTO>();
            
            foreach (var funcionario in funcionarios)
            {
                var toDTO = new ListarFuncionarioDTO(funcionario.Id, funcionario.Nome, funcionario.Sobrenome, funcionario.Area, funcionario.Datacriacao);  
                funcionarioDTO.Add(toDTO);
            }

            return new ResultDTO<IEnumerable<ListarFuncionarioDTO>>(funcionarioDTO);
            
        }

        public async Task<ResultDTO<ListarFuncionarioDTO>> GetFuncionarioById(Guid id)
        {
            var funcionarioEntity = await _funcionarioRepository.GetFuncionario(id);              
            if (funcionarioEntity == null) return new ResultDTO<ListarFuncionarioDTO>("Funcionário não encontrado");

            var funcionarioDTO = new ListarFuncionarioDTO(funcionarioEntity.Id,funcionarioEntity.Nome,funcionarioEntity.Sobrenome,funcionarioEntity.Area,funcionarioEntity.Datacriacao);
            return new ResultDTO<ListarFuncionarioDTO>(funcionarioDTO);
        }

        public async Task<ResultDTO<AtualizarFuncionarioDTO>> Update(AtualizarFuncionarioDTO funcionarioDTO, Guid id)
        {
            var funcionario = await _funcionarioRepository.GetFuncionario(id);
            if (funcionario == null) return new ResultDTO<AtualizarFuncionarioDTO>("Funcionário não encontrado");
                       
            funcionario.Update(funcionarioDTO.Nome, funcionarioDTO.Sobrenome, (EArea)funcionarioDTO.Area);  

            var validacao = funcionario.Validates();
            if (!validacao.isValid)
                return new ResultDTO<AtualizarFuncionarioDTO>(validacao.errors);

            await _funcionarioRepository.Update(funcionario);

            var funcionarioToDTO = new AtualizarFuncionarioDTO { Nome = funcionario.Nome, Sobrenome = funcionario.Sobrenome, Area = (int)funcionario.Area };           
            return new ResultDTO<AtualizarFuncionarioDTO>(funcionarioToDTO);
            

        }
    }
}
