using System.Collections.Generic;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Rule triggered result
    /// </summary>
    public sealed class ErrorValidationResult<T> : ValidationResult<T>
    {
        public ErrorValidationResult(IRule rule, T entity)
        {
            Rule = rule;
            Entity = entity;
        }

        /// <summary>
        /// Triggered Rule
        /// </summary>
        private IRule Rule { get; }

        /// <summary>
        /// Indicates that the rule is fired
        /// </summary>
        public override bool HasTriggered => Rule != null;

        /// <summary>
        /// Validation message text
        /// </summary>
        public override IEnumerable<string> Errors
        {
            get
            {
                if (HasTriggered)
                {
                    yield return $"{Rule.DisplayName} ({Rule.Name})";
                }
            }
        }
    }
}