using Microsoft.EntityFrameworkCore;
using ProjectsRegister.ProjectsAPI.Domain.Entities;

namespace ProjectsAPI.Infraestructure.Context;
public sealed class SqlServerContext : DbContext
{
    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
    { }

    public DbSet<Project> Projects { get; set; }

}
