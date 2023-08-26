using Models;

namespace Interfaces
{
    public interface IAuthRepository
    {
        public User CheckUserExist(string email);
        public User AddUser(User user);
        public void Update(User user);
       
    }
}
