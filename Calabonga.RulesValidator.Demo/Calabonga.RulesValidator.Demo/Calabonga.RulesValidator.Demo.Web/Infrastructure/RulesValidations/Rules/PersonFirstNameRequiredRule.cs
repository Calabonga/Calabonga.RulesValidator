using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calabonga.RulesValidator;
using Calabonga.RulesValidator.Demo.Models;
using Microsoft.Extensions.Options;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.RulesValidations.Rules
{
    [DebuggerDisplay("{DisplayName}")]
    public class PersonFirstNameRequiredRule: ValidationRule<Person>
    {
        /// <inheritdoc />
        public override string DisplayName => "Требуется имя пользователя";

        /// <inheritdoc />
        protected override Func<Person, bool> GetCriteria()
        {
            return person => string.IsNullOrEmpty(person.FirstName);
        }

        /// <inheritdoc />
        public override int OrderIndex => 1;

        /// <inheritdoc />
        public override string Name => "FirstNameRule";
    }
}
