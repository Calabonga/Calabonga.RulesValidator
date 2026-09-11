## Договоренности по тестированию

### Frameworks
- xUnit версии 3 и выше
- Moq
- Autofixture

### Интеграционные тесты
- Используй `WebApplicationFactory` для запуска полноценных тестовых серверов API.
- Используй `xUnit` для написания unit-тестов, так как это современный и популярный фреймворк для тестирования в .NET.
- Используй `Moq` для создания mock-объектов в unit-тестах, чтобы изолировать тестируемый код от внешних зависимостей.

### Шаблоны для именования
- `MethodName_Should_ExpectedBehavior_When_Condition` (например, `CreateUser_Should_ThrowException_When_EmailExists`) 
- `MethodName_ShouldNot_ExpectedBehavior_When_Condition` (например, `CreateUser_ShouldNot_ThrowException_When_EmailUnique`).