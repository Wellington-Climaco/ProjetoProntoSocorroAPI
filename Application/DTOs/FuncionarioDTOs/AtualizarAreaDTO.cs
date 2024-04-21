using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.FuncionarioDTOs
{
    public class AtualizarAreaDTO
    {
        [Required(ErrorMessage = "Email é um campo obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email inválido")]
        public string email {  get; set; }

        [Required(ErrorMessage = "Area é um campo obrigatório")]
        [Range(1, 6, ErrorMessage = "Somente numeros de 1 a 6")]
        public int area {  get; set; }
    }
}
