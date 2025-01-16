using System.Collections.Generic;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Base class for validation result
    /// </summary>
    public abstract class ValidationResult<T> : IValidatorResult<T>
    {
        /// <summary>
        /// Indicates that the rule is fired
        /// </summary>
        public abstract bool HasTriggered { get; }

        /// <summary>
        /// Validation message text
        /// </summary>
        public abstract IEnumerable<string> Errors { get; }

        /// <summary>
        ///  Entity that has been validated
        /// </summary>
        public T Entity { get; set; }
    }
}