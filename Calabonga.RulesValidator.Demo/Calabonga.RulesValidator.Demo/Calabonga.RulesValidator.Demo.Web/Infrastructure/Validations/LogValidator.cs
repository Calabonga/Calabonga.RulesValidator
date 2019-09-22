using Calabonga.EntityFrameworkCore.UOW;
using Calabonga.RulesValidator.Demo.Models;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.Validations.Base;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.Validations
{
    /// <summary>
    /// Validator for entity Log
    /// </summary>
    public class LogValidator : EntityValidator<Log>
    {
        /// <inheritdoc />
        public LogValidator(IRepositoryFactory factory) : base(factory)
        {
        }
    }
}