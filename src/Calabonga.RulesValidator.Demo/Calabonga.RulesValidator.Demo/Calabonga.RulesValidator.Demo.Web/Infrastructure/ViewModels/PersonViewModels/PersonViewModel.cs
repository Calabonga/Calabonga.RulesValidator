namespace Calabonga.RulesValidatorDemo.Web.Infrastructure.ViewModels.PersonViewModels
{
    public class PersonViewModel
    {
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string FullName
        {
            get { return $"{LastName} {FirstName} {SecondName}"; }
        }
    }
}