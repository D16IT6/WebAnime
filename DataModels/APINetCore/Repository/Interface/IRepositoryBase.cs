using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.APINetCore.Models;

namespace DataModels.APINetCore.Repository.Interface
{
    public interface IRepositoryBase<T, in TKey>
    {
        public string ConnectionString { get; set; }
        
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(TKey id);
        public Task<bool> Create(T entity);
        public Task<bool> Update(T entity);
        public Task<bool> Delete(TKey id);
        public Task<Paging<T>> Paging(int pageNumber, int pageSize = 10);
    }
}
