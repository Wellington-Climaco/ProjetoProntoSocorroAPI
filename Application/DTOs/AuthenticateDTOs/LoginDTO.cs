using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AuthenticateDTOs
{
    public class LoginDTO
    {
        [EmailAddress(ErrorMessage = "Email inválido")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email inválido")]
        public string email {  get; set; }

        [MinLength(9, ErrorMessage = "Senha deve ter no minímo 9 caracteres")]
        [MaxLength(30, ErrorMessage = "Senha deve ter no máximo 30 caracteres")]
        public string senha { get; set; }

    }
}
