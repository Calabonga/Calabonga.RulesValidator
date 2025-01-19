using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Interface for Rules Validator
    /// </summary>
    public interface IRulesValidator<T> where T : class
    {
        /// <summary>
        /// Returns validator mode
        /// </summary>
        ValidatorMode Mode { get; }

        /// <summary>
        /// Returns validator has rules
        /// </summary>
        bool HasRules { get; }

        /// <summary>
        /// Returns task for first fired rule that should be checked
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dynamicRules"></param>
        /// <returns></returns>
        Task<IValidatorResult<T>> ValidateAsync(T entity, IEnumerable<IValidationRule<T>> dynamicRules = null);

        /// <summary>
        /// Represents rules to use in validation
        /// </summary>
        List<IValidationRule<T>> Rules { get; }

        /// <summary>
        /// Add rules for validator
        /// </summary>
        /// <param name="rules"></param>
        void AddRules(IEnumerable<IValidationRule<T>> rules);
    }
}
