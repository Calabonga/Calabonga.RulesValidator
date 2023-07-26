using System;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.RulesValidator.AspNetCore
{
    /// <summary>
    /// Validator configuration extensions
    /// </summary>
    public static class RulesValidatorConfigurationExtension
    {
        /// <summary>
        /// Configure validator for concrete entity service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureValidatorFor<T>(this IServiceCollection services, Action<IValidatorConfiguration<T>> config) where T : class
        {
            if (config == null)
            {
                throw new InvalidOperationException(nameof(IValidatorConfiguration<T>));
            }

            var configuration = new ValidatorConfiguration<T>();
            config(configuration);

            services.AddSingleton(configuration);
            services.AddTransient<IValidatorConfiguration<T>, ValidatorConfiguration<T>>(x => configuration);
        }
    }
}
