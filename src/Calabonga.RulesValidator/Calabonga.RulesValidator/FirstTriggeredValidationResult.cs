using System.Collections.Generic;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Rule triggered result
    /// </summary>
    public class FirstTriggeredValidationResult<T> : ValidationResult<T>
    {
        public FirstTriggeredValidationResult(IRule rule, T entity)
        {
            Rule = rule;
            Entity = entity;
        }

        public IRule Rule { get; }

        public override bool HasTriggered => Rule != null;

        /// <inheritdoc />
        public override IEnumerable<string> Errors
        {
            get
            {
                if (!HasTriggered)
                {
                    yield return $"{Rule.DisplayName} ({Rule.Name})";
                }
            }
        }
    }
}