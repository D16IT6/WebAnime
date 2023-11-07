using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataModels.EF;
using DataModels.Repository.Interface;
using static Dapper.SqlMapper;

namespace DataModels.Repository.Implement.Dapper
{
    public class CountryRepositoryDapper : ICountryRepository
    {
        private readonly IDbConnection _connection;

        public CountryRepositoryDapper(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Countries>> GetAll()
        {
            return await _connection.QueryAsync<Countries>("Select * from dbo.Countries where IsDeleted = 0");
        }

        public async Task<Countries> GetById(int id)
        {
            return await _connection.QuerySingleAsync<Countries>(
                "Select * from dbo.Countries where IsDeleted = 0 and id =@id",
                new { id }
                );

        }

        public async Task<bool> Create(Countries entity)
        {
            var newId = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@Name", entity.Name, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@CreatedBy", entity.CreatedBy, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Id", newId, dbType: DbType.Int32, direction: ParameterDirection.Output);


            var result = await _connection.ExecuteAsync(
               "usp_Create_Country",
               parameters,
               commandType: CommandType.StoredProcedure
               );
            newId = parameters.Get<int>("@Id");
            if (result > 0)
            {
                return newId > 0;
            }

            return false;
        }

        public async Task<bool> Update(Countries entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", entity.Name.Trim(), DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@ModifiedBy", entity.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Id", entity.Id, dbType: DbType.Int32, ParameterDirection.Input);

            var result = await _connection.ExecuteAsync(
                "usp_Update_Country",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }

        public async Task<bool> Delete(int id, int deletedBy = default)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, dbType: DbType.Int32, ParameterDirection.Input);
            parameters.Add(@"DeletedBy", deletedBy, DbType.Int32, ParameterDirection.Input);
            var result = await _connection.ExecuteAsync(
                "usp_Delete_Country",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }
    }
}
