using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using ViewModels.Admin;

namespace DataModels.Repository.Interface
{
    public interface IStudioRepository : IRepositoryBase<Studios,int>
    {

        Task<Paging<Studios>> GetPaging (string searchName,int pageSize,int pageNumber);
    }
}
