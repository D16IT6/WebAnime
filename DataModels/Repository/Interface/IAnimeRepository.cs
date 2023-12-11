using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using ViewModels.Admin;
using ViewModels.Client;

namespace DataModels.Repository.Interface
{
    public interface IAnimeRepository : IRepositoryBase<Animes, int>
    {
        public Task<IEnumerable<AnimeItemViewModel>> GetAnimeTrending(int take = 9);
        public Task<IEnumerable<AnimeItemViewModel>> GetAnimeRecenly(int take = 9);
        public Task<AnimeDetailViewModel> GetAnimeDetail(int id);

        public Task<AnimeWatchingViewModel> GetAnimeWatching(int id);

        public Task<bool> IncreaseView(int id);

        public Task<Paging<Animes>> GetPaging(string searchTitle, int pageNumber,int pageSize);

        public Task<Paging<AnimeItemViewModel>> AdvanceSearch(AnimeSearchViewModel model);

        public Task<IEnumerable<Animes>> GetHotAnimesAPI(int take);
        public Task<Paging<Animes>> GetNewEpisodesReleaseAPI(int pageNumber, int pageSize);
    }
}
