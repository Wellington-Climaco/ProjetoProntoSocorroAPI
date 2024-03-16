using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IFuncionarioRepository
    {
        Task<Funcionario> GetFuncionario(Guid id);

        Task<IEnumerable<Funcionario>> GetAll();

        Task<Funcionario> Remove(Funcionario funcionario);

        Task<Funcionario> Update(Funcionario funcionario);

        Task<Funcionario> Create(Funcionario funcionario);

        Task<Paciente> ChamarProximoNãoPreferencial();

        Task<Paciente> ChamarProximoPreferencial();





    }
}
