# Calabonga.RulesValidator

## Проверка объекта через правила
Валидировать объект, например, перед сохранением в базу данных, можно разными способами. Есть способ "Простой", обычно все им и пользуются. Хочу показать  "Продвинутый" способ. И не только показать сам способ, но и его реализацию. И даже презентовать новый nuget-пакет.

![Presentation](https://github.com/Calabonga/Calabonga.RulesValidator/blob/master/Whatnot/presentation.PNG)

## Видео презентация
Посмотреь видео вы можете [на канале youtube](https://youtu.be/qprdRbMgEZU). На видео показан процесс создания IRulesValidator<T>, а также пример использования уже готового nuget-пакета [Calabonga.RulesVlidator](https://www.nuget.org/packages/Calabonga.RulesValidator/)  

## Screenshots 

This is a sample for demo purposes. Please, see in repository on GitHub.com

### Register your rules in Dependency Injection container.
<img width="643" alt="2" src="https://github.com/user-attachments/assets/e036fbbf-00a6-470d-8542-de888de49c53" />

### Get validator from container
```csharp
var validator = container.GetRequiredService<PersonValidator>();
```

### Run your validator
<img width="625" alt="3" src="https://github.com/user-attachments/assets/be3dc17d-ba34-4842-8bc2-edcce4e94984" />

### The results can be like this
<img width="747" alt="1" src="https://github.com/user-attachments/assets/c7870fec-0990-4d78-a0ff-049359e05d58" />
