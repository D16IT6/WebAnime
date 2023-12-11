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
        private int currentAnimeCount = 11;
        private WebAnimeDbContext context;

        [TestInitialize]
        public void Initialize()
        {
            context = new WebAnimeDbContext();
            _animeRepositoryEf = new AnimeRepository(context);
        }

        [TestMethod]
        public async Task Get_All_Anime()
        {
            var test = await _animeRepositoryEf.GetAll();

            Assert.IsNotNull(test);
            Assert.AreEqual(currentAnimeCount, test.Count());
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
            var anime = new Animes();

            anime.Title = "Test Title";
            anime.OriginalTitle = "Test Title";
            anime.Synopsis = "<p>Test</p>";
            anime.Poster = "/Uploads/images/108030.jpg";
            anime.Duration = 23;
            anime.Release = DateTime.Now;
            anime.Trailer = "https://youtu.be/Yt4N0UUEd90";
            anime.StatusId = 2;
            anime.AgeRatingId = 2;
            anime.CreatedDate = DateTime.Now;
            anime.CreatedBy = 1;
            anime.CategoriesId =  context.Categories.Where(x => x.Id < 3 && !x.IsDeleted).Select( x=> x.Id).ToArray();
            anime.StudiosId = new int[] { 1 };

            var result = await _animeRepositoryEf.Create(anime);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
