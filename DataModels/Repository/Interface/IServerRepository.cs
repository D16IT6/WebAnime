using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;

namespace DataModels.Repository.Interface
{
    public interface IServerRepository : IRepositoryBase<Servers, int>
    {
        Task<Servers> GetFirst();
    }
}
