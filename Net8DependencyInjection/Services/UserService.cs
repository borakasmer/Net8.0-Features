using Microsoft.Extensions.Diagnostics.HealthChecks;
using Net8DependencyInjection.Models;

namespace Net8DependencyInjection.Services
{
    public class UserService : IUserService
    {
        static List<User> UserList;
        public UserService() {
            if(UserList == null)
            {
                UserList = new List<User>();
                UserList.Add(new User() { Id = 1, Name = "Bora",Email="bora@borakasmer.com" });
                UserList.Add(new User() { Id = 2, Name = "Emre", Email = "emre78@gmail.com" });
                UserList.Add(new User() { Id = 3, Name = "Mehmet", Email = "mehmet@yahoo.com" });
                UserList.Add(new User() { Id = 4, Name = "Ahmet", Email = "ahmet85@gmail.com" });
            }
        }
        public User GetUserByID(int Id)
        {
            return UserList.Where(x => x.Id == Id).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return UserList;
        }
    }
}
