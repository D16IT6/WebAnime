using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using ViewModels.Client;

namespace DataModels.Repository.Interface
{
    public interface IAnimeRepository : IRepositoryBase<Animes, int>
    {
        public Task<IEnumerable<AnimeItemViewModel>> GetAnimeTrending(int take = 9);
        public Task<IEnumerable<AnimeItemViewModel>> GetAnimeRecenly(int take = 9);
        public Task<AnimeDetailViewModel> GetAnimeDetail(int id);

    }
}
