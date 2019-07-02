using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TransactIt.Application.Read.Helpers;
using TransactIt.Application.Write.Helpers;

namespace TransactIt.Infrastructure.Extensions
{
    public static class MediatorRegistrationExtensions
    {
        public static IServiceCollection AddMediatorWithRequests(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.AsTransient();
            },
                ReadAssemblyHelper.Get(),
                WriteAssemblyHelper.Get());
            return services;
        }
    }
}
