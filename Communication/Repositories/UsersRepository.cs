using Communication.Interfaces;
using Communication.Models;
using DapperCore;

namespace Communication.Repositories
{
    public class UsersRepository : IUsersInterface
    {
        private readonly IDapperCoreService _dapper;
        public UsersRepository(IDapperCoreService dapper)
        {
            _dapper = dapper;
        }
        public Task<IEnumerable<Users>> GetAllUsers()
        {
            try
            {
                string query = $"SELECT *FROM Users ";
                var data = _dapper.GetAllAsync<Users>(query, null);
                return data;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
