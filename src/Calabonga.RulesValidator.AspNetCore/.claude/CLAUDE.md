# CLAUDE.md

Инструкции для Claude Code при работе именно с этим проектом —
`src/Calabonga.RulesValidator.AspNetCore/Calabonga.RulesValidator.AspNetCore/`
(nuget-пакет `Calabonga.RulesValidator.AspNetCore`).

Общие правила репозитория — в корневом `.claude/CLAUDE.md` и `.claude/rules/*.md`. Этот файл
описывает, что в них применимо к данному проекту буквально, а что нет, и добавляет то, что
специфично именно для этой папки.

## Назначение проекта

Тонкий пакет-расширение поверх `Calabonga.RulesValidator` (см. `PackageReference` в `.csproj`):
добавляет один метод-расширение `IServiceCollection.AddValidatorFor<T>(...)` для регистрации
`IValidatorConfiguration<T>` в DI-контейнере ASP.NET Core / `Microsoft.Extensions.DependencyInjection`.
Никакой другой логики (правил, валидаторов, результатов) здесь нет и не должно быть — это
исключительно "клей" для DI, а не самостоятельная функциональность.

## Состав проекта

- `RulesValidatorConfigurationExtension.cs` — единственный файл с кодом, статический класс с
  методом `AddValidatorFor<T>`.
- `Calabonga.RulesValidator.AspNetCore.csproj` — описание nuget-пакета; зависит от
  `Calabonga.RulesValidator` (версия должна быть синхронизирована с версией самого пакета —
  сейчас обе `3.1.1`) и от `Microsoft.Extensions.DependencyInjection.Abstractions`.

При добавлении новых расширений следуй тому же правилу "один публичный тип — один файл",
что и в `Calabonga.RulesValidator`.

## Целевой фреймворк

`TargetFrameworks` в `.csproj` — только `netstandard2.1`. Папки `obj/Debug/netcoreapp2.2` и
`obj/Debug/netcoreapp3.1` — это артефакты прошлых сборок, актуальным мультитаргетингом не
являются; не ориентируйся на них.

`Nullable` не включён в `.csproj` — ссылочные типы не nullable-aware, как и в
`Calabonga.RulesValidator`. Не включай `Nullable` самостоятельно без явной просьбы пользователя.

## Синхронизация версий с `Calabonga.RulesValidator`

`<Version>` этого пакета и `<PackageReference Include="Calabonga.RulesValidator" Version="...">`
в `.csproj` должны указывать на одну и ту же версию (см. `<PackageReleaseNotes>nuget-version
synchronization</PackageReleaseNotes>` — это устоявшаяся практика в репозитории). Если меняешь
версию основного пакета, обнови и здесь оба места; если меняешь версию только этого пакета —
не меняй зависимость на `Calabonga.RulesValidator`, если её версия не менялась.

## Что из `.claude/rules/*.md` здесь НЕ применимо буквально

- **testing.md**: как и в `Calabonga.RulesValidator`, тестового проекта для этого пакета нет.
  Пункт про `WebApplicationFactory` (полноценный тестовый API-сервер) неприменим — это библиотека
  расширения DI, а не сама конечная точка API. Соглашения по именованию тестов применимы, если
  тестовый проект появится.
- **code-styles.md**, пункты про EF Core (`AsNoTracking()`, `IDbContextFactory`) и про
  `TimeProvider`/`DateTime.UtcNow`: здесь нет доступа к БД и нет кода, работающего с текущим
  временем — не применимо.
- **code-styles.md**, пункты про Blazor-компоненты (`[Inject]`, `[CascadingParameter]`, порядок
  `protected`-членов): в этом проекте нет UI/Blazor-кода — не применимо.
- **code-styles.md**, пункт про `record`/`readonly record struct` для DTO: здесь нет DTO — не
  применимо, если не добавляются подобные типы.
- **conventions.md** (именование `Create[Entity]Command`, `Get[Entity]Query`, `[Entity]ViewModel`):
  это CQRS/MediatR-соглашения для прикладных проектов, здесь нет команд, запросов и view-моделей
  — не применимо.

## Что применимо и на что обратить внимание

- **Публичный API — особая осторожность.** `AddValidatorFor<T>` — публичная точка входа пакета;
  изменение её сигнатуры или поведения — потенциальный breaking change для потребителей. Перед
  такими изменениями уточни у пользователя и напомни про бамп версии в `.csproj` (см. раздел
  про синхронизацию версий выше).
- **Стиль существующего кода старее, чем в `code-styles.md`** (блочный `namespace { }`, а не
  file-scoped, XML-документация на английском). При правке `RulesValidatorConfigurationExtension.cs`
  сохраняй текущий стиль ради согласованности; для новых файлов в этом проекте по умолчанию
  следуй тому же классическому стилю, если пользователь явно не попросит современный синтаксис.
- **workflow.md** (ветки `feature/`/`bugfix/`/`hotfix/`, формат коммитов `type: description`,
  `dotnet test` перед коммитом, проверка на дублирующиеся имена файлов перед созданием новых
  классов) — применимо без изменений.
