namespace RxAPI.Interfaces.Services;

public interface IEncryptionService
{
    string Decrypt(string value);
    string Encrypt(string value);
}