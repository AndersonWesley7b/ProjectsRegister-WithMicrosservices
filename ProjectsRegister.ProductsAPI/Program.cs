
using Microsoft.EntityFrameworkCore;
using ProjectsAPI.Infraestructure.Context;

namespace ProjectsRegister.ProductsAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connection = builder.Configuration["SqlServerConnection:SqlServerConnectionString"];


        builder.Services.AddDbContext<SqlServerContext>(options => 
        options.UseSqlServer(connection,
            b => b.MigrationsAssembly("ProjectsRegister.ProjectsAPI.Infrastructure")));

        builder.Services.AddControllers();
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
}
