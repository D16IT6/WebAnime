
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.EF6
{
    public class ServerRepository : IServerRepository
    {
        public WebAnimeDbContext Context { get; set; }
        public ServerRepository(WebAnimeDbContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<Servers>> GetAll()
        {
            return await Context.Servers
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async  Task<Servers> GetById(int id)
        {
            return await Context.Servers
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        public async Task<bool> Create(Servers entity)
        {
            try
            {
                entity.ModifiedDate = entity.CreatedDate = DateTime.Now;
                Context.Servers.Add(entity);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Servers entity)
        {
            var updateEntity = await GetById(entity.Id);
            if (updateEntity == null) return false;
            updateEntity.Name = entity.Name;
            updateEntity.Description = entity.Description;
            updateEntity.ModifiedDate = DateTime.Now;
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id, int deletedBy = default)
        {
            try
            {
                var deleteEntity = await GetById(id);
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

        public async Task<Servers> GetFirst()
        {
            return await Context.Servers.FirstOrDefaultAsync();

        }
    }
}
