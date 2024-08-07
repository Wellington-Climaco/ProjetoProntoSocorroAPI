﻿using Domain.Enums;
using Domain.Validate;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public List<string> Notifications = new();
        public bool IsValid { get => Notifications.Count == 0; }

        public Paciente()
        {

        }

        public Paciente(string nome, string sobrenome, string documento, DateTime datanascimento, EPreferencial preferencial)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            DataNascimento = datanascimento;

            if (!Enum.IsDefined(typeof(EPreferencial), preferencial))
                Notifications.Add("situação preferencial inválida");


            Preferencial = preferencial;

            Validate();
        }

        public Paciente(string nome, string sobrenome, string documento, DateTime datanascimento, EPreferencial preferencial, EStatusPreferencial statusPreferencial)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            DataNascimento = datanascimento;
            Preferencial = preferencial;
            StatusPreferencial = statusPreferencial;

            if (!Enum.IsDefined(typeof(EStatusPreferencial), StatusPreferencial))
                Notifications.Add("situação preferencial inválido");

            if (!Enum.IsDefined(typeof(EPreferencial), Preferencial))
                Notifications.Add("situação preferencial inválida");

            Validate();
        }

        public Paciente(Guid id, string nome, string sobrenome, string documento, DateTime datanascimento, EPreferencial preferencial, EStatusPreferencial statusPreferencial)
        {
            if (!Enum.IsDefined(typeof(EStatusPreferencial), StatusPreferencial))
                Notifications.Add("Status preferencial inválido");

            if (!Enum.IsDefined(typeof(EPreferencial), Preferencial))
                Notifications.Add("situação preferencial inválida");

            Validate();
        }

        public void Update(string nome, string sobrenome, string documento, DateTime datanascimento, EPreferencial preferencial)
        {
            if (!string.IsNullOrEmpty(nome))
                Nome = nome;

            if (!string.IsNullOrEmpty(sobrenome))
                Sobrenome = sobrenome;

            if (!string.IsNullOrEmpty(documento))
                Documento = documento;

            if (datanascimento != DateTime.MinValue && datanascimento < DateTime.Today)
                DataNascimento = datanascimento;

            if (Enum.IsDefined(typeof(EPreferencial), Preferencial))
                Preferencial = preferencial;
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Nome))
                Notifications.Add("Nome não pode ser vazio");

            if (string.IsNullOrEmpty(Sobrenome))
                Notifications.Add("Sobrenome não pode ser vazio");

            if (string.IsNullOrEmpty(Documento))
                Notifications.Add("Documento não pode ser vazio");

            if (Nome.Length > 46)
                Notifications.Add("Máximo de 46 caracteres para o nome");

            if (Sobrenome.Length > 46)
                Notifications.Add("Máximo de 46 caracteres para o sobrenome");

            if (Documento.Length > 12)
                Notifications.Add("Documento inválido");

            if (Documento.Length < 9)
                Notifications.Add("Documento inválido");

            if (DataNascimento >= DateTime.Today)
                Notifications.Add("Data nascimento Inválida");

            if (DataNascimento <= DateTime.MinValue)
                Notifications.Add("Data nascimento Inválida");
        }

        public void Encaminhar(EStatusAtendimento area)
        {
            if (Enum.IsDefined(typeof(EStatusAtendimento), area))
            {
                StatusAtendimento = area;
            }
            else
            {
                Notifications.Add("Encaminhamento inválido");
            }
        }




    }
}
