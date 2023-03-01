using RxAPI.Types;

namespace RxAPI.Models;

public class UserModel
{
   public string Email { get; set; }
   public string Password { get; set; }
   public bool Trusted { get; set; }
   public UserRole Role { get; set; }
}