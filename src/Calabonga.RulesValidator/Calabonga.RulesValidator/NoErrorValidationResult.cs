using System.Collections.Generic;
using System.Linq;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Empty result return when now rules triggered
    /// </summary>
    public sealed class NoErrorValidationResult<T> : ValidationResult<T>
    {
        /// <inheritdoc />
        public NoErrorValidationResult(T entity)
        {
            Entity = entity;
        }

        /// <inheritdoc />
        public override bool HasTriggered => false;

        /// <inheritdoc />
        public override IEnumerable<string> Errors => Enumerable.Empty<string>();
    }
}