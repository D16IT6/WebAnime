using DataModels.EF;

namespace DataModels.Helpers
{
    public abstract class BaseDto
    {
        protected readonly WebAnimeDbContext Context = new WebAnimeDbContext();

    }
}
