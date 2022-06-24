using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portal.Application.Users.Commands.AddUser;
using Portal.Application.Users.Commands.EditUser;
using Portal.Application.Users.Contracts;
using Portal.Application.Users.Queries.GetUserById;
using Portal.Application.Users.Queries.GetUsers;
using Portal.EF.Users.Repositories;
using System.Reflection;
using MediatR.Pipeline;
using Portal.Application.Common;
using Portal.Application.Movies.Contracts;
using Portal.EF.Movies.Repositories;
using Portal.Application.Movies.Commands.AddFilm;
using Portal.Application.Movies.Queries.GetAllFilms;
using Portal.Application.Movies.Queries.GetFilmById;

namespace Portal.EF.Configurations;

public static class EFInitializer
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, string connectionString)
    {

        services.AddMemoryCache();

        #region DatabaseConfigs 

        services.AddDbContext<EFdbApplication>(options =>
        {
            options.UseSqlServer(connectionString,
                 serverDbContextOptionsBuilder =>
                 {
                     var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                     serverDbContextOptionsBuilder.CommandTimeout(minutes);
                     serverDbContextOptionsBuilder.EnableRetryOnFailure();
                 });
        });




        #endregion



        #region Movies
        services.AddTransient<FilmRepository,EFFilmRepository>();
        services.AddMediatR(typeof(AddFilmCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(GetAllFilmsQuery).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(GetFilmByIdQuery).GetTypeInfo().Assembly);
        #endregion
        #region Users
        services.AddTransient<RoleRepository, EFRoleRepository>();
        services.AddTransient<UserRepository, EFUserRepository>();

        services.AddMediatR(typeof(AddUserCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(EditUserCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(GetUserByIdQuery).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(GetUsersQuery).GetTypeInfo().Assembly);
        #endregion

        

        #region IOC
        services.AddTransient<IDataSeeder, DataSeeder>();
        services.AddTransient<UnitOfWork, EFUnitOfWork>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
        services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(CommitCommandPostProcessor<,>));
        #endregion


        //---------------------

        return services;
    }


}
