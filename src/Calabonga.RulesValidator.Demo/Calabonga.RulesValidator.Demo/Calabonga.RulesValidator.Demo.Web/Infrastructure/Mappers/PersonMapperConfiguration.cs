using Calabonga.EntityFrameworkCore.UOW;
using Calabonga.RulesValidator.Demo.Models;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.Mappers.Base;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.ViewModels.PersonViewModels;

namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper Configuration for entity Log
    /// </summary>
    public class PersonMapperConfiguration : MapperConfigurationBase
    {
        /// <inheritdoc />
        public PersonMapperConfiguration()
        {
            CreateMap<PersonCreateViewModel, Person>();

            CreateMap<Person, PersonViewModel>();

            CreateMap<IPagedList<Person>, IPagedList<PersonViewModel>>()
                .ConvertUsing<PagedListConverter<Person, PersonViewModel>>();
        }
    }
}
