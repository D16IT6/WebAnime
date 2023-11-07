using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.Dapper
{
    public class StatusRepositoryDapper : IStatusRepository
    {
        private readonly IDbConnection _connection;

        public StatusRepositoryDapper(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Statuses>> GetAll()
        {
            return await _connection.QueryAsync<Statuses>("Select* from dbo.Statuses S where S.IsDeleted = 0 ");
        }

        public Task<Statuses> GetById(int id)
        {
            throw new NotImplementedException();

        }

        public Task<bool> Create(Statuses entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Statuses entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }
    }
}
