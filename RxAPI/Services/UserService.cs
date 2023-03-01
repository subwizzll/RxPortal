using RxAPI.Interfaces.Services;
using RxAPI.Models;
using RxAPI.Models.DTO;
using RxAPI.Types;

namespace RxAPI.Services;

public class UserService : IUserService
{
    readonly UserModel[] _users = new UserModel[]
    {
        new (){ Email = "Admin@foo.com", Password = "P@ssw0rd!", Trusted = true, Role = UserRole.Admin},
        new (){ Email = "JohnDoe@foo.com", Password = "P@ssw0rd!", Trusted = true, Role = UserRole.Standard},
    };
    
    public UserModel GetUser(UserDTO userDto)
    {
        UserModel user = _users.FirstOrDefault(o =>
            o.Email.Equals(userDto.Email, StringComparison.OrdinalIgnoreCase) &&
            o.Password.Equals(userDto.Password));
        
        return user;
    }
}