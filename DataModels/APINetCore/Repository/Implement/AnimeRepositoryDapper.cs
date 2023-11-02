using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DataModels.APINetCore.Models;
using DataModels.APINetCore.Repository.Interface;
using DataModels.EF;

namespace DataModels.APINetCore.Repository.Implement
{
    public class AnimeRepositoryDapper : IAnimeRepository
    {
        public AnimeRepositoryDapper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
        public async Task<IEnumerable<Animes>> GetAll()
        {
            var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<Animes>("Select * from dbo.Animes");
        }

        public Task<Animes> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Animes entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Animes entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Paging<Animes>> Paging(int pageNumber, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public IDbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }
    }
}
