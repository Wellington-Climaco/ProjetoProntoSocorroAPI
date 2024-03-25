using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PacienteDTO
    {
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        [MinLength(3)]
        [MaxLength(20)]
        public string Nome { get;  set; }

        [Required(ErrorMessage = "Sobrenome é um campo obrigatório")]
        [MinLength(3)]
        [MaxLength(20)]
        public string Sobrenome { get;  set; }

        [Required(ErrorMessage = "Documento é um campo obrigatório")]
        [Length(9, 12, ErrorMessage = "Digite um documento valido")]
        public string Documento { get;  set; }

        [Required(ErrorMessage = "Data de Nascimento é um campo obrigatório")]
        public DateTime DataNascimento { get;  set; }

        [Required(ErrorMessage = "Preferencial é um campo obrigatório")]
        [MaxLength(1)]
        public int Preferencial { get;  set; }

        public int StatusPreferencial { get;  set; }
        public int StatusAtendimento { get;  set; }
        public DateTime Datacriação { get; private set; }
    }
}
