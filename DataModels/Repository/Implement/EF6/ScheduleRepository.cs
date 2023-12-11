using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;
using static Dapper.SqlMapper;

namespace DataModels.Repository.Implement.EF6
{
    public class ScheduleRepository:IScheduleRepository
    {
        public WebAnimeDbContext Context = new WebAnimeDbContext();

        public ScheduleRepository(WebAnimeDbContext _context)
        {
            Context=_context;
        }
        public async Task<IEnumerable<Schedules>> GetAll()
        {
            return await Context.Schedules.Where(s => !s.IsDeleted).ToListAsync();
        }

        public async Task<Schedules> GetById(int id)
        {
            return await Context.Schedules.FirstOrDefaultAsync(s => !s.IsDeleted && s.Id == id);
        }

        public async Task<bool> Create(Schedules entity)
        {
            try
            {
                var prevEntity = Context.Schedules.FirstOrDefault(s => s.Id == entity.Id);
                if (prevEntity == null)
                {
                    entity.ModifiedDate = entity.CreatedDate = DateTime.Now;
                    Context.Schedules.Add(entity);
                    await Context.SaveChangesAsync();
                    return true;
                }

                prevEntity.ModifiedDate = DateTime.Now;
                prevEntity.AiringDate = entity.AiringDate;
                prevEntity.AiringTime = entity.AiringTime;
                prevEntity.ModifiedBy = entity.ModifiedBy;
                prevEntity.IsDeleted = false;

                await Context.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Schedules entity)
        {
            try
            {
                var updateEtity = await GetById(entity.Id);
                if (updateEtity == null) {return false;}
                updateEtity.AiringDate = entity.AiringDate;
                updateEtity.AiringTime = entity.AiringTime;
                updateEtity.ModifiedDate = DateTime.Now;
                await Context.SaveChangesAsync();
                return true;
        
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id, int deletedBy = default)
        {
            try
            {
                var deleteEtity = await GetById(id);
                if (deleteEtity == null) { return false; }

                deleteEtity.IsDeleted = true;
                deleteEtity.DeletedDate = DateTime.Now;
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
