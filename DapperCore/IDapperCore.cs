using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperCore
{
    public interface IDapperCoreService
    {
        Task<T> GetByIdAsync<T>(string query, DynamicParameters? parameters);
        Task<IEnumerable<T>> GetAllAsync<T>(string query, DynamicParameters? parameters);
        Task<T> InsertAsync<T>(string query, DynamicParameters? parameters);
        Task<T> UpdateAsync<T>(string query, DynamicParameters? parameters);
        Task<int> DeleteAsync(string query, DynamicParameters? parameters);
    }
}
