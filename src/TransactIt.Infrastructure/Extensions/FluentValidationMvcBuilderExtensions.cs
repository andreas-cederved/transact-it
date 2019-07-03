using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using TransactIt.Application.Read.Helpers;
using TransactIt.Application.Write.Helpers;

namespace TransactIt.Infrastructure.Extensions
{
    public static class FluentValidationMvcBuilderExtensions
    {
        public static IMvcBuilder AddFluentValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(new List<Assembly>
                {
                    ReadAssemblyHelper.Get(),
                    WriteAssemblyHelper.Get()
                });
            });
            return builder;
        }
    }
}
