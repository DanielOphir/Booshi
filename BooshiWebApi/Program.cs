using BooshiDAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooshiWebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Creating a scope and getting the service provider
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                // Seeding data if there's no data in the db
                var context = services.GetRequiredService<BooshiDBContext>();

                await context.Database.MigrateAsync();

                await Seed.SeedRoles(context);

                await Seed.SeedUsers(context);

                await Seed.SeedDeliveriesStatuses(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetService<ILogger<Program>>();
                logger.LogError(ex, "An error occured");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
