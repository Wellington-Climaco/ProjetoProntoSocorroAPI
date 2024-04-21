using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.FuncionarioDTOs
{
    public class ListarFuncionarioDTO
    {
        public ListarFuncionarioDTO(Guid id, string nome, string sobrenome, EArea area, DateTime datacriacao, string email)
        {
            Id = id;
            Nome = $"{nome} {sobrenome}";
            EnumArea = area;
            Datacriacao = datacriacao.ToString("dd/MM/yyyy HH:mm:ss");
            Email = email;
        }

        public ListarFuncionarioDTO()
        {

        }

        public Guid? Id { get; private set; }

        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        [MinLength(3)]
        [MaxLength(20)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Email é um campo obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Range(1, 6, ErrorMessage = "Somente numeros de 1 a 6")]
        public EArea? EnumArea { get; set; }

        public string? Datacriacao { get; private set; }


    }
}
