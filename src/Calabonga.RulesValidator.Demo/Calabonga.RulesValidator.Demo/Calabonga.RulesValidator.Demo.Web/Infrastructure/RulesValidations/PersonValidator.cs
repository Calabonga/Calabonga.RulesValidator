using System.Collections.Generic;
using Calabonga.RulesValidator;
using Calabonga.RulesValidator.Demo.Models;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.RulesValidations
{
    public class PersonValidator: RulesValidator<Person>
    {
        /// <inheritdoc />
        public PersonValidator(
            IEnumerable<IValidationRule<Person>> rules, 
            IValidatorConfiguration<Person> configuration) 
            : base(rules, configuration)
        {
        }
    }
}
