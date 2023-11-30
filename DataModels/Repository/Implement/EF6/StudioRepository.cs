using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;
using ViewModels.Admin;

namespace DataModels.Repository.Implement.EF6
{
    public class StudioRepository : IStudioRepository
    {
        public WebAnimeDbContext Context { get; set; }
        public StudioRepository(WebAnimeDbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Studios>> GetAll()
        {
            return await Context.Studios
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<Studios> GetById(int id)
        {
            return await Context.Studios
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        public async Task<bool> Create(Studios entity)
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

        public async Task<Paging<Studios>> GetPaging(string searchName, int pageSize, int pageNumber)
        {
            var searchResult = Context.Studios
                .Where(x =>
                    (!x.IsDeleted) && (x.Name.Contains(searchName) || (String.IsNullOrEmpty(searchName))));

            var searchShow = searchResult
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            var searchCount = await searchResult.CountAsync();
            var result = new Paging<Studios>()
            {
                Data = searchShow,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling(searchCount * 1.0 / pageSize)
            };
            return await Task.FromResult(result);
        }
    }
}
