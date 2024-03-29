﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactIt.Data.Contexts;

namespace TransactIt.Infrastructure.Extensions
{
    public static class DatabaseContextRegistrationExtensions
    {
        public static IServiceCollection AddDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<TrackingContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TrackingContext"));
            });

            services.AddDbContextPool<NoTrackingContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TrackingContext"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services;
        }
    }
}
