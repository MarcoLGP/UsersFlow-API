namespace UsersFlow_API.Services
{
    public interface ICryptoService
    {
        public string encrypt(string plaintext);
        public string decrypt(string encryptedText);
    }
}
