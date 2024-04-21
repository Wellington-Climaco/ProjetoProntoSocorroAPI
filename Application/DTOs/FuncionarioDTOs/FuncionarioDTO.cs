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
    public class FuncionarioDTO
    {

        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        [MinLength(3, ErrorMessage = "Nome deve ter no minímo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "Nome deve ter no máximo 20 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome é um campo obrigatório")]
        [MinLength(3, ErrorMessage = "Sobrenome deve ter no minímo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "Sobrenome deve ter no máximo 20 caracteres")]
        public string Sobrenome { get; set; }

        [Range(1, 6, ErrorMessage = "Somente numeros de 1 a 6")]
        public int Area { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigatório")]
        [MinLength(9, ErrorMessage = "Senha deve ter no minímo 9 caracteres")]
        [MaxLength(30,ErrorMessage = "Senha deve ter no máximo 30 caracteres")]
        public string Senha {  get; set; }

        
        [Required(ErrorMessage = "Email é um campo obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage ="Email inválido")]
        public string Email { get; set; }

        public DateTime Datacriação { get; private set; }

    }
}
