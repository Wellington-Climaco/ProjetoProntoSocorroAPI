using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.PacienteDTOs
{
    public class ListarPacienteDTO
    {
        public ListarPacienteDTO(Guid id,string nome,string sobrenome,string documento,DateTime dataNascimento,string preferencial,string statusAtendimento,DateTime dataCriacao)
        {
            Id = id;
            Nome = $"{nome} {sobrenome}";
            Documento = documento ;
            DataNascimento = dataNascimento.ToString("dd/MM/yyyy");
            Preferencial = preferencial;
            StatusAtendimento = statusAtendimento;
            Datacriacao = dataCriacao.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public ListarPacienteDTO()
        {
            
        }

        public Guid Id { get; private set; }

        public string Nome { get; set; }

        public string Documento { get; set; }

        public string DataNascimento { get; set; }

        public string Preferencial { get; set; }

        public int StatusPreferencial { get; set; } = 0;
        public string StatusAtendimento { get; set; }
        public string Datacriacao { get; private set; }
    }
}
