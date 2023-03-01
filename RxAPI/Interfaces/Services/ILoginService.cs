using RxAPI.Models;

namespace RxAPI.Interfaces.Services;

public interface ILoginService
{
    string Login(UserModel userModel);
}