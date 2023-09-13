using DataModels.EF;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Helpers
{
    public static class LoadFirstData
    {
        public static void LoadStudios(this WebAnimeDbContext context)
        {
            if (!context.Studios.Any())
            {
                var listStudios = new List<Studios>
                {
                    new Studios { Name = "Studio Bind" },
                    new Studios { Name = "Mappa" },
                    new Studios { Name = "Studio Jemi" }
                };
                context.Studios.AddRange(listStudios);
                context.SaveChanges();
            }
        }

        public static void LoadTypes(this WebAnimeDbContext context)
        {
            if (!context.Types.Any())
            {
                var listTypes = new List<Types>
                {
                    new Types { Name = "TV Series" },
                    new Types { Name = "Movies" },
                    new Types { Name = "Bluray" },
                    new Types { Name = "OVA" }
                };
                context.Types.AddRange(listTypes);
                context.SaveChanges();
            }
        }

        public static void LoadStatuses(this WebAnimeDbContext context)
        {
            if (!context.Statuses.Any())
            {
                var listStatuses = new List<Statuses>
                {
                    new Statuses
                    {
                        Name = "Đã hoàn thành"
                    },
                    new Statuses
                    {
                        Name = "Chưa hoàn thành"
                    },
                    new Statuses
                    {
                        Name = "Chưa phát sóng"
                    }
                };
                context.Statuses.AddRange(listStatuses);
                context.SaveChanges();
            }
        }

        public static void LoadAgeRatings(this WebAnimeDbContext context)
        {
            if (!context.AgeRatings.Any())
            {
                var listAgeRatings = new List<AgeRatings>
                {
                    new AgeRatings { Name = "Mọi lứa tuổi" },
                    new AgeRatings { Name = "Trẻ em (dưới 13 tuổi)" },
                    new AgeRatings { Name = "13+" },
                    new AgeRatings { Name = "18+" }
                };
                context.AgeRatings.AddRange(listAgeRatings);
                context.SaveChanges();
            }
        }

        public static void LoadCountries(this WebAnimeDbContext context)
        {
            if (!context.Countries.Any())
            {
                var listCountries = new List<Countries>
                {
                    new Countries
                    {
                        Name = "Nhật Bản"
                    },
                    new Countries
                    {
                        Name = "Trung Quốc"
                    },
                    new Countries
                    {
                        Name = "Hàn Quốc"
                    },
                    new Countries
                    {
                        Name = "Quốc gia khác"
                    }
                };
                context.Countries.AddRange(listCountries);
                context.SaveChanges();
            }
        }

        public static void LoadCategories(this WebAnimeDbContext context)
        {
            if (!context.Categories.Any())
            {
                var listCategoryName = new string[]
                {
                    "Action", "Adventure", "Cartoon", "Comedy", "Dementia", "Demons", "Drama", "Ecchi", "Fantasy",
                    "Game", "Harem", "Historical", "Horror", "Josei", "Kids", "Live Action", "Magic", "Martial Arts",
                    "Mecha", "Military", "Music", "Mystery", "Parody", "Police", "Psychological", "Romance", "Samurai",
                    "School", "Sci-Fi", "Seinen", "Shoujo", "Shoujo Ai", "Shounen", "Shounen Ai", "Slice of Life",
                    "Space", "Sports", "Super Power", "Supernatural", "Thriller", "Tokusatsu", "Vampire", "Yaoi", "Yuri"

                };
                int n = listCategoryName.Length;
                for (int i = 0; i < n; i++)
                {
                    context.Categories.Add(new Categories()
                    {
                        Name = listCategoryName[i]
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
