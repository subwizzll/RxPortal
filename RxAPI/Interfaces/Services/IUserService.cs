using RxAPI.Models;
using RxAPI.Models.DTO;

namespace RxAPI.Interfaces.Services;

public interface IUserService
{
    UserModel GetUser(UserDTO userDto);
}