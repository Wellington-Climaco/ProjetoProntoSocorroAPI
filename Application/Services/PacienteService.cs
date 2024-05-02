using Application.DTOs;
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
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<ResultDTO<ListarPacienteDTO>> CreatePaciente(PacienteDTO pacienteDTO)
        {
            var exist = _pacienteRepository.GetByDocument(pacienteDTO.Documento);

            if (exist != null)
                return new ResultDTO<ListarPacienteDTO>("Já existe paciente no sistema com este documento");

            var paciente = new Paciente(pacienteDTO.Nome, pacienteDTO.Sobrenome, pacienteDTO.Documento, pacienteDTO.DataNascimento, (EPreferencial)pacienteDTO.Preferencial);
            
            if (!paciente.IsValid)
            {                          
                return new ResultDTO<ListarPacienteDTO>(paciente.Notifications);
            }

            await _pacienteRepository.Create(paciente);
            var result = new ListarPacienteDTO(paciente.Id, paciente.Nome, paciente.Sobrenome, paciente.Documento,
                paciente.DataNascimento, paciente.Preferencial.ToString(), paciente.StatusAtendimento.ToString(), paciente.Datacriacao);
            return new ResultDTO<ListarPacienteDTO>(result, true);
        }

        public async Task<ResultDTO<IEnumerable<ListarPacienteDTO>>> GetAllPacientes()
        {
            var pacientes = await _pacienteRepository.GetAll();
            if (pacientes == null) return new ResultDTO<IEnumerable<ListarPacienteDTO>>("Não existem pacientes");

            var pacienteDTO = new List<ListarPacienteDTO>();

            foreach(var person in pacientes)
            {
                var toDTO = new ListarPacienteDTO(person.Id, person.Nome, person.Sobrenome, person.Documento, person.DataNascimento, person.Preferencial.ToString(), person.StatusAtendimento.ToString(), person.Datacriacao);
                pacienteDTO.Add(toDTO);
            }

            return new ResultDTO<IEnumerable<ListarPacienteDTO>>(pacienteDTO, true);
        }

        public async Task<ResultDTO<ListarPacienteDTO>> GetByDocument(string document)
        {
            var paciente = await _pacienteRepository.GetByDocument(document);

            if (paciente == null)
                return new ResultDTO<ListarPacienteDTO>("Paciente não encontrado");

            var toDTO = new ListarPacienteDTO(paciente.Id, paciente.Nome, paciente.Sobrenome, paciente.Documento,
                paciente.DataNascimento, paciente.Preferencial.ToString(), paciente.StatusAtendimento.ToString(), paciente.Datacriacao);

            return new ResultDTO<ListarPacienteDTO>(toDTO,true);
        }

        public async Task<ResultDTO<ListarPacienteDTO>> GetById(Guid id)
        {
            var paciente = await _pacienteRepository.GetPaciente(id);
            if (paciente == null) return new ResultDTO<ListarPacienteDTO>("Paciente não encontrado");

            var toDTO = new ListarPacienteDTO(paciente.Id, paciente.Nome, paciente.Sobrenome, paciente.Documento, paciente.DataNascimento, paciente.Preferencial.ToString(),paciente.StatusAtendimento.ToString(), paciente.Datacriacao);
            return new ResultDTO<ListarPacienteDTO>(toDTO, true);

        }

        public async Task<ResultDTO<IEnumerable<ListarPacienteDTO>>> GetByName(string name)
        {
            var pacientes = await _pacienteRepository.GetByName(name);

            if (!pacientes.Any())
                return new ResultDTO<IEnumerable<ListarPacienteDTO>>("Nenhum paciente encontrado");

            var pacientesDTO = new List<ListarPacienteDTO>();

            foreach (var paciente in pacientes)
            {
                var toDTO = new ListarPacienteDTO(paciente.Id, paciente.Nome, paciente.Sobrenome, paciente.Documento, paciente.DataNascimento, paciente.Preferencial.ToString(), paciente.StatusAtendimento.ToString(), paciente.Datacriacao);

                pacientesDTO.Add(toDTO);
            }

            return new ResultDTO<IEnumerable<ListarPacienteDTO>>(pacientesDTO, true);
        }

        public async Task<ResultDTO<string>> RemoverPaciente(Guid id)
        {
            var paciente = await _pacienteRepository.GetPaciente(id);

            if (paciente == null)
                return new ResultDTO<string>("Paciente não encontrado");

            await _pacienteRepository.Remove(paciente);
            return new ResultDTO<string>("excluido com sucesso", true);                
        }

        public async Task<ResultDTO<UpdatePacienteDTO>> UpdatePaciente(UpdatePacienteDTO pacienteDTO,Guid id)
        {
            var paciente = await _pacienteRepository.GetPaciente(id);

            if (paciente == null)
                return new ResultDTO<UpdatePacienteDTO>("Paciente não encontrado");

            paciente.Update(pacienteDTO.Nome, pacienteDTO.Sobrenome, pacienteDTO.Documento, pacienteDTO.DataNascimento, (EPreferencial)pacienteDTO.Preferencial);

            await _pacienteRepository.Update(paciente);

            var pacienteToDTO = new UpdatePacienteDTO { Nome = paciente.Nome,Sobrenome = paciente.Sobrenome,DataNascimento=paciente.DataNascimento,
                Documento=paciente.Documento,Preferencial = (int)paciente.Preferencial};

            return new ResultDTO<UpdatePacienteDTO>(pacienteToDTO, true);
        }

        
    }
}
