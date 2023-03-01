namespace RxAPI.Models.DTO;

public record UserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}