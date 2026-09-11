# CLAUDE.md

Инструкции для Claude Code при работе именно с этим проектом —
`src/Calabonga.RulesValidator/Calabonga.RulesValidator/` (ядро nuget-пакета `Calabonga.RulesValidator`).

Общие правила репозитория — в корневом `.claude/CLAUDE.md` и `.claude/rules/*.md`. Этот файл
описывает, что в них применимо к данному проекту буквально, а что нет, и добавляет то, что
специфично именно для этой папки.

## Назначение проекта

Это сам nuget-пакет: набор абстракций и реализация "валидации через правила" (rule-based
validation) — без зависимостей от ASP.NET Core, EF Core или любого конкретного приложения.
Используется как библиотека другими проектами (в этом репозитории — `Calabonga.RulesValidator.AspNetCore`
и `Calabonga.RulesValidator.Samples`).

## Состав проекта

- `IRule.cs`, `IValidationRule.cs`, `ValidationRule.cs` — контракт правила и базовая реализация.
- `IRulesValidator.cs`, `RulesValidator.cs` — валидатор, прогоняющий правила по сущности.
- `IValidatorConfiguration.cs`, `ValidatorConfiguration.cs`, `ValidatorMode.cs` — конфигурация
  и режимы (`First` / `All`).
- `IValidatorResult.cs`, `ValidationResult.cs`, `ErrorValidationResult.cs`, `NoErrorValidationResult.cs`,
  `AllTriggeredValidationResult.cs`, `RulesNotFoundValidationResult.cs` — результаты валидации.
- `RulesValidatorExtensions.cs` — методы-расширения.
- `Calabonga.RulesValidator.csproj` — описание nuget-пакета (версия, теги, README/иконка пакета).

Новый публичный тип почти всегда означает новый файл с именем типа — здесь принято
"один публичный тип — один файл", этому следуй и для новых классов.

## Целевой фреймворк

В `.csproj` указан единственный `TargetFrameworks` — `netstandard2.1`. Папки `obj/Debug/netcoreapp2.2`
и `obj/Debug/netcoreapp3.1` — это артефакты прошлых сборок, а не актуальный мультитаргетинг; не
ориентируйся на них и не добавляй эти TFM обратно без явной просьбы пользователя.

`Nullable` в `.csproj` не включён (`<Nullable>enable</Nullable>` отсутствует) — ссылочные типы
в этом проекте не nullable-aware. Не добавляй `?` у ссылочных типов и не включай `Nullable`
самостоятельно: это изменит предупреждения по всему проекту и может затронуть потребителей
пакета. Если нужно включить nullable — явно спроси пользователя.

## Что из `.claude/rules/*.md` здесь НЕ применимо буквально

- **testing.md** (xUnit/Moq/AutoFixture, `WebApplicationFactory`): в этом проекте нет API/веб-слоя,
  пункт про `WebApplicationFactory` неприменим. Соглашения по именованию тестов
  (`MethodName_Should_..._When_...`) применимы, если/когда в репозитории появится тестовый проект
  для этой библиотеки — сейчас такого проекта нет.
- **code-styles.md**, пункты про EF Core (`AsNoTracking()`, `IDbContextFactory`) и про `TimeProvider`
  вместо `DateTime.UtcNow`: в этом проекте нет доступа к базе данных и нет кода, работающего с
  текущим временем — эти пункты здесь не применимы.
- **code-styles.md**, пункты про Blazor-компоненты (`[Inject]`, `[CascadingParameter]`, порядок
  `protected`-методов и т.д.): в этом проекте нет UI/Blazor-кода — не применимо.
- **code-styles.md**, пункт про `record`/`readonly record struct` для DTO: в этом проекте нет DTO
  в этом смысле (запросов API, ответов, событий домена) — применимо только если добавляются
  подобные типы.
- **conventions.md** (именование `Create[Entity]Command`, `Get[Entity]Query`, `[Entity]ViewModel`):
  это соглашения CQRS/MediatR-стиля для прикладных проектов — здесь нет команд, запросов и
  view-моделей, это правило не применимо к данной библиотеке.

## Что применимо и на что обратить внимание

- **Публичный API — особая осторожность.** Любое изменение сигнатур `IRule`, `IValidationRule<T>`,
  `IRulesValidator<T>`, `RulesValidator<T>`, `IValidatorConfiguration<T>`, `ValidatorMode` и
  результатов валидации — потенциальный breaking change для потребителей пакета. Перед такими
  изменениями уточни у пользователя, что это осознанно, и напомни о необходимости бампа
  мажорной версии (`<Version>` в `.csproj`, сейчас `3.1.1`).
- **Существующий стиль кода старее, чем в `code-styles.md`** (классический `namespace { }` вместо
  file-scoped, XML-документация на английском, явные `public` на членах интерфейсов не нужны —
  это язык требует implicit public). При правке существующих файлов сохраняй их текущий стиль
  ради согласованности; для новых файлов в этом проекте по умолчанию следуй тому же классическому
  стилю (блочный `namespace`, XML-комментарии), если пользователь явно не попросит file-scoped
  namespaces и современный синтаксис.
- **workflow.md** (ветки `feature/`/`bugfix/`/`hotfix/`, формат коммитов `type: description`,
  `dotnet test` перед коммитом, проверка на дублирующиеся имена файлов перед созданием новых
  классов) — применимо без изменений.
