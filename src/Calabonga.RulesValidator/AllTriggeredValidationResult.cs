using System.Collections.Generic;
using System.Linq;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// WhenAll validation result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AllTriggeredValidationResult<T> : ValidationResult<T>
    {
        public AllTriggeredValidationResult(T entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Triggered rules
        /// </summary>
        public List<IValidatorResult<T>> TriggeredRules { get; } = new List<IValidatorResult<T>>();

        /// <inheritdoc />
        public override bool HasTriggered => TriggeredRules.Any();

        /// <inheritdoc />
        public override IEnumerable<string> Errors
        {
            get
            {
                foreach (var validatorResult in TriggeredRules.SelectMany(x => x.Errors))
                {
                    yield return validatorResult;
                }
            }
        }

        /// <summary>
        /// Add validation result to result list
        /// </summary>
        /// <param name="result"></param>
        protected internal void AddResult(IValidatorResult<T> result)
        {
            if (result != null)
            {
                TriggeredRules.Add(result);
            }
        }
    }
}