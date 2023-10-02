using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class EpisodeDto : BaseDto
    {

        public Episodes GetById(int id)
        {
            return Context.Episodes.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public Episodes GetById(int animeId, int serverId)
        {
            return Context.Episodes.FirstOrDefault(x => x.AnimeId == animeId && x.ServerId == serverId && !x.IsDeleted);
        }
        public Episodes GetById(int animeId, int serverId, int order)
        {
            return Context.Episodes.FirstOrDefault(x => x.AnimeId == animeId && x.ServerId == serverId && x.Order == order && !x.IsDeleted);
        }

        public IEnumerable<Episodes> GetAll(int animeId)
        {
            return Context.Episodes.Where(x => x.AnimeId == animeId && !x.IsDeleted).OrderBy(x => x.Order);
        }
        public IEnumerable<Episodes> GetAll(int animeId, int serverId)
        {
            return Context.Episodes.Where(x => x.AnimeId == animeId && x.ServerId == serverId && !x.IsDeleted).OrderBy(x => x.Order);
        }
        public int GetMaxOrderId(int animeId, int serverId)
        {
            var maxOrder = Context.Episodes
                .Where(x => x.AnimeId == animeId && x.ServerId == serverId && !x.IsDeleted)
                .Select(x => (int?)x.Order)
                .Max();

            return maxOrder ?? 0;
        }


        public bool Add(Episodes entity)
        {
            try
            {
                entity.Animes = Context.Animes.FirstOrDefault(x => x.Id == entity.AnimeId && !x.IsDeleted);
                entity.Servers = Context.Servers.FirstOrDefault(x => x.Id == entity.ServerId && !x.IsDeleted);

                entity.ModifiedDate = entity.CreatedDate = DateTime.Now;
                entity.IsDeleted = false;

                Context.Episodes.Add(entity);
                Context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Update(Episodes entity)
        {
            try
            {
                var updateEntity = GetById(entity.Id);
                if (updateEntity == null) { return false; }

                updateEntity.Order = entity.Order;
                updateEntity.Title = entity.Title;
                updateEntity.Url = entity.Url;

                updateEntity.ModifiedDate = DateTime.Now;


                Context.SaveChanges();
                return true;
            }
            catch { return false; }

        }

        public bool Delete(int id)
        {
            try
            {
                var deleteEntity = GetById(id);
                if (deleteEntity == null)
                {
                    return false;
                }

                deleteEntity.IsDeleted = true;
                deleteEntity.DeletedDate = DateTime.Now;

                Context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
