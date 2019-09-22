using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calabonga.RulesValidator;
using Calabonga.RulesValidator.Demo.Models;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.RulesValidations.Rules
{
    [DebuggerDisplay("{DisplayName}")]
    public class PersonAveRule:  ValidationRule<Person>
    {
        /// <inheritdoc />
        public override string DisplayName => "Пользователь должен быть старше 21 года";

        /// <inheritdoc />
        protected override Func<Person, bool> GetCriteria()
        {
            return person => person.Age < 21;
        }

        /// <inheritdoc />
        public override int OrderIndex => 4;

        /// <inheritdoc />
        public override string Name => "AgeRule";
    }
}
