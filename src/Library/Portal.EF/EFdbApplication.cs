using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Portal.Domain.Movies;
using Portal.Domain.Users;

namespace Portal.EF;
public class EFdbApplication : DbContext
{
    public string _ConnectionString { get; set; } = "data source =.; initial catalog =movies; integrated security = True; MultipleActiveResultSets=True";
    public bool UseSqlServer { get; set; }
    public EFdbApplication()
    {

    }

    private EFdbApplication(DbContextOptions<EFdbApplication> options) : this((DbContextOptions)options)
    {


    }

    protected EFdbApplication(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFdbApplication).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (UseSqlServer)
            optionsBuilder.UseSqlServer(_ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {

        configurationBuilder.Properties<string>().HaveMaxLength(250).AreUnicode(false);
        base.ConfigureConventions(configurationBuilder);
    }

    public override ChangeTracker ChangeTracker
    {
        get
        {
            var tracker = base.ChangeTracker;
            tracker.LazyLoadingEnabled = false;
            tracker.AutoDetectChangesEnabled = true;
            tracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            return tracker;
        }
    }


    #region Users

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleUser> UserRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserLoginHistory> UserLoginHistories { get; set; }

    #endregion


    #region Movies
    public DbSet<Film> Films { get; set; }
    #endregion








}
