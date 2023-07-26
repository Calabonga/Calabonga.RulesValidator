using System.Collections.Generic;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Empty result return when now rules triggered
    /// </summary>
    public class EmptyValidationResult<T> : ValidationResult<T>
    {
        /// <inheritdoc />
        public EmptyValidationResult(T entity)
        {
            Entity = entity;
        }

        /// <inheritdoc />
        public override bool HasTriggered => false;

        /// <inheritdoc />
        public override IEnumerable<string> Errors
        {
            get
            {
                yield break;
            }
        }
    }
}