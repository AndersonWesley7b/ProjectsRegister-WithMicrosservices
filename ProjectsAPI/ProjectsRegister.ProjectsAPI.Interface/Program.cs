using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ProjectsAPI.Infraestructure.Context;
using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories;
using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;
using ProjectsRegister.ProjectsAPI.Services.ApplicationServices;
using ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;
using System.Globalization;

namespace ProjectsRegister.ProjectsAPI.Interface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connection = builder.Configuration["SqlServerConnection:SqlServerConnectionString"];

            #region Repository Injection

            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

            #endregion

            #region ServiceInjection

            builder.Services.AddScoped<IProjectsApplicationServices, ProjectsApplicationServices>();

            #endregion

            builder.Services.AddDbContext<SqlServerContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("ProjectsRegister.ProjectsAPI.Infrastructure")));

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
}
