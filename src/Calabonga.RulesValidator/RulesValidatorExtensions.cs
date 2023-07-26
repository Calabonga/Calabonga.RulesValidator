using System.Collections.Generic;
using System.Linq;

namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Extensions for IRulesValidator<T>
    /// </summary>
    public static class RulesValidatorExtensions
    {

        /// <summary>
        /// Returns triggered rules
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<IRule> GetTriggered<T>(this List<IValidationRule<T>> source) where T : class
        {
            if (source.Any())
            {
                return source.Where(x => x.IsTriggered);
            }
            return new List<IRule>();
        }
    }
}