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

## 🧪 Функциональные e2e тест-кейсы (Gherkin)

<details>
<summary>Развернуть тест-кейсы</summary>

```gherkin
Feature: WeatherApp Entry and Reporting

  Background:
    Given the WeatherApp console application is running

  # --- УСПЕШНЫЙ СЦЕНАРИЙ СОХРАНЕНИЯ ---
  # Проверка успешного ввода и сохранения данных в файл
  Scenario: Successful weather entry and saving to file
    When the user enters temperature "0" (within -90 to +60)
    And the user selects weather condition "3" (Rain)
    And the user enters comment "Весенний дождик"
    And the user chooses action "Save to file"
    And the user enters a valid folder path "/tmp"
    Then the application displays a success message with the full file path
    And the file is created in "/tmp" with correct content:
      | Temperature | Condition | Comment         |
      | 0 °C        | Rain      | Весенний дождик |

  # --- ВАЛИДАЦИЯ ТЕМПЕРАТУРЫ ---
  # Проверка нижней границы температуры
  Scenario: Temperature below minimum edge
    When the user enters temperature "-91"
    Then the application displays an error "Temperature must be between -90 and +60 °C."
    And the user is asked to enter the temperature again

  # Проверка верхней границы температуры
  Scenario: Temperature above maximum edge
    When the user enters temperature "61"
    Then the application displays an error "Temperature must be between -90 and +60 °C."
    And the user is asked to enter the temperature again

  # --- ВАЛИДАЦИЯ ВЫБОРА ПОГОДЫ ---
  # Индекс погодного состояния меньше минимального
  Scenario: Invalid weather condition index (below minimum)
    When the user selects weather condition "0"
    Then the application displays an error "Invalid selection. Try again."
    And the user is asked to select a weather condition again

  # Индекс погодного состояния больше максимального
  Scenario: Invalid weather condition index (above maximum)
    When the user selects weather condition "10"
    Then the application displays an error "Invalid selection. Try again."
    And the user is asked to select a weather condition again

  # --- ВАЛИДАЦИЯ КОММЕНТАРИЯ ---
  # Пустой комментарий
  Scenario: Empty comment
    When the user enters an empty comment ""
    Then the application displays an error "Comment cannot be empty."
    And the user is asked to enter the comment again

  # Слишком длинный комментарий
  Scenario: Too long comment
    When the user enters a comment longer than 200 characters
    Then the application displays an error "Comment is too long. Max 200 characters."
    And the user is asked to enter the comment again

  # --- ОШИБКА ДОСТУПА К ПАПКЕ ---
  # Попытка сохранить в запрещённую папку
  Scenario: Saving to a forbidden folder
    Given the user has entered all valid weather data
    When the user chooses action "Save to file"
    And the user enters folder path "/root/forbidden"
    Then the application displays an error "Access to the path is denied."
    And the user is asked to enter the folder path again
```
</details>