using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchEngines.Api.Services;
using SearchEngines.Api.Services.Contracts;

namespace SearchEngines.Api.Config
{
    public static class ServicesConfigurator
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services) {
            services.AddSingleton<ISearchEnginesService, SearchEnginesService>();
            return services;
        }
    }
}
