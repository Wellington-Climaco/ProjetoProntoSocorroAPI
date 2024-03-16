using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IPacienteRepository
    {
         Task<Paciente> GetPaciente(int id);

         Task<IEnumerable<Paciente>> GetAll();

         Task<Paciente> Remove(Paciente paciente);

         Task<Paciente> Update(Paciente paciente);

         Task<Paciente> Create(Paciente paciente);
    }
}
