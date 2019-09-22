using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calabonga.RulesValidator;
using Calabonga.RulesValidator.Demo.Models;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.RulesValidations
{
    public class PersonValidationConfiguration: IValidatorConfiguration<Person>
    {
        /// <inheritdoc />
        public ValidatorMode ValidatorMode { get; set; } = ValidatorMode.All;
    }
}
