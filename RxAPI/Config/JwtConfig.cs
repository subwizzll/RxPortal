namespace RxAPI.Config;

public record JwtConfig
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string Key { get; set; }
}