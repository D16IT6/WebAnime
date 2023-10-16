using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels.Dto
{
    public class StudioDto : BaseDto
    {
        public async Task<Studios> GetById(int id)
        {
            return await Context.Studios
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        public async Task<IEnumerable<Studios>> GetAll()
        {
            return await Context.Studios
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> Add(Studios entity)
        {
            try
            {
                entity.IsDeleted = false;
                entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
                Context.Studios.Add(entity);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Studios entity)
        {
            try
            {
                var updateEntity = await GetById(entity.Id);
                if (updateEntity == null) return false;
                updateEntity.Name = entity.Name;
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
                var updateEntity = await GetById(id);
                if (updateEntity == null) return false;
                updateEntity.IsDeleted = true;
                updateEntity.DeletedDate = DateTime.Now;
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