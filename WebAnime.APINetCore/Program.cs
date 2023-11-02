
using DataModels.APINetCore.Repository.Implement;
using DataModels.APINetCore.Repository.Interface;

namespace WebAnime.APINetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper
                (typeof(AutoMapperProfile).Assembly);
            AddRepository<IUserRepository, UserRepositoryDapper>(builder);
            AddRepository<IAnimeRepository, AnimeRepositoryDapper>(builder);


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static void AddRepository<TTRepository, TImplementation>(WebApplicationBuilder builder)
            where TTRepository : class
            where TImplementation : TTRepository
        {
            var connectionString = builder.Configuration.GetConnectionString("WebAnimeDbContext");
            builder.Services.AddTransient<TTRepository>(provider =>
            {
                if (connectionString != null)
                    return (ActivatorUtilities.CreateInstance
                    (
                        provider,
                        typeof(TImplementation),
                        new object[] { connectionString }
                    ) as TTRepository)!;
                return null!;
            });

        }

    }
}