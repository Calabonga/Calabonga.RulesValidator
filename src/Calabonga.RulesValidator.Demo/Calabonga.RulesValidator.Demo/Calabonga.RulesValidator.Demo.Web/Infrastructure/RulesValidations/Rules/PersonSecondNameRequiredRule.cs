using System;
using System.Diagnostics;
using Calabonga.RulesValidator;
using Calabonga.RulesValidator.Demo.Models;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.RulesValidations.Rules
{
    [DebuggerDisplay("{DisplayName}")]
    public class PersonSecondNameRequiredRule:  ValidationRule<Person>
    {
        /// <inheritdoc />
        public override string DisplayName => "Требуется отчество пользователя";

        /// <inheritdoc />
        protected override Func<Person, bool> GetCriteria()
        {
            return person => string.IsNullOrEmpty(person.SecondName);
        }

        /// <inheritdoc />
        public override int OrderIndex => 2;

        /// <inheritdoc />
        public override string Name => "SecondNameRule";
    }
}