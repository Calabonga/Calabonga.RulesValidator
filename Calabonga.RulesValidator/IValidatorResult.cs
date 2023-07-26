using System.Collections.Generic;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Validator result interface
    /// </summary>
    public interface IValidatorResult
    {
        /// <summary>
        /// Indicates that the rule is fired
        /// </summary>
        bool HasTriggered { get; }

        /// <summary>
        /// Validation message text
        /// </summary>
        IEnumerable<string> Errors { get; }

    }

    /// <summary>
    /// Validator result interface
    /// </summary>
    public interface IValidatorResult<out T> : IValidatorResult
    {

        /// <summary>
        ///  Entity that has been validated
        /// </summary>
        T Entity { get; }
    }
}
