using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.FuncionarioDTOs
{
    public class FuncionarioAuthenticateDTO
    {
        public FuncionarioAuthenticateDTO(string token, string id, string nome, string email, string area)
        {
            Token = token;
            Id = id;
            Nome = nome;
            Email = email;
            Area = area;
        }

        public string Token { get; set; } = string.Empty;

        public string Id { get; set; } = string.Empty;

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Area { get; set; } = string.Empty;

    }
}
