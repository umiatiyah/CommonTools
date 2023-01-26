using Communication.Models;

namespace Communication.Interfaces
{
    public interface IUsersInterface
    {
        Task<IEnumerable<Users>> GetAllUsers();
    }
}
