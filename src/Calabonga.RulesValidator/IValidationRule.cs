using System.Threading.Tasks;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Interface for ValidationRule
    /// </summary>
    public interface IValidationRule<T> : IRule where T : class
    {
        /// <summary>
        /// Returns true when rules triggered
        /// </summary>
        bool IsTriggered { get; set; }

        /// <summary>
        /// validation rule
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IValidatorResult<T>> ValidateAsync(T entity);
    }
}