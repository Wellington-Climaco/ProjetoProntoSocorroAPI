using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObject
{
    public class Senha
    {
        protected Senha()
        {           
        }

        public Senha(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                text = "12345678abc";

            Hash = Hashing(text);           
        }

        public string Hash { get; private set; } = string.Empty;

        public bool Verificacao(string PlainTextPassword)
            => Verify(Hash, PlainTextPassword);

        public static implicit operator string(Senha senha)
        => senha.ToString();

        public static implicit operator Senha(string senha)
        => new Senha(senha);

        private static string Hashing(
        string password,
        short saltSize = 16,
        short keySize = 32,
        int iterations = 10000,
        char splitChar = '.')
        {
            if (string.IsNullOrEmpty(password))
                throw new Exception("Password should not be null or empty");

            password += "Cm20]N5?4pb9%R+k8[L";


            using var algorithm = new Rfc2898DeriveBytes(
                password,
                saltSize,
                iterations,
                HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{iterations}{splitChar}{salt}{splitChar}{key}";
        }

        private static bool Verify(
        string hash,
        string password,
        short keySize = 32,
        int iterations = 10000,
        char splitChar = '.')
        {
            password += "Cm20]N5?4pb9%R+k8[L";

            var parts = hash.Split(splitChar, 3);
            if (parts.Length != 3)
                return false;

            var hashIterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            if (hashIterations != iterations)
                return false;

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(keySize);

            return keyToCheck.SequenceEqual(key);
        }


    }
}
