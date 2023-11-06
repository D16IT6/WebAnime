using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataModels.APINetCore.Models;
using DataModels.APINetCore.Repository.Interface;
using DataModels.EF.Identity;

namespace DataModels.APINetCore.Repository.Implement
{
    public class UserRepositoryDapper : IUserRepository
    {
        public UserRepositoryDapper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public async Task<IEnumerable<Users>> GetAll()
        {
            var sqlConnection = new SqlConnection(ConnectionString);
            return await sqlConnection.QueryAsync<Users>("Select * from users");
        }

        public async Task<Users> GetById(int id)
        {
            var sqlConnection = new SqlConnection(ConnectionString);
            return await sqlConnection.QuerySingleAsync<Users>("Select * from users where id = @id", new { id });
        }

        public Task<bool> Create(Users entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Users entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Paging<Users>> Paging(int pageNumber, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
    }
}
