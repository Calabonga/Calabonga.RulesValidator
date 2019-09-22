using System;
using Calabonga.RulesValidator.Demo.Models.Base;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.Factories.Base;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.ViewModels
{
    /// <summary>
    /// ViewModelBase for WritableController
    /// </summary>
    public class ViewModelBase : IViewModel, IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
