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
            Nome = nome;
            Sobrenome = sobrenome;
            Area = area;
            
        }

        public Funcionario(Guid Id,string nome, string sobrenome, EArea area)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Area = area;                  
        }

        public void Update(string nome, string sobrenome, EArea area)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                Nome = nome;
            }

            if (!string.IsNullOrEmpty(sobrenome))
            {
                Sobrenome= sobrenome;
            }

            if((int)area >= 1 && (int)area <= 5)
            {
                Area = area;
            }
           
        }

        public (bool isValid,List<string> errors) Validates()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                errors.Add("Nome não pode ser vazio");

            if (string.IsNullOrEmpty(Sobrenome))
                errors.Add("Sobrenome não pode ser vazio");

            
            return (errors.Count == 0, errors);            
        }

    }
}
