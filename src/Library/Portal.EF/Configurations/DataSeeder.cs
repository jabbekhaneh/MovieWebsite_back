using Portal.Domain.Users;

namespace Portal.EF.Configurations;

public interface IDataSeeder
{
    Task Seed();
}
public class DataSeeder : IDataSeeder
{
    private readonly EFdbApplication _context;

    public DataSeeder(EFdbApplication context)
    {
        this._context = context;
    }

    public async Task Seed()
    {
        var permissions = new List<Permission>
            {
                new Permission
                {
                    Id =Guid.NewGuid(),
                    Name ="ManagerRoles",
                    Title = "مدیریت نقش ها",
                },
                new Permission
                {
                    Id =Guid.NewGuid(),
                    Name ="ManagerUsers",
                    Title = "مدیریت کاربران",
                },
                new Permission
                {
                    Id =Guid.NewGuid(),
                    Name ="ManagerBlogCategory",
                    Title = "مدیریت دسته بندی بلاگ",
                },
                new Permission
                {
                    Id =Guid.NewGuid(),
                    Name ="ManagerBlogs",
                    Title = "مدیریت بلاگ",
                },
            };

        
        if (!_context.Users.Any())
        {
            await _context.Permissions.AddRangeAsync(permissions);
            _context.SaveChanges();
            var adminRole = new Role
            {
                Id = Guid.NewGuid(),
                Title = "Admin",
            };
             _context.Roles.Add(adminRole);
            _context.SaveChanges();
            var rolePermissions = new List<RolePermission>();
            foreach (var permision in permissions)
            {
                rolePermissions.Add(new RolePermission
                {
                    Id = Guid.NewGuid(),
                    PermissionId = permision.Id,
                    RoleId = adminRole.Id,
                });
            };
            await _context.RolePermissions.AddRangeAsync(rolePermissions);
            var developerUser = new User
            {
                Id = Guid.NewGuid(),
                Firstname = "Seyed Hassan",
                Lastname = "Jabbekhaneh",
                Email = "jabbekhaneh@gmail.com",
                Mobile = "9107066676",
                IsActiveMobile = true,
                IsActiveEmail = true,
                City = "Shiraz",
                County = "Iran",
                Company = "JabbekhanehCompany",
                Geneder = GenederType.Female,
                Job = "Developer",
                Wallet = 0,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
            };
            await _context.Users.AddAsync(developerUser);
            await _context.UserRoles.AddAsync(new RoleUser
            {
                Id = Guid.NewGuid(),
                RoleId = adminRole.Id,
                UserId = developerUser.Id,
            });

            _context.SaveChanges();
        }
    }
}
