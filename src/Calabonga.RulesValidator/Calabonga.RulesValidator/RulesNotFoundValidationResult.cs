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

        /// <inheritdoc />
        public override bool HasTriggered => false;

        /// <inheritdoc />
        public override IEnumerable<string> Errors => new[] { "Rules not found. Validation stopped" };
    }
}