using System.Security.Cryptography;
using System.Text;

namespace UsersFlow_API.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly IConfiguration _configuration;
        public CryptoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string decrypt(string encryptedText)
        {
            string key = _configuration.GetSection("crypto").GetValue<string>("key") ?? throw new Exception("Invalid key");
            // Converter a chave para bytes
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            // Converter a string criptografada para bytes
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            // Configurar o objeto AES para descriptografar
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.Mode = CipherMode.ECB; // O modo de operação precisa ser o mesmo que foi usado para criptografar (por padrão é CBC no CryptoJS)
                aes.Padding = PaddingMode.PKCS7; // O preenchimento precisa ser o mesmo que foi usado para criptografar (por padrão é PKCS7 no CryptoJS)

                // Criar um decifrador
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // Criar um fluxo de memória para descriptografar os dados
                using (var ms = new System.IO.MemoryStream(encryptedBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Ler os bytes descriptografados do fluxo de criptografia
                        byte[] decryptedBytes = new byte[encryptedBytes.Length];
                        int decryptedByteCount = cs.Read(decryptedBytes, 0, decryptedBytes.Length);

                        // Converter os bytes descriptografados em uma string UTF-8
                        return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedByteCount);
                    }
                }
            }
        }

        public string encrypt(string plaintext)
        {
            string key = _configuration.GetSection("crypto").GetValue<string>("key") ?? throw new Exception("Invalid key");
            // Converter a chave para bytes
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            // Converter a string para bytes UTF-8
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            // Configurar o objeto AES para criptografar
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.Mode = CipherMode.ECB; // Mesmo modo usado na descriptografia
                aes.Padding = PaddingMode.PKCS7; // Mesmo preenchimento usado na descriptografia

                // Criar um cifrador
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // Criar um fluxo de memória para armazenar os dados criptografados
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Escrever os bytes criptografados no fluxo de criptografia
                        cs.Write(plaintextBytes, 0, plaintextBytes.Length);
                    }

                    // Obter os bytes criptografados do fluxo de memória e convertê-los para uma string base64
                    byte[] encryptedBytes = ms.ToArray();
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }
    }
}
