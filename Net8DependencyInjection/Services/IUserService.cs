using Net8DependencyInjection.Models;

namespace Net8DependencyInjection.Services
{
    public interface IUserService
    {
        public List<User> GetUsers();
        public User GetUserByID(int Id);
    }
}
