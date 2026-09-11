# CLAUDE.md

Инструкции для Claude Code при работе с этим репозиторием.

## О проекте

`Calabonga.RulesValidator` — небольшая библиотека для .NET, реализующая валидацию объектов
через набор изолированных "правил" (`IRule` / `IValidationRule<T>`), а не через один большой
метод проверки. Каждое правило инкапсулирует одно условие и решает, "сработало" ли оно
(`HasTriggered`) для проверяемой сущности.

Библиотека распространяется как nuget-пакет [Calabonga.RulesValidator](https://www.nuget.org/packages/Calabonga.RulesValidator/).

## Структура репозитория

Единого solution-файла для всего репозитория нет — у каждого проекта свой `.sln`.

- `src/Calabonga.RulesValidator/` — ядро библиотеки (сам nuget-пакет).
  Основные типы:
  - `IRule`, `IValidationRule<T>`, `ValidationRule<T>` — контракты и базовая реализация правила.
  - `IRulesValidator<T>`, `RulesValidator<T>` — валидатор, прогоняющий правила по сущности.
  - `IValidatorConfiguration<T>`, `ValidatorConfiguration<T>`, `ValidatorMode` — конфигурация
    (режим `First` — вернуть первое сработавшее правило, `All` — собрать все сработавшие).
  - Результаты валидации: `IValidatorResult<T>`, `ValidationResult<T>`, `ErrorValidationResult<T>`,
    `NoErrorValidationResult<T>`, `AllTriggeredValidationResult<T>`, `RulesNotFoundValidationResult<T>`.
  - `RulesValidatorExtensions` — методы-расширения.
- `src/Calabonga.RulesValidator.AspNetCore/` — интеграция с `Microsoft.Extensions.DependencyInjection`
  (`RulesValidatorConfigurationExtension.AddValidatorFor<T>`).
- `src/Calabonga.RulesValidator.Samples/` — консольный демо-проект (net9.0), показывающий
  регистрацию правил в DI и вызов `validator.ValidateAsync(entity, dynamicRules)`.
  Правила-примеры лежат в `PersonRules/`.
- `Whatnot/` — скриншоты и изображения для README.

## Как устроена валидация (для контекста при изменениях)

1. Правило (`IValidationRule<T>`) регистрируется в DI как `IValidationRule<TEntity>`.
2. Конкретный валидатор (например `PersonValidator`) наследует `RulesValidator<T>` и получает
   все зарегистрированные правила через конструктор.
3. `ValidateAsync` прогоняет правила по `OrderIndex` в соответствии с `ValidatorMode`
   (`First` или `All`) и возвращает `IValidatorResult<T>` с ошибками сработавших правил.
4. В `ValidateAsync` можно передать дополнительные `dynamicRules` — они добавляются к уже
   зарегистрированным на лету (см. `ExcludeAgeRule` в примере).

## Целевые фреймворки

Библиотека таргетится на несколько TFM одновременно (видно по `obj/`): `netstandard2.1`,
`netcoreapp2.2`, `netcoreapp3.1`. Sample-проект — `net9.0`. При правке `.csproj` сохраняй
мультитаргетинг основной библиотеки, если явно не попросили его сузить.