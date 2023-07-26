namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Default validator configuration
    /// </summary>
    public class ValidatorConfiguration<T> : IValidatorConfiguration<T>
    {
        /// <summary>
        /// Default validation mode for validator
        /// </summary>
        public virtual ValidatorMode ValidatorMode { get; set; } = ValidatorMode.First;
    }
}
