using Calabonga.RulesValidator;
using Calabonga.RulesValidator.Demo.Data;
using Calabonga.RulesValidator.Demo.Models;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.RulesValidations;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.RulesValidations.Rules;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Registrations for both points: API and Scheduler
    /// </summary>
    public partial class DependencyContainer
    {
        /// <summary>
        /// Register 
        /// </summary>
        /// <param name="services"></param>
        public static void Common(IServiceCollection services)
        {
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();

            // services
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IEmailService, EmailService>();

            // Rules validation registration

            services.AddTransient<IValidationRule<Person>, PersonFirstNameRequiredRule>();
            services.AddTransient<IValidationRule<Person>, PersonSecondNameRequiredRule>();
            services.AddTransient<IValidationRule<Person>, PersonLastNameRequiredRule>();
            services.AddTransient<IValidationRule<Person>, PersonAveRule>();
            
            services.AddTransient<IRulesValidator<Person>, PersonValidator>();
            services.AddTransient<IValidatorConfiguration<Person>, PersonValidationConfiguration>();

            // notifications
            Notifications(services);
        }
    }
}
