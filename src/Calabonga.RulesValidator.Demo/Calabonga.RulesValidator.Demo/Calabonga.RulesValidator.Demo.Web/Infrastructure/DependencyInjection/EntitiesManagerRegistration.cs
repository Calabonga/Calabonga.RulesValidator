﻿using System.Linq;
using System.Reflection;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.Managers.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Dependency Injection Registration
    /// </summary>
    public partial class DependencyContainer
    {
        /// <summary>
        /// Registration Entity Managers
        /// </summary>
        /// <param name="services"></param>
        public static void EntityManagers(IServiceCollection services)
        {
            // Register Entity Managers
            //services.AddScoped<IEntityManager<TextCard, TextCardCreateViewModel, TextCardUpdateViewModel>, TextCardManager>();

            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && !t.IsAbstract);
            foreach (var type in types)
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (!i.IsGenericType || i.GetGenericTypeDefinition() != typeof(IEntityManager<,,>))
                    {
                        continue;
                    }

                    var interfaceType = typeof(IEntityManager<,,>).MakeGenericType(i.GetGenericArguments());
                    services.AddTransient(interfaceType, type);
                }
            }
        }
    }
}
