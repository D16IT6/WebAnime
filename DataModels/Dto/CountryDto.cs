using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels.Dto
{
    public class CountryDto : BaseDto
    {
        public async Task<Countries> GetById(int id)
        {
            return await Context.Countries.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        public async Task<IEnumerable<Countries>> GetAll()
        {
            return await Context.Countries
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> Add(Countries entity)
        {
            try
            {
                entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
                entity.IsDeleted = false;

                Context.Countries.Add(entity);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Countries entity)
        {
            var updateEntity = await Context.Countries
                .FirstOrDefaultAsync(x => !x.IsDeleted && entity.Id == x.Id);
            if (updateEntity == null) return false;
            updateEntity.Name = entity.Name;
            entity.ModifiedDate = DateTime.Now;
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id, int deletedBy)
        {
            try
            {
                var deleteEntity = await Context.Countries
                    .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
                if (deleteEntity == null) return false;
                deleteEntity.IsDeleted = true;
                deleteEntity.DeletedDate = DateTime.Now;
                deleteEntity.DeletedBy = deletedBy;
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