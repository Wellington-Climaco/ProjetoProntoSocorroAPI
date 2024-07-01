using Application.DTOs;
using Application.DTOs.FuncionarioDTOs;
using Application.DTOs.PacienteDTOs;
using Application.Interface;
using Domain.Entities;
using Domain.Enums;
using Domain.Interface;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly ITokenService _tokenService;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IPacienteRepository _pacienteRepository;
        public FuncionarioService(IFuncionarioRepository funcionarioRepository, IPacienteRepository pacienteRepository, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _funcionarioRepository = funcionarioRepository;
            _pacienteRepository = pacienteRepository;
        }

        private static int pacienteNaoPreferencial = 0;

        public async Task<ResultDTO<ListarPacienteDTO>> ChamarProximo(Guid id)
        {
            var funcionario = await _funcionarioRepository.GetFuncionario(id);

            if (funcionario == null)
                return new ResultDTO<ListarPacienteDTO>("Funcionário não encontrado");

            if (funcionario.EmAtendimento)
                return new ResultDTO<ListarPacienteDTO>("Dispense o paciente atual para chamar o próximo!!");

            var existemNaoPreferenciais = await _funcionarioRepository.ExistemPacientesNaoPreferenciais(funcionario);
            var existemPreferenciais = await _funcionarioRepository.ExistemPacientesPreferenciais(funcionario);

            if (pacienteNaoPreferencial < 2 && existemNaoPreferenciais)
            {
                pacienteNaoPreferencial++;
                var paciente = await _funcionarioRepository.ChamarProximoNãoPreferencial(funcionario);

                if (paciente == null)
                    return new ResultDTO<ListarPacienteDTO>("fila vazia");


                var pacienteToDTO = new ListarPacienteDTO(paciente.Id, paciente.Nome, paciente.Sobrenome, paciente.Documento,
                    paciente.DataNascimento, paciente.Preferencial.ToString(), paciente.StatusAtendimento.ToString(), paciente.Datacriacao);

                funcionario.MudarAtendimento();
                return new ResultDTO<ListarPacienteDTO>(pacienteToDTO, true);
            }
            else if (existemPreferenciais)
            {
                pacienteNaoPreferencial = 0;
                var paciente = await _funcionarioRepository.ChamarProximoPreferencial(funcionario);

                if (paciente == null)
                    return new ResultDTO<ListarPacienteDTO>("fila vazia");

                var pacienteToDTO = new ListarPacienteDTO(paciente.Id, paciente.Nome, paciente.Sobrenome, paciente.Documento,
                    paciente.DataNascimento, paciente.Preferencial.ToString(), paciente.StatusAtendimento.ToString(), paciente.Datacriacao);

                funcionario.MudarAtendimento();
                await _funcionarioRepository.Update(funcionario);
                return new ResultDTO<ListarPacienteDTO>(pacienteToDTO, true);

            }
            else
            {
                return new ResultDTO<ListarPacienteDTO>("fila vazia");
            }
        }

        public async Task<ResultDTO<ListarFuncionarioDTO>> Create(FuncionarioDTO funcionarioDTO)
        {
            var funcionarioExist = _funcionarioRepository.GetFuncionarioByEmail(funcionarioDTO.Email);

            if (funcionarioExist != null)
                return new ResultDTO<ListarFuncionarioDTO>("Já existe cadastro com este email");

            var funcionario = new Funcionario(funcionarioDTO.Nome, funcionarioDTO.Sobrenome, (EArea)funcionarioDTO.Area, funcionarioDTO.Email, funcionarioDTO.Senha);

            if (funcionario.IsValid)
            {
                await _funcionarioRepository.Create(funcionario);
                var result = new ListarFuncionarioDTO(funcionario.Id, funcionario.Nome, funcionario.Sobrenome, funcionario.Area, funcionario.Datacriacao, funcionario.Email);
                return new ResultDTO<ListarFuncionarioDTO>(result, true);
            }

            return new ResultDTO<ListarFuncionarioDTO>(funcionario.Notifications);

        }

        public async Task<ResultDTO<string>> Delete(Guid id)
        {
            var funcionario = await _funcionarioRepository.GetFuncionario(id);

            if (funcionario == null) return new ResultDTO<string>("Funcionário não encontrado");

            await _funcionarioRepository.Remove(funcionario);
            return new ResultDTO<string>("Removido com sucesso!!", true);

        }

        public async Task<ResultDTO<IEnumerable<ListarFuncionarioDTO>>> GetAllFuncionarios()
        {
            var funcionarios = await _funcionarioRepository.GetAll();
            if (funcionarios == null) return new ResultDTO<IEnumerable<ListarFuncionarioDTO>>("Não existem funcionários");

            var funcionarioDTO = new List<ListarFuncionarioDTO>();

            foreach (var funcionario in funcionarios)
            {
                var toDTO = new ListarFuncionarioDTO(funcionario.Id, funcionario.Nome, funcionario.Sobrenome, funcionario.Area, funcionario.Datacriacao, funcionario.Email);
                funcionarioDTO.Add(toDTO);
            }

            return new ResultDTO<IEnumerable<ListarFuncionarioDTO>>(funcionarioDTO, true);

        }

        public async Task<ResultDTO<ListarFuncionarioDTO>> GetFuncionarioById(Guid id)
        {
            var funcionarioEntity = await _funcionarioRepository.GetFuncionario(id);
            if (funcionarioEntity == null) return new ResultDTO<ListarFuncionarioDTO>("Funcionário não encontrado");

            var funcionarioDTO = new ListarFuncionarioDTO(funcionarioEntity.Id, funcionarioEntity.Nome, funcionarioEntity.Sobrenome, funcionarioEntity.Area, funcionarioEntity.Datacriacao, funcionarioEntity.Email);
            return new ResultDTO<ListarFuncionarioDTO>(funcionarioDTO, true);
        }

        public async Task<ResultDTO<AtualizarFuncionarioDTO>> Update(AtualizarFuncionarioDTO funcionarioDTO, Guid id)
        {
            var funcionario = await _funcionarioRepository.GetFuncionario(id);
            if (funcionario == null) return new ResultDTO<AtualizarFuncionarioDTO>("Funcionário não encontrado");

            var funcionarioExist = await _funcionarioRepository.GetFuncionarioByEmail(funcionarioDTO.Email);

            if (funcionarioExist != null)
                return new ResultDTO<AtualizarFuncionarioDTO>("Já existe cadastro com este email");

            funcionario.Update(funcionarioDTO.Nome, funcionarioDTO.Sobrenome, funcionarioDTO.Email, funcionarioDTO.Senha);

            if (!funcionario.IsValid)
                return new ResultDTO<AtualizarFuncionarioDTO>(funcionario.Notifications);

            await _funcionarioRepository.Update(funcionario);

            var funcionarioToDTO = new AtualizarFuncionarioDTO { Nome = funcionario.Nome, Sobrenome = funcionario.Sobrenome, Email = funcionario.Email };
            return new ResultDTO<AtualizarFuncionarioDTO>(funcionarioToDTO, true);
        }

        public async Task<ResultDTO<string>> DispensarPaciente(Guid id, Guid idFuncionario)
        {
            var paciente = await _pacienteRepository.GetPaciente(id);
            var funcionario = await _funcionarioRepository.GetFuncionario(idFuncionario);

            if (paciente == null)
                return new ResultDTO<string>("Paciente não encontrado");

            if (funcionario == null)
                return new ResultDTO<string>("Funcionario não encontrado");

            funcionario.MudarAtendimento();
            await _funcionarioRepository.Update(funcionario);

            await _pacienteRepository.Remove(paciente);
            return new ResultDTO<string>("Paciente dispensado", true);
        }

        public async Task<ResultDTO<string>> EncaminharPaciente(Guid idPaciente, Guid idFuncionario, int area)
        {
            var paciente = await _pacienteRepository.GetPaciente(idPaciente);
            var funcionario = await _funcionarioRepository.GetFuncionario(idFuncionario);

            if (paciente == null)
                return new ResultDTO<string>("Paciente não encontrado");

            if (funcionario == null)
                return new ResultDTO<string>("Funcionario não encontrado");

            paciente.Encaminhar((EStatusAtendimento)area);

            if (!paciente.IsValid)
                return new ResultDTO<string>(paciente.Notifications);

            paciente.MudarAtendimento();
            funcionario.MudarAtendimento();

            await _pacienteRepository.Update(paciente);
            await _funcionarioRepository.Update(funcionario);

            return new ResultDTO<string>("Paciente encaminhado", true);

        }

        public async Task<ResultDTO<FuncionarioAuthenticateDTO>> AutenticarFuncionario(string email, string senha)
        {
            var funcionario = await _funcionarioRepository.GetFuncionarioByEmail(email);

            if (funcionario == null)
                return new ResultDTO<FuncionarioAuthenticateDTO>("Email incorreto");

            var senhaValida = await _funcionarioRepository.VerificarSeSenhaEstaCorreta(funcionario, senha);

            if (!senhaValida)
                return new ResultDTO<FuncionarioAuthenticateDTO>("senha incorreta");

            var token = _tokenService.GenerateToken(funcionario);

            var funcionarioToDTO = new FuncionarioAuthenticateDTO(token, funcionario.Id.ToString(), funcionario.Nome, funcionario.Email, funcionario.Area.ToString());
            return new ResultDTO<FuncionarioAuthenticateDTO>(funcionarioToDTO);
        }

        public async Task<ResultDTO<ListarFuncionarioDTO>> MudarAreaDoFuncionario(string email, int area)
        {
            var funcionario = await _funcionarioRepository.GetFuncionarioByEmail(email);

            if (funcionario == null)
                return new ResultDTO<ListarFuncionarioDTO>("Funcionário não encontrado");

            var senhaAlterada = funcionario.ChangeArea((EArea)area);

            if (!senhaAlterada)
                return new ResultDTO<ListarFuncionarioDTO>("Area inválida");

            await _funcionarioRepository.Update(funcionario);

            var funcionarioDTO = new ListarFuncionarioDTO(funcionario.Id, funcionario.Nome, funcionario.Sobrenome, funcionario.Area, funcionario.Datacriacao, funcionario.Email);

            return new ResultDTO<ListarFuncionarioDTO>(funcionarioDTO);
        }
    }
}
