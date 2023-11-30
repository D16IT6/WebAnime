using System.Threading.Tasks;
using DataModels.EF;
using ViewModels.Admin;
using BlogViewModel = ViewModels.Client.BlogViewModel;

namespace DataModels.Repository.Interface
{
    public interface IBlogRepository : IRepositoryBase<Blogs, int>
    {
        Task<BlogViewModel> GetBlogViewModel(int blogId);
        Task<Paging<Blogs>> GetPaging(string searchTitle,int pageSize,int pageNumber);
    }
}
