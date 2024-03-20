using Microsoft.EntityFrameworkCore;
using ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;
using ProjectsRegister.UsersAPI.Infrastructure.Repositories;
using ProjectsRegister.UsersAPI.Infrastructure.Repositories.IRepositories;
using ProjectsRegister.UsersAPI.Services.ApplicationServices;
using ProjectsRegister.UsersAPI.Services.ApplicationServices.IApplicationServices;
using ProjectsRegister.UsersAPI.Services.ConnectedServices;
using ProjectsRegister.UsersAPI.Services.ConnectedServices.IConnectedServices;
using UsersAPI.Infraestructure.Context;
using UsersRegister.UsersAPI.Infrastructure.UnitOfWork;

namespace ProjectsRegister.UsersAPI.Interface;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

         builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        var connection = builder.Configuration["SqlServerConnection:SqlServerConnectionString"];

        builder.Services.AddDbContext<SqlServerContext>(options =>
            options.UseSqlServer(connection, b => b.MigrationsAssembly("ProjectsRegister.UsersAPI.Infrastructure")));

        #region UnitOfWorkInjection
        builder.Services.AddTransient<IUsersUnitOfWork, UsersUnitOfWork>();
        #endregion

        #region Repository Injection

        builder.Services.AddScoped<IUsersRepository, UsersRepository>();

        #endregion

        #region ServiceInjection

        builder.Services.AddScoped<IUsersApplicationServices, UsersApplicationServices>();

        #endregion

        #region MicrosservicesInjection

        builder.Services.AddHttpClient<IProjectsConnectedServices, ProjectsConnectedServices>(c =>
        {
            var baseAddress = builder.Configuration["ServiceUrls:ProjectsAPI"];
            c.BaseAddress = baseAddress != null ? new Uri(baseAddress) : null;
        });

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

        app.UseCors("MyPolicy");
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
