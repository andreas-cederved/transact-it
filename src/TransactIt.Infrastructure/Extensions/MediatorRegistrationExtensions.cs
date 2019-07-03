using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TransactIt.Application.Read.Helpers;
using TransactIt.Application.Write.Helpers;
using TransactIt.Infrastructure.Pipelines;

namespace TransactIt.Infrastructure.Extensions
{
    public static class MediatorRegistrationExtensions
    {
        public static IServiceCollection AddMediatorWithRequests(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
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
