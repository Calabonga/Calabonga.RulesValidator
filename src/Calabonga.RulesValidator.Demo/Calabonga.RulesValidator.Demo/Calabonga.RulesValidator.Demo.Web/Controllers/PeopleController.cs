using AutoMapper;
using Calabonga.OperationResultsCore;
using Calabonga.RulesValidator;
using Calabonga.RulesValidator.Demo.Models;
using Calabonga.RulesValidatorDemo.Web.Controllers.Base;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.RulesValidations;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.ViewModels.PersonViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.RulesValidatorDemo.Web.Controllers
{
    [Route("api/v1/[controller]")]
    public class PeopleController: OperationResultController
    {
        private readonly IRulesValidator<Person> _validator;
        private readonly IMapper _mapper;

        public PeopleController(
            IRulesValidator<Person> validator,
            IMapper mapper)
        {
            _validator = validator;
            _mapper = mapper;
        }

        [HttpPost("post-person")]
        public ActionResult<OperationResult<PersonViewModel>> PostPerson(PersonCreateViewModel model)
        {
            var operation = OperationResult.CreateResult<PersonViewModel>();
            var person = _mapper.Map<Person>(model);

            var validationResult  = _validator.Validate(person);



            var result = _mapper.Map<PersonViewModel>(person);
            operation.Result = result;
            operation.AddData(validationResult);
            return operation;
        }
    }
}
