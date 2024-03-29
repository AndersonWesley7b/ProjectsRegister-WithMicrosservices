using Microsoft.EntityFrameworkCore;
using ProjectsAPI.Infraestructure.Context;
using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories;
using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;
using ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork;
using ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;
using ProjectsRegister.ProjectsAPI.Services.ApplicationServices;
using ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;
using ProjectsRegister.ProjectsAPI.Services.ConnectedServices;
using ProjectsRegister.ProjectsAPI.Services.ConnectedServices.IConnectedServices;

namespace ProjectsRegister.ProjectsAPI.Interface
{
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
                options.UseSqlServer(connection, b => b.MigrationsAssembly("ProjectsRegister.ProjectsAPI.Infrastructure")));

            #region
            builder.Services.AddTransient<IProjectsUnitOfWork, ProjectsUnitOfWork>();
            #endregion

            #region Repository Injection

            builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();

            #endregion

            #region ServiceInjection

            builder.Services.AddScoped<IProjectsApplicationServices, ProjectsApplicationServices>();

            #endregion

            #region MicrosservicesInjection

            builder.Services.AddHttpClient<IUsersConnectedServices, UsersConnectedServices>(c =>
            {
                var baseAddress = builder.Configuration["ServiceUrls:UsersAPI"];
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
}
