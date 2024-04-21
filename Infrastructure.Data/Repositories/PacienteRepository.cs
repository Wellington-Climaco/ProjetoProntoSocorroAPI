using Domain.Entities;
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
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _dbContext;
        public PacienteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Paciente> Create(Paciente paciente)
        {
            await _dbContext.AddAsync(paciente);
            await _dbContext.SaveChangesAsync();
            return paciente;
        }

        public async Task<IEnumerable<Paciente>> GetAll()
        {
            return await _dbContext.Pacientes.AsNoTracking().ToListAsync();
        }

        public async Task<Paciente> GetPaciente(Guid id)
        {
            return await _dbContext.Pacientes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Paciente> Remove(Paciente paciente)
        {
            _dbContext.Remove(paciente);
            await _dbContext.SaveChangesAsync();
            return paciente;
        }

        public async Task<Paciente> Update(Paciente paciente)
        {            
            _dbContext.Update(paciente);
            await _dbContext.SaveChangesAsync();
            return paciente;
        }
    }
}
