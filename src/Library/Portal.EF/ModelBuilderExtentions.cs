using Microsoft.EntityFrameworkCore;
using Portal.Domain.Users;

namespace Portal.EF;

public static class ModelBuilderExtentions
{
    public static void Indexes(this ModelBuilder modelBuilder)
    {
        #region Users
        //modelBuilder.Entity<Role>().HasIndex(m => new { m.RoleId, m.RoleName });
        #endregion

    }
    //-----------------------------------------------
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        


    }
    //-----------------------------------------------
    public static void QueryFilters(this ModelBuilder modelBuilder)
    {
        
    }
}
