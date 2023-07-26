namespace Calabonga.RulesValidator
{
    /// <summary>
    /// Rule interface
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Index sorting
        /// </summary>
        int OrderIndex { get; }

        /// <summary>
        /// Name for the rule
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Friendly name
        /// </summary>
        string DisplayName { get; }
    }
}