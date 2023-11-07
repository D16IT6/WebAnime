using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.Dapper
{
    public class AgeRatingRepositoryDapper : IAgeRatingRepository
    {
        private readonly IDbConnection _connection;

        public AgeRatingRepositoryDapper(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<AgeRatings>> GetAll()
        {
            return await _connection.QueryAsync<AgeRatings>("Select* from dbo.AgeRatings where IsDeleted = 0 ");
        }

        public Task<AgeRatings> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(AgeRatings entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(AgeRatings entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }
    }
}
