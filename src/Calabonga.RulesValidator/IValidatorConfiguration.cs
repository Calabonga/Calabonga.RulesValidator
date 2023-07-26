namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Validation configuration generic
    /// </summary>
    public interface IValidatorConfiguration<T>
    {
        /// <summary>
        /// Default validation mode for validator
        /// </summary>
        ValidatorMode ValidatorMode { get; set; }
    }
}