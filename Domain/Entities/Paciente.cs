using Domain.Enums;
using Domain.Validate;


namespace Domain.Entities
{
    public sealed class Paciente : EntidadeBase
    {
        public string Nome { get; private set; } = string.Empty;
        public string Sobrenome { get; private set; } = string.Empty;
        public string Documento { get; private set; } = string.Empty;
        public DateTime DataNascimento { get; private set; }
        public EPreferencial Preferencial { get; private set; }
        public EStatusPreferencial StatusPreferencial { get; private set; }
        public EStatusAtendimento StatusAtendimento { get; private set; } = EStatusAtendimento.Triagem;
        public Funcionario Funcionario { get; private set; }

        public Paciente()
        {
            
        }

        public Paciente(string nome, string sobrenome, string documento, DateTime datanascimento, EPreferencial preferencial)
        {
            ValidateDomain(nome, sobrenome, documento, datanascimento, preferencial);
        }

        public Paciente(string nome,string sobrenome,string documento,DateTime datanascimento, EPreferencial preferencial,EStatusPreferencial statusPreferencial)
        {
            ValidateDomain(nome,sobrenome,documento,datanascimento,preferencial);
            StatusPreferencial = statusPreferencial;

        }

        public Paciente(Guid id,string nome, string sobrenome, string documento, DateTime datanascimento, EPreferencial preferencial, EStatusPreferencial statusPreferencial)
        {
            DomainValidation.Validate(id == Guid.Empty, "Id inválido");
            StatusPreferencial = statusPreferencial;
            ValidateDomain(nome, sobrenome, documento, datanascimento, preferencial);
        }

        public void Update(Guid id, string nome, string sobrenome, string documento, DateTime datanascimento, EPreferencial preferencial, EStatusPreferencial statusPreferencial)
        {
            DomainValidation.Validate(id == Guid.Empty, "Id inválido");
            StatusPreferencial = statusPreferencial;
            ValidateDomain(nome, sobrenome, documento, datanascimento, preferencial);
        }

        private void ValidateDomain(string nome, string sobrenome, string documento, DateTime datanascimento, EPreferencial preferencial)
        {
            DomainValidation.Validate(string.IsNullOrEmpty(nome), "Nome não pode ser vazio");
            DomainValidation.Validate(string.IsNullOrEmpty(sobrenome), "SobreNome não pode ser vazio");
            DomainValidation.Validate(string.IsNullOrEmpty(documento), "Documento não pode ser vzio");
            DomainValidation.Validate(datanascimento >= DateTime.Today, "Data nascimento Inválida");
            DomainValidation.Validate(datanascimento <= DateTime.MinValue, "Data nascimento Inválida");

            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            DataNascimento = datanascimento;
            Preferencial = preferencial;            

        }

    }
}
