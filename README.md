# WeatherApp — Console Weather Station Application

## Описание

WeatherApp — это консольное приложение для сотрудников метеорологической станции:

- Ввод температуры (°C)
- Выбор погодного состояния (ясно, облачно, дождь, снег, град, туман)
- Комментарий к погоде
- Автоматическое сохранение даты и времени
- Сохранение данных в файл с уникальным именем или вывод структурированного отчёта в консоль
- Валидация всех полей (температура, комментарий, состояние)
- Покрытие unit- и интеграционными тестами (NUnit)

## 🚀 Быстрый старт

1. Клонируй репозиторий  
   `git clone https://github.com/edmuundoo/weather-console-app.git`
2. Установи [.NET 9+](https://dotnet.microsoft.com/en-us/download)
3. Запусти приложение:
   ```bash
   cd WeatherApp
   dotnet run

## 🖥️ Пример работы

```
+----------------------------------------+
|    WEATHER STATION CONSOLE APP ☁️☀️     |
+----------------------------------------+
| Enter, store and print weather data:   |
|  • Temperature (°C)                    |
|  • Weather condition (Clear, Cloudy..) |
|  • Comment about weather               |
|  • Date & time auto-saved              |
|  • Save report to file                 |
|  • Print report to screen              |
|  Author: Sergei Kazantsev, 2025        |
+----------------------------------------+

=== Weather Entry ===
Enter temperature (°C) [-90 .. +60]: 15

Select weather condition:
1. Clear
2. Cloudy
3. Rain
4. Snow
5. Hail
6. Fog

Enter number: 2

Enter comment (max 200 chars): Переменная облачность

Choose action:
1. Save to file
2. Print report

Your choice (1/2): 2

--- Weather Report ---
Temperature: 15 °C
Condition:   Cloudy
Comment:     Переменная облачность
Date/Time:   2024-06-08 17:21:12
```

## 📑 Документация
- Функциональные тест-кейсы в каталоге docs/