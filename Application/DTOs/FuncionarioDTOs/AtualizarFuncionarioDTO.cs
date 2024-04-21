using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.FuncionarioDTOs
{
    public class AtualizarFuncionarioDTO
    {

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimo 3 Maximo 20 caracteres")]
        public string? Nome { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimo 3 Maximo 20 caracteres")]
        public string? Sobrenome { get; set; }


        [MinLength(9, ErrorMessage = "Senha deve ter no minímo 9 caracteres")]
        [MaxLength(30, ErrorMessage = "Senha deve ter no máximo 30 caracteres")]
        public string? Senha { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email inválido")]
        public string? Email { get; set; }


    }
}
