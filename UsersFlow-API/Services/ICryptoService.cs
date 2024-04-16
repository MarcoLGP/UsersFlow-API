namespace UsersFlow_API.Services
{
    public interface ICryptoService
    {
        public string decrypt(string encryptedText, string key);
    }
}
