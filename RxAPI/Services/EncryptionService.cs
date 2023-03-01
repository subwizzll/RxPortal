using System.Web;
using Effortless.Net.Encryption;
using RxAPI.Interfaces.Services;

namespace RxAPI.Services;

public class EncryptionService : IEncryptionService
{
    readonly byte[]  _key;
    readonly byte[]  _iv;

    public EncryptionService()
    {
        _key = Bytes.GenerateKey();
        _iv = Bytes.GenerateIV();
    }

    public string Decrypt(string value)
    {
        var cipherString = HttpUtility.UrlDecode(value);
        return Strings.Decrypt(cipherString, _key, _iv);
    }

    public string Encrypt(string value) 
        => HttpUtility.UrlEncode(Strings.Encrypt(value, _key, _iv));
}