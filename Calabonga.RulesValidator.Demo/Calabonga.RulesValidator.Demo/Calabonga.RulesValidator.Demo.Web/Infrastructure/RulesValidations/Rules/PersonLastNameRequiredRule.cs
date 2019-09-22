using System;
using System.Diagnostics;
using Calabonga.RulesValidator;
using Calabonga.RulesValidator.Demo.Models;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.RulesValidations.Rules
{
    [DebuggerDisplay("{DisplayName}")]
    public class PersonLastNameRequiredRule:  ValidationRule<Person>
    {
        /// <inheritdoc />
        public override string DisplayName => "Требуется фамилия пользователя";

        /// <inheritdoc />
        protected override Func<Person, bool> GetCriteria()
        {
            return person => string.IsNullOrEmpty(person.LastName);
        }

        /// <inheritdoc />
        public override int OrderIndex => 3;

        /// <inheritdoc />
        public override string Name => "LastNameRule";
    }
}