using Domain.Entities;
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
        public FuncionarioRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<Paciente> ChamarProximoNãoPreferencial()
        {
            var paciente = await _dbContext.Pacientes.Where(x=>x.StatusAtendimento.ToString() == x.Funcionario.Area.ToString()
            && x.Preferencial == EPreferencial.N).OrderBy(x=>x.Datacriação).FirstAsync();
            return paciente;
        }

        public async Task<Paciente> ChamarProximoPreferencial()
        {
            var paciente = await _dbContext.Pacientes.Where(x => x.StatusAtendimento.ToString() == x.Funcionario.Area.ToString()
            && x.Preferencial == EPreferencial.S).OrderBy(x => x.Datacriação).FirstAsync();
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
