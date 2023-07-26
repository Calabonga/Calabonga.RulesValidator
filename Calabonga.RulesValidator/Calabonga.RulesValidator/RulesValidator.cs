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

        /// <inheritdoc />
        public async Task<IValidatorResult<T>> ValidateAsync(T entity)
        {
            if (!HasRules)
            {
                return new RulesNotFoundValidationResult<T>(entity);
            }

            switch (_configuration.ValidatorMode)
            {
                case ValidatorMode.First:
                    foreach (var rule in Rules.OrderBy(x => x.OrderIndex))
                    {
                        var result = await rule.ValidateAsync(entity);
                        if (result.HasTriggered)
                        {
                            rule.IsTriggered = true;
                            return result;
                        }
                    }

                    return new EmptyValidationResult<T>(entity);

                case ValidatorMode.All:
                    var resultAll = new AllTriggeredValidationResult<T>(entity);
                    foreach (var rule in Rules.OrderBy(x => x.OrderIndex))
                    {
                        var result = await rule.ValidateAsync(entity);
                        if (result.HasTriggered)
                        {
                            rule.IsTriggered = true;
                            resultAll.AddResult(result);
                        }
                    }
                    return resultAll;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}