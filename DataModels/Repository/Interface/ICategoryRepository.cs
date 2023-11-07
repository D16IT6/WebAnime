using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;

namespace DataModels.Repository.Interface
{
    public interface ICategoryRepository : IRepositoryBase<Categories,int>
    {
    }
}
