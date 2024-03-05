using Microsoft.EntityFrameworkCore;
using ProjectsRegister.UsersAPI.Domain.Entities;

namespace UsersAPI.Infraestructure.Context;

public sealed class SqlServerContext : DbContext
{
    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }

}
