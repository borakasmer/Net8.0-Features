using Microsoft.AspNetCore.Mvc;
using Net8DependencyInjection.Models;
using Net8DependencyInjection.Services;

namespace Net8DependencyInjection
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
           
        }

        [HttpGet("GetUserList")]
        public List<User> GetUserList([FromKeyedServices("user")] IUserService user)
        {
            return user.GetUsers();
        }
        [HttpGet("GetUserByID")]
        public User GetUserByID([FromKeyedServices("user")] IUserService user,int Id)
        {
            return user.GetUserByID(Id);
        }
    }
}
