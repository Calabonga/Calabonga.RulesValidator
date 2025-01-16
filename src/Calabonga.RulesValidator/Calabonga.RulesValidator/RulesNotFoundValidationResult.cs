using System.Collections.Generic;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Rules not Found result return when no rules has been found for validation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RulesNotFoundValidationResult<T> : ValidationResult<T>
    {
        public RulesNotFoundValidationResult(T entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Indicates that the rule is fired
        /// </summary>
        public override bool HasTriggered => false;

        /// <summary>
        /// Validation message text
        /// </summary>
        public override IEnumerable<string> Errors => new[] { "No rules were found. Validation process not started." };
    }
}