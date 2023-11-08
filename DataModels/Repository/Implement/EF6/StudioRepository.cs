﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;

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
    }
}