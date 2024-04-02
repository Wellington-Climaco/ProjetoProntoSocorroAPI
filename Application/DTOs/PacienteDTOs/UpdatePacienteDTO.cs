using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.PacienteDTOs
{
    public class UpdatePacienteDTO
    {
        [JsonIgnore]
        public Guid Id { get; private set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimo 3 Maximo 20 caracteres")]
        public string? Nome { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimo 3 Maximo 20 caracteres")]
        public string? Sobrenome { get; set; }

        [Length(9, 12, ErrorMessage = "Digite um documento valido")]
        public string? Documento { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [RegularExpression("^\\d$",ErrorMessage = "insira somente 1 digito")]
        public int Preferencial { get; set; }
    }
}
