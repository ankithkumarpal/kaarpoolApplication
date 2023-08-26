using Interfaces;
using Models;

namespace DataLayer
{
    public class AuthRepository : IAuthRepository
    {
        private CarPoolContext _context {get;set;}
        private ILog _logger ; 
        public AuthRepository(CarPoolContext context , ILog logger ) { 
            _context = context;
            _logger = logger;
        }
        public   User CheckUserExist(string email)
        {
           return  _context.Users.FirstOrDefault(u => u.Email == email);
        }
        public User AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                return user;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Update(User user)
        {
            try
            {
                _context.Users.Update(user);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
