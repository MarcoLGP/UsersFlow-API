using System.Security.Cryptography;
using System.Text;

namespace UsersFlow_API.Services
{
    public class CryptoService : ICryptoService
    {
        public string decrypt(string encryptedText, string key)
        {
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
    }
}
