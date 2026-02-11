Важно, при работе с API добавь в appsettings.json строку подключения
Потом зарегистрируй её в Program.cs
И пользуйся БД на здоровье.

Команда на получение моделей и контекста БД:
``` PM
Scaffold-DbContext "{Вот тут твоя строка подключения}" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir DatabaseContext
```
``` PM
Scaffold-DbContext "Data Source=DESKTOP-KSRNQI5;Initial Catalog=DigitalTwinPatientDBTest;Integrated Security=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir DatabaseContext -Force
```
Если изменил БДху и надо обновить модели, добавь в конец команды: `-Force`

Так же важно, из всех эендпоинтов, только роут с авторизацией без заголовков, остальные обязательно должны их принимать и работать с ними, на случай если JWT устарел или вообще пользователь не имеет доступа к какой-то инфе.
Пример получения токена из шапки запроса

``` csharp
[HttpGet("/all_address")]
public IActionResult Get([FromHeader] string token)
{
    // работа с токеном или ещё какая дрянь
}
```