using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ListarFuncionarioDTO
    {
        public ListarFuncionarioDTO(Guid id, string nome, string sobrenome, EArea area)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            EnumArea = area;
        }
        
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        [MinLength(3)]
        [MaxLength(20)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome é um campo obrigatório")]
        [MinLength(3)]
        [MaxLength(20)]
        public string Sobrenome { get; set; }
       
        public EArea EnumArea { get; set; }

        [JsonIgnore]
        public Paciente Paciente { get; set; }

        public DateTime Datacriação { get; private set; }
    }
}
