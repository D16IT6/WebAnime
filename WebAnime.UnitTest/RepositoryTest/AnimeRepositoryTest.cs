using System;
using System.Linq;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Implement.EF6;
using DataModels.Repository.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace WebAnime.UnitTest.RepositoryTest
{
    [TestClass]
    public class AnimeRepositoryTest
    {
        private IAnimeRepository _animeRepositoryEf;
        private const int CurrentAnimeCount = 11;
        private WebAnimeDbContext _context;

        [TestInitialize]
        public void Initialize()
        {
            _context = new WebAnimeDbContext();
            _animeRepositoryEf = new AnimeRepository(_context);
        }

        [TestMethod]
        public async Task Get_All_Anime()
        {
            var test = await _animeRepositoryEf.GetAll();

            Assert.IsNotNull(test);
            Assert.AreEqual(CurrentAnimeCount, test.Count());
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task Get_Anime_By_Id(int id)
        {
            var find = await _animeRepositoryEf.GetById(id);
            Assert.IsNotNull(find);
        }
        [TestMethod]
        public async Task Create_Anime()
        {
            var anime = new Animes
            {
                Title = "Test Title",
                OriginalTitle = "Test Title",
                Synopsis = "<p>Test</p>",
                Poster = "/Uploads/images/108030.jpg",
                Duration = 23,
                Release = DateTime.Now,
                Trailer = "https://youtu.be/Yt4N0UUEd90",
                StatusId = 2,
                AgeRatingId = 2,
                CreatedDate = DateTime.Now,
                CreatedBy = 1,
                CategoriesId = _context.Categories.Where(x => x.Id < 3 && !x.IsDeleted).Select( x=> x.Id).ToArray(),
                StudiosId = new[] { 1 }
            };

            var result = await _animeRepositoryEf.Create(anime);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
