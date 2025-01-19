using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Rule Validator for generic entity
    /// </summary>
    public abstract class RulesValidator<T> : IRulesValidator<T> where T : class
    {
        private readonly IValidatorConfiguration<T> _configuration;

        protected RulesValidator(IEnumerable<IValidationRule<T>> rules, IValidatorConfiguration<T> configuration)
        {
            _configuration = configuration;
            Rules = rules == null ? new List<IValidationRule<T>>() : rules.ToList();
        }

        /// <inheritdoc />
        public List<IValidationRule<T>> Rules { get; private set; }

        /// <inheritdoc />
        public void AddRules(IEnumerable<IValidationRule<T>> rules)
        {
            if (rules == null) throw new ArgumentNullException(nameof(rules));

            if (!HasRules)
            {
                Rules = rules.ToList();
            }
        }

        /// <inheritdoc />
        public ValidatorMode Mode => _configuration.ValidatorMode;

        /// <inheritdoc />
        public bool HasRules => Rules.Count > 0;

        /// <summary>
        /// Returns task for first fired rule that should be checked
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dynamicRules"></param>
        /// <returns></returns>
        public async Task<IValidatorResult<T>> ValidateAsync(T entity, IEnumerable<IValidationRule<T>> dynamicRules = null)
        {
            if (!HasRules && dynamicRules == null)
            {
                return new RulesNotFoundValidationResult<T>(entity);
            }

            if (dynamicRules != null)
            {
                foreach (var validationRule in dynamicRules)
                {
                    if (!Rules.Contains(validationRule))
                    {
                        Rules.Add(validationRule);
                    }
                }
            }

            switch (_configuration.ValidatorMode)
            {
                case ValidatorMode.First:

                    foreach (var rule in Rules.OrderBy(x => x.OrderIndex))
                    {
                        var result = await rule.ValidateAsync(entity);
                        if (!result.HasTriggered)
                        {
                            continue;
                        }

                        rule.IsTriggered = true;
                        return result;
                    }

                    return new NoErrorValidationResult<T>(entity);

                case ValidatorMode.All:

                    var resultAll = new AllTriggeredValidationResult<T>(entity);
                    foreach (var rule in Rules.OrderBy(x => x.OrderIndex))
                    {
                        var result = await rule.ValidateAsync(entity);
                        if (!result.HasTriggered)
                        {
                            continue;
                        }

                        rule.IsTriggered = true;
                        resultAll.AddResult(result);
                    }
                    return resultAll;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}