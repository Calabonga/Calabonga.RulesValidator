using System;
using System.Threading.Tasks;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Base class for ValidationRule implementation
    /// </summary>
    public abstract class ValidationRule<T> : IValidationRule<T> where T : class
    {
        /// <summary>
        /// Name for the rule
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// User friendly display name
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Return order index for rules sorting
        /// </summary>
        public abstract int OrderIndex { get; }

        /// <summary>
        /// Create criteria
        /// </summary>
        /// <returns></returns>
        protected abstract Func<T, bool> GetCriteria();

        /// <summary>
        /// Returns true when rules triggered
        /// </summary>
        public bool IsTriggered { get; set; }

        /// <summary>
        /// Validate rule
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<IValidatorResult<T>> ValidateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} is NULL");
            }
            var func = GetCriteria();
            return func.Invoke(entity) 
                ? Task.FromResult<IValidatorResult<T>>(new FirstTriggeredValidationResult<T>(this, entity))
                : Task.FromResult<IValidatorResult<T>>(new EmptyValidationResult<T>(entity));
        }
    }
}