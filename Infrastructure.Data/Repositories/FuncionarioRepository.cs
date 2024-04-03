﻿using Domain.Entities;
using Domain.Enums;
using Domain.Interface;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IPacienteRepository _pacienteRepository;
        public FuncionarioRepository(AppDbContext appDbContext, IPacienteRepository pacienteRepository)
        {
            _dbContext = appDbContext;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Paciente> ChamarProximoNãoPreferencial(Funcionario funcionario)
        {
            var pacientes = await _dbContext.Pacientes.Where(x => x.Preferencial == EPreferencial.N && x.EmAtendimento == false).ToListAsync();

            List<Paciente> proximo = new List<Paciente>();
            foreach (var person in pacientes)
            {
                if (person.StatusAtendimento.ToString() == funcionario.Area.ToString())
                {
                    proximo.Add(person);
                }
            }

            var paciente = proximo.OrderBy(x => x.Datacriacao).First();
            paciente.ParaAtendimento();
            await _pacienteRepository.Update(paciente);
            return paciente;
        }

        public async Task<Paciente> ChamarProximoPreferencial(Funcionario funcionario)
        {
            var pacientes = await _dbContext.Pacientes.Where(x => x.Preferencial == EPreferencial.S && x.EmAtendimento == false).ToListAsync();

            List<Paciente> proximo = new List<Paciente>();
            foreach (var person in pacientes)
            {
                if (person.StatusAtendimento.ToString() == funcionario.Area.ToString())
                {
                    proximo.Add(person);
                }
            }

            var paciente = proximo.OrderBy(x => x.Datacriacao).First();
            paciente.ParaAtendimento();
            await _pacienteRepository.Update(paciente);
            return paciente;
        }

        public async Task<IEnumerable<Funcionario>> GetAll()
        {
            return await _dbContext.Funcionarios.AsNoTracking().ToListAsync();

        }

        public async Task<Funcionario> GetFuncionario(Guid id)
        {
            var funcionario = await _dbContext.Funcionarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return funcionario;
        }

        public async Task<Funcionario> Create(Funcionario funcionario)
        {                       
            await _dbContext.AddAsync(funcionario);
            await _dbContext.SaveChangesAsync();
            return funcionario;
        }

        public async Task<Funcionario> Remove(Funcionario funcionario)
        {       
            _dbContext.Remove(funcionario);
            await _dbContext.SaveChangesAsync();
            return funcionario;
        }

        public async Task<Funcionario> Update(Funcionario funcionario)
        {
            _dbContext.Update(funcionario);
            await _dbContext.SaveChangesAsync();
            return funcionario;
        }
    }
}
