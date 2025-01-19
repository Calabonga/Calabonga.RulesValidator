using Calabonga.DemoClasses;
using Calabonga.RulesValidator;
using Calabonga.RulesValidator.Samples;
using Calabonga.RulesValidator.Samples.PersonRules;
using Calabonga.Utils.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
    .Build();

// logger from Serilog
var logger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Console().CreateLogger();

// services
var services = new ServiceCollection();
services.AddLogging(x => x.AddSerilog(logger));
services.Configure<AppSettings>(x => { configuration.GetSection(nameof(AppSettings)).Bind(x); });

// validations dependencies
services.AddSingleton<PersonValidator>();
services.AddSingleton<IValidatorConfiguration<Person>, PersonValidationConfiguration>();
services.AddScoped<IValidationRule<Person>, ShouldBeOlderThan18Rule>();
services.AddScoped<IValidationRule<Person>, NameRequiredRule>();
services.AddScoped<IValidationRule<Person>, WeightGreaterThen100Rule>();
services.AddScoped<IValidationRule<Person>, WeightLessThen50Rule>();


var container = services.BuildServiceProvider(new ServiceProviderOptions() { ValidateOnBuild = true });
var validator = container.GetRequiredService<PersonValidator>();


var people = new[] {
    new Person { Age= 5 },
    new Person { Age = 21, Name = "Bob" }
};

var excludeAgeRule = new ExcludeAgeRule(21);


foreach (var person in people.WithIndex())
{
    Console.WriteLine($"Person #{person.Index + 1}");
    Console.WriteLine("---------------");

    var result = await validator.ValidateAsync(person.Item, [excludeAgeRule]);
    if (result.HasTriggered)
    {
        Console.WriteLine($"Has errors: {result.Errors.Count()}");

        foreach (var error in result.Errors)
        {
            Console.WriteLine(error);
        }
    }
    Console.WriteLine();
}

Console.ReadLine();