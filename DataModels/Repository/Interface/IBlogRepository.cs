using System.Threading.Tasks;
using DataModels.EF;
using ViewModels.Client;

namespace DataModels.Repository.Interface
{
    public interface IBlogRepository : IRepositoryBase<Blogs, int>
    {
        Task<BlogViewModel> GetBlogViewModel(int blogId);

    }
}
