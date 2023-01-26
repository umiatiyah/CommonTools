using Communication.Interfaces;
using Communication.Models;
using DapperCore;
using Microsoft.AspNetCore.Mvc;

namespace Communication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersInterface _usersInterface;

        public UsersController(ILogger<UsersController> logger, IUsersInterface usersInterface)
        {
            _logger = logger;
            _usersInterface = usersInterface;
        }

        [HttpGet]
        public async Task<IEnumerable<Users>> GetUsers()
        {
            try
            {
                var users = await _usersInterface.GetAllUsers();
                _logger.Log(LogLevel.Information, "Get All Users");
                return users;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message); 
                throw new Exception();
            }
        }
    }
}