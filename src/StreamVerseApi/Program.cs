using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StreamVerse.Infraestructure;
using StreamVerse.Infraestructure.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.

        builder.Services.AddDbContext<DataContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
      b => b.MigrationsAssembly("StreamVerseApi")));



        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<GenreRepository>();
        builder.Services.AddScoped<MovieRepository>();
        builder.Services.AddScoped<SerieRepository>();
        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<UnitOfWork>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();
    }
}