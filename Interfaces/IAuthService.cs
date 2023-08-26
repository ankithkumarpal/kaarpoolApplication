using Models;
using ViewLayer;
namespace Interfaces
{
    public interface IAuthService
    {
        public bool SignUp(UserDTO user);
        public string Login(LoginRequestDTO requestDetails);
        public Task<string> GoogleLogin(string token);
    }
}
