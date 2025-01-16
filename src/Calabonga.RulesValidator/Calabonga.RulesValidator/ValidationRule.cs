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
        public virtual string Name => GetType().Name;

        /// <summary>
        /// User-friendly display name
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Return order index for rules sorting
        /// </summary>
        public virtual int OrderIndex { get; } = 0;

        /// <summary>
        /// Create criteria where shit rule should fire
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
                ? Task.FromResult<IValidatorResult<T>>(new ErrorValidationResult<T>(this, entity))
                : Task.FromResult<IValidatorResult<T>>(new NoErrorValidationResult<T>(entity));
        }
    }
}