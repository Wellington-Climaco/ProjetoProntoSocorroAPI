using Domain.Enums;
using Domain.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Funcionario : EntidadeBase
    {
        public string Nome {  get; private set; }
        public string Sobrenome { get; private set; }
        public EArea Area { get; private set; }

        public Funcionario()
        {
            
        }

        public Funcionario(string nome,string sobrenome,EArea area)
        {
            ValidateDomain(nome, sobrenome, area);
        }

        public Funcionario(Guid Id,string nome, string sobrenome, EArea area)
        {
            DomainValidation.Validate(Id == Guid.Empty, "Id inválido");
            ValidateDomain(nome, sobrenome, area);
        }

        public void Update(Guid Id, string nome, string sobrenome, EArea area)
        {
            DomainValidation.Validate(Id == Guid.Empty, "Id inválido");
            ValidateDomain(nome, sobrenome, area);
        }


        private void ValidateDomain(string nome,string sobrenome,EArea area)
        {
            DomainValidation.Validate(string.IsNullOrEmpty(nome), "Nome não pode ser vazio");
            DomainValidation.Validate(string.IsNullOrEmpty(sobrenome), "Sobrenome não pode ser vazio");

            Nome = nome;
            Sobrenome = sobrenome;
            Area = area;
        }
    }
}
