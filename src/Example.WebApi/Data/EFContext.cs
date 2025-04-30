using Example.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Data;

public class EFContext : DbContext
{
    public DbSet<UserEntity> Users => Set<UserEntity>();

    public EFContext()
    {
    }

    public EFContext(DbContextOptions options) : base(options)
    {
    }
}