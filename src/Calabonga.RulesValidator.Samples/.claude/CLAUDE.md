# CLAUDE.md

Инструкции для Claude Code при работе именно с этим проектом —
`src/Calabonga.RulesValidator.Samples/Calabonga.RulesValidator.Samples/`
(консольный демо-проект, не публикуется как nuget-пакет).

Общие правила репозитория — в корневом `.claude/CLAUDE.md` и `.claude/rules/*.md`. Этот файл
описывает, что в них применимо к данному проекту буквально, а что нет, и добавляет то, что
специфично именно для этой папки.

## Назначение проекта

Демонстрационное консольное приложение, показывающее, как использовать `Calabonga.RulesValidator`
на практике: регистрация правил и конфигурации валидатора в DI, разрешение валидатора из
контейнера, вызов `ValidateAsync` с динамическими правилами. Ссылается на основную библиотеку
через `ProjectReference` (а не через nuget), то есть всегда собирается с актуальным кодом
`Calabonga.RulesValidator` из этого репозитория.

Это код для примера/презентации (см. README и видео в корне репозитория), а не production-код
и не публичный API — совместимость с предыдущими версиями значения не имеет.

## Состав проекта

- `Program.cs` — точка входа: настройка DI, логирования (Serilog), регистрация правил и
  конфигурации, прогон валидатора по массиву `Person`.
- `AppSettings.cs` — модель настроек, биндится из `appSettings.json`.
- `PersonValidator.cs` — конкретный валидатор (`RulesValidator<Person>`).
- `PersonValidationConfiguration.cs` — конфигурация валидатора (`IValidatorConfiguration<Person>`,
  режим `ValidatorMode.All`).
- `PersonRules/` — примеры правил (`NameRequiredRule`, `ShouldBeOlderThan18Rule`,
  `WeightGreaterThen100Rule`, `WeightLessThen50Rule`, `ExcludeAgeRule`) — каждое наследует
  `ValidationRule<Person>` и переопределяет `DisplayName` и `ThrowWhen()`.

При добавлении нового примера правила клади его в `PersonRules/` и регистрируй в `Program.cs`
через `services.AddScoped<IValidationRule<Person>, ...>()`, по аналогии с существующими.

## Целевой фреймворк и стиль — отличаются от остальных проектов репозитория

В отличие от `Calabonga.RulesValidator` и `Calabonga.RulesValidator.AspNetCore` (оба —
`netstandard2.1`, без `Nullable`, блочные `namespace { }`), этот проект:

- таргетится на `net9.0`;
- имеет `<ImplicitUsings>enable</ImplicitUsings>` и `<Nullable>enable</Nullable>` — здесь
  реально применимы правила из `code-styles.md` про nullable-ссылочные типы (не подавляй `!`
  без документирования, используй `string?` там, где значение может быть `null`, как уже
  сделано в `AppSettings.cs`);
- уже написан с file-scoped namespaces и без блочных `{ }` — при добавлении нового кода
  сохраняй именно этот, более современный, стиль (в отличие от двух других проектов
  репозитория, где принят старый стиль).

## Что из `.claude/rules/*.md` здесь НЕ применимо буквально

- **testing.md**: тестового проекта нет; пункт про `WebApplicationFactory` неприменим — это
  консольное приложение, а не веб-API. Само приложение не имеет публичных методов/классов,
  которые нужно покрывать unit-тестами в общепринятом смысле (это демо, а не библиотека).
- **code-styles.md**, пункты про EF Core (`AsNoTracking()`, `IDbContextFactory`) и про
  `TimeProvider`/`DateTime.UtcNow`: здесь нет доступа к БД и нет кода, работающего с текущим
  временем — не применимо.
- **code-styles.md**, пункты про Blazor-компоненты: в проекте нет UI/Blazor — не применимо.
- **code-styles.md**, пункт про `record`/`readonly record struct` для DTO: в проекте нет DTO
  в этом смысле — не применимо.
- **conventions.md** (именование `Create[Entity]Command`, `Get[Entity]Query`, `[Entity]ViewModel`):
  CQRS/MediatR-соглашения для прикладных проектов, здесь нет команд/запросов/view-моделей — не
  применимо.
- **workflow.md**, пункт про запуск `dotnet test` перед коммитом: применим формально, но
  фактического эффекта не даёт, так как тестов для этого проекта нет — не пропускай запуск,
  но не считай его отсутствие ошибкой.

## Что применимо и на что обратить внимание

- **Public API здесь не критичен.** В отличие от `Calabonga.RulesValidator` и
  `Calabonga.RulesValidator.AspNetCore`, изменения сигнатур в этом проекте не являются breaking
  change для внешних потребителей — это демо-приложение. Не нужно спрашивать разрешения на
  рефакторинг примеров ради ясности презентации, если это явно не публичный пакет.
- **Синхронизация с основной библиотекой.** Так как проект ссылается на `Calabonga.RulesValidator`
  через `ProjectReference`, а не `PackageReference`, при изменении публичного API основной
  библиотеки нужно поправить и использующий его код здесь (`PersonValidator`,
  `PersonValidationConfiguration`, правила в `PersonRules/`), иначе демо перестанет собираться.
- **workflow.md** (ветки `feature/`/`bugfix/`/`hotfix/`, формат коммитов `type: description`,
  атомарные коммиты, проверка на дублирующиеся имена файлов перед созданием новых классов) —
  применимо без изменений.
