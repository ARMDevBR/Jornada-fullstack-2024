using Finan.Api.Data.Mappings;
using Finan.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Finan.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) // ctor primário do novo dotnet e c#
{
    /*public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }*/

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Todas que implementam "IEntityTypeConfiguration"
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Uma a uma
        //modelBuilder.ApplyConfiguration(new CategoryMapping());
        //modelBuilder.ApplyConfiguration(new TransactionMapping());
    }
}