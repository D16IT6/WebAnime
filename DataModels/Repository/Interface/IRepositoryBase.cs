using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DataModels.EF;

namespace DataModels.Repository.Interface
{
    public interface IRepositoryBase<T, in TKey> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(TKey id);
        public Task<bool> Create(T entity);
        public Task<bool> Update(T entity);
        public Task<bool> Delete(TKey id, int deletedBy = default);

    }
}
