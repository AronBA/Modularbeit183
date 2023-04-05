using Microsoft.EntityFrameworkCore;
using Modularbeit183.Models;

namespace Modularbeit183.Data;


public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
    
    public DbSet<UserModel> Users { get; set; }
}