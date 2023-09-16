using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class EpisodeDto : BaseDto
    {

        public Episodes GetById(int id)
        {
            return Context.Episodes.FirstOrDefault(x => x.Id == id);
        }

        public Episodes GetById(int animeId, int serverId)
        {
            return Context.Episodes.FirstOrDefault(x => x.AnimeId == animeId && x.ServerId == serverId);
        }
        public Episodes GetById(int animeId, int serverId, int order)
        {
            return Context.Episodes.FirstOrDefault(x => x.AnimeId == animeId && x.ServerId == serverId && x.Order == order);
        }

        public IEnumerable<Episodes> GetAll(int animeId)
        {
            return Context.Episodes.Where(x => x.AnimeId == animeId).OrderBy(x => x.Order);
        }
        public IEnumerable<Episodes> GetAll(int animeId, int serverId)
        {
            return Context.Episodes.Where(x => x.AnimeId == animeId && x.ServerId == serverId).OrderBy(x => x.Order);
        }
        public int GetMaxOrderId(int animeId, int serverId)
        {
            var maxOrder = Context.Episodes
                .Where(x => x.AnimeId == animeId && x.ServerId == serverId)
                .Select(x => (int?)x.Order)
                .Max();

            return maxOrder ?? 0;
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
                var updateEntity = GetById(entity.AnimeId, entity.ServerId);
                if (updateEntity == null) { return false; }
                updateEntity.Order = entity.Order;
                updateEntity.Title = entity.Title;
                updateEntity.Url = entity.Url;
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
                Context.Episodes.Remove(deleteEntity);
                Context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
