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
    public class EpisodeRepositoryDapper : IEpisodeRepository
    {
        private readonly IDbConnection _connection;

        public EpisodeRepositoryDapper(IDbConnection connection)
        {
            _connection = connection;
        }

        public Task<IEnumerable<Episodes>> GetAll()
        {
            throw new NotImplementedException();

        }

        public async Task<Episodes> GetById(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<Episodes>("usp_Get_Episode_By_Id", new { id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Create(Episodes entity)
        {
            int id = 0;
            var paramList = new DynamicParameters();
            paramList.Add("@SortOrder", entity.SortOrder);
            paramList.Add("@AnimeId", entity.AnimeId);
            paramList.Add("@ServerId", entity.ServerId);
            paramList.Add("@Url", entity.Url);
            paramList.Add("@Title", entity.Title);
            paramList.Add("@CreatedBy", entity.CreatedBy);
            paramList.Add("@Id",id, direction: ParameterDirection.Output);
            var result = await _connection.ExecuteAsync("usp_Create_Episode", paramList,
                commandType: CommandType.StoredProcedure);
            if (result > 0)
            {
                id = paramList.Get<int>("@Id");
                return id > 0;
            }

            return false;
        }

        public async Task<bool> Update(Episodes entity)
        {
            var paramList = new DynamicParameters();

            paramList.Add("@SortOrder", entity.SortOrder);
            paramList.Add("@Url", entity.Url);
            paramList.Add("@Title", entity.Title);
            paramList.Add("@ModifiedBy", entity.ModifiedBy);
            paramList.Add("@Id", entity.Id);

            var result = await _connection.ExecuteAsync("usp_Update_Episode", paramList,
                commandType: CommandType.StoredProcedure);
            return result > 0;
        }

        public async Task<bool> Delete(int id, int deletedBy = default)
        {
            var paramList = new DynamicParameters();
            paramList.Add("@DeletedBy", deletedBy);
            paramList.Add("@Id", id);

            var result = await _connection.ExecuteAsync("usp_Delete_Episode", paramList,
                commandType: CommandType.StoredProcedure);
            return result > 0;
        }

        public async Task<IEnumerable<Episodes>> GetAll(int animeId, int serverId)
        {
            return await _connection.QueryAsync<Episodes>("usp_Get_All_Episodes_By_AnimeId_And_ServerId",
                new { animeId, serverId }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetMaxOrderId(int animeId, int serverId)
        {
            return await _connection.QuerySingleAsync<int?>("usp_Get_Max_Episode_Order",
                new { animeId, serverId }, commandType: CommandType.StoredProcedure) ?? 0;
        }

        public Task<bool> AddRange(List<Episodes> episodes)
        {
            throw new NotImplementedException();
        }
    }
}
