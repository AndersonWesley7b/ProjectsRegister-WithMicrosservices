using Microsoft.EntityFrameworkCore;
using ProjectsRegister.UsersAPI.Infrastructure.Repositories;
using ProjectsRegister.UsersAPI.Infrastructure.Repositories.IRepositories;
using ProjectsRegister.UsersAPI.Services.ApplicationServices;
using ProjectsRegister.UsersAPI.Services.ApplicationServices.IApplicationServices;
using UsersAPI.Infraestructure.Context;

namespace ProjectsRegister.UsersAPI.Interface;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connection = builder.Configuration["SqlServerConnection:SqlServerConnectionString"];

        builder.Services.AddDbContext<SqlServerContext>(options =>
            options.UseSqlServer(connection, b => b.MigrationsAssembly("ProjectsRegister.UsersAPI.Infrastructure")));

        #region Repository Injection

        builder.Services.AddScoped<IUsersRepository, UsersRepository>();

        #endregion

        #region ServiceInjection

        builder.Services.AddScoped<IUsersApplicationServices, UsersApplicationServices>();

        #endregion

        #region MicrosservicesInjection
        #endregion

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

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
