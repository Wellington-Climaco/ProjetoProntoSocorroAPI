using Domain.Enums;
using Domain.Validate;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entities
{
    public sealed class Funcionario : EntidadeBase
    {
        public string Nome {  get; private set; }
        public string Sobrenome { get; private set; }
        public EArea Area { get; private set; }
        public string Email { get; private set; }
        public Senha Senha { get; private set; }
        public List<string> Notifications = new();
        public bool IsValid { get => Notifications.Count == 0; }

        public Funcionario()
        {
            
        }

        public Funcionario(string nome,string sobrenome,EArea area,string email,Senha senha)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Area = area;
            Email = email;
            Senha = senha;

            if (!Enum.IsDefined(typeof(EArea), area))
                Notifications.Add("Area inválida");

            Validate();
        }

        public Funcionario(Guid Id,string nome, string sobrenome, EArea area,string email, Senha senha)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Area = area;       
            Email = email;
            Senha = senha;

            if (!Enum.IsDefined(typeof(EArea), area))
                Notifications.Add("Area inválida");

            Validate();
        }

        public void Update(string? nome, string? sobrenome,string? email,string? senha)
        {
            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrWhiteSpace(nome))
            {
                Nome = nome;
            }

            if (!string.IsNullOrEmpty(sobrenome) && !string.IsNullOrWhiteSpace(sobrenome))
            {
                Sobrenome= sobrenome;
            }

            if(!string.IsNullOrEmpty(email) && !string.IsNullOrWhiteSpace(email))
            {
                Email = email.Trim();
            }

            if(!string.IsNullOrEmpty(senha) && !string.IsNullOrWhiteSpace(senha))
            {
                var result = new Senha(senha);
                Senha = result;
            }

            //if((int)area >= 1 && (int)area <= 5)
            //{
            //    Area = area;
            //}

        }

        public bool ChangeArea(EArea area)
        {
            if ((int)area >= 1 && (int)area <= 5)
            {
                Area = area;
                return true;
            }
            return false;
        }


        private void Validate()
        {
            if (string.IsNullOrEmpty(Nome))
                Notifications.Add("Nome não pode ser vazio");

            if (string.IsNullOrEmpty(Email))
                Notifications.Add("Email não pode ser vazio");

            if (string.IsNullOrEmpty(Sobrenome))
                Notifications.Add("Sobrenome não pode ser vazio");

            if (!string.IsNullOrEmpty(Senha.ToString()))
                Notifications.Add("Senha não pode ser vazia");
        }

    }
}
