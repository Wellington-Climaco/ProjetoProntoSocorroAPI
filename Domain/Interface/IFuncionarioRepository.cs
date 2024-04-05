using Domain.Entities;
using Domain.Enums;
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

        Task<Paciente> ChamarProximoNãoPreferencial(Funcionario funcionario);

        Task<Paciente> ChamarProximoPreferencial(Funcionario funcionario);

        Task<bool> ExistemPreferenciais(Funcionario funcionario);

        Task<bool> ExistemNaoPreferenciais(Funcionario funcionario);


    }
}
