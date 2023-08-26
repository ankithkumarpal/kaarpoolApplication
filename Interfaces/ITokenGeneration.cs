using Models;
namespace Interfaces
{
    public interface ITokenGeneration
    {
        public string GenerateToken(User user);
    }
}
