using System.Collections.Generic;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Base class for validation result
    /// </summary>
    public abstract class ValidationResult<T> : IValidatorResult<T>
    {
        /// <inheritdoc />
        public abstract bool HasTriggered { get; }

        /// <inheritdoc />
        public abstract IEnumerable<string> Errors { get; }

        /// <inheritdoc />
        public T Entity { get; set; }
    }
}