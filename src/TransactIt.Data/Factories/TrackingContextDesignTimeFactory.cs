using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TransactIt.Data.Contexts;

namespace TransactIt.Data.Factories
{
    public class TrackingContextDesignTimeFactory : IDesignTimeDbContextFactory<TrackingContext>
    {
        public TrackingContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<TrackingContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("TrackingContext"));
            return new TrackingContext(optionsBuilder.Options);
        }
    }
}
