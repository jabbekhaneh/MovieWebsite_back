using Autofac.Extensions.DependencyInjection;
using MediatR;
using Portal.EF.Configurations;
using Portal.EF.Migrations;
using System.Reflection;

namespace Portal.WebApi;

public static class HostingExtentions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        string ConnectionString = $"data source =.; initial catalog =movies; integrated security = True; MultipleActiveResultSets=True";
        builder.Services.AddMvc();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
        builder.Services.ConfigureApplicationServices(ConnectionString);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        return builder.Build();
    }
    //-----------------------------------
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        
        if (app.Environment.IsDevelopment())
        {
            MigrationManager.Runner();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        app.MapGet("/404/{*catchall}", async context =>
        {
            await context.Response.WriteAsJsonAsync("404 ...");
        });

        //app.Run(async (context) =>
        //{
        //    await context.Response.WriteAsJsonAsync("RUN APP ...");
        //});
        return app;
    }


}

