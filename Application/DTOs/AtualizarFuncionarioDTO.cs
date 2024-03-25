using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AtualizarFuncionarioDTO
    {

        [JsonIgnore]
        public Guid Id { get; private set; }
        
        [StringLength(20,MinimumLength = 3, ErrorMessage = "Minimo 3 Maximo 20 caracteres")]
        public string? Nome { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimo 3 Maximo 20 caracteres")]
        public string? Sobrenome { get; set; }
      
        public int Area { get; set; }

    }
}
