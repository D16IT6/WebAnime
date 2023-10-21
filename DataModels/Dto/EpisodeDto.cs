using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels.Dto
{
    public class EpisodeDto : BaseDto
    {
        public async Task<Episodes> GetById(int id)
        {
            return await Context.Episodes
                            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<IEnumerable<Episodes>> GetAll(int animeId, int serverId)
        {
            return await Context.Episodes
                            .Where(x => x.AnimeId == animeId && x.ServerId == serverId && !x.IsDeleted)
                            .OrderBy(x => x.SortOrder)
                            .ToListAsync();
        }

        public async Task<int> GetMaxOrderId(int animeId, int serverId)
        {
            var maxOrder = await Context.Episodes
                .Where(x => x.AnimeId == animeId && x.ServerId == serverId && !x.IsDeleted)
                .Select(x => (int?)x.SortOrder)
                .MaxAsync();

            return maxOrder ?? 0;
        }

        public async Task<bool> Add(Episodes entity)
        {
            try
            {
                entity.Animes = await Context.Animes.FirstOrDefaultAsync(x => x.Id == entity.AnimeId && !x.IsDeleted);
                entity.Servers = await Context.Servers.FirstOrDefaultAsync(x => x.Id == entity.ServerId && !x.IsDeleted);

                entity.ModifiedDate = entity.CreatedDate = DateTime.Now;
                entity.IsDeleted = false;

                Context.Episodes.Add(entity);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Episodes entity)
        {
            try
            {
                var updateEntity = await GetById(entity.Id);
                if (updateEntity == null) return false;

                updateEntity.SortOrder = entity.SortOrder;
                updateEntity.Title = entity.Title;
                updateEntity.Url = entity.Url;

                updateEntity.ModifiedDate = DateTime.Now;

                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var deleteEntity = await GetById(id);
                if (deleteEntity == null) return false;

                deleteEntity.IsDeleted = true;
                deleteEntity.DeletedDate = DateTime.Now;

                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}