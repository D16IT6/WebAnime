using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class EpisodeDto : BaseDto
    {
        public Episodes GetById(int animeId, int serverId, int order)
        {
            return Context.Episodes.FirstOrDefault(x => x.AnimeId == animeId && x.ServerId == serverId && x.Order == order);
        }

        public IEnumerable<Episodes> GetAll(int animeId)
        {
            return Context.Episodes.Where(x => x.AnimeId == animeId).OrderBy(x => x.Order);
        }
        public int? GetMaxOrderId(int animeId)
        {
            return Context.Episodes.Where(x => x.AnimeId == animeId).Max(x => x.Order);
        }

        public IQueryable<int> GetAllEPs(int animeId)
        {
            return Context.Episodes.Where(x => x.AnimeId == animeId).Select(x => x.Order);
        }

        public bool Add(Episodes entity)
        {
            try
            {
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
                var updateEntity = GetById(entity.AnimeId, entity.ServerId, entity.Order);
                if (updateEntity == null) { return false; }
                updateEntity.Order = entity.Order;
                updateEntity.Title = entity.Title;
                updateEntity.Url = updateEntity.Url;
                updateEntity.ServerId = entity.ServerId;
                Context.SaveChanges();
                return true;
            }
            catch { return false; }

        }

        public bool Delete(int animeId, int serverId, int order)
        {
            try
            {
                var deleteEntity = GetById(animeId, serverId, order);
                if (deleteEntity == null)
                {
                    return false;
                }
                Context.Episodes.Remove(deleteEntity);
                return true;
            }
            catch { return false; }
        }
    }
}
