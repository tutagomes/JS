using System;
using System.Security.Cryptography;

namespace AuthTeste
{
    class Program
    {
        public string Key { get; }

        static void Main(string[] args)
        {

            var provider = new RSACryptoServiceProvider(2048);
            Console.WriteLine(provider.ExportParameters(true));
            /*
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }
            Console.WriteLine(Key);
            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
            Console.WriteLine("Hello World!"); */
        }
    }
}
