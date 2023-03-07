using lagaltApp;
using Microsoft.EntityFrameworkCore;

namespace lagalt
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<UserModel> Users { get; set; }
    // public DbSet<SearchWordModel> searchWords { get; set; }
    //  public DbSet<SkillModel> skills { get; set; }
     public DbSet<ProjectModel> Projects { get; set; }


  }
}