## Тестовое задание ЦРК СОТА

Описание задания:
https://docs.google.com/document/d/1uPjrecuVEdYpcWsae7kLBq2kGA5C35Di5hOo9AW9N84/edit

### Схема работы сервисов

<img src='https://g.gravizo.com/svg?
@startuml;
actor User;
participant "Client" as A;
participant "ProductFacadeApi" as B;
participant "UserManagementService" as C;
User -> A: Authorize;
activate A;
A -> B: Create Request;
activate B;
B -> C: GetToken;
activate C;
C -> B: GetUser;
deactivate C;
B -> A: Response Created;
deactivate B;
A -> User: Login;
deactivate A;
User -> A: GetProducts;
activate A;
A -> B: Create Request (Check token);
activate B;
B -> A: GetProductsPage;
deactivate B;
A -> User: Show Products List;
@enduml
'>

### Что реализовано из описания задачи:
- [x] Приложение для клиента (react + redux.).
- [x] Сервис для авторизации пользователей (IdentityServer4).
- [x] Сервис для постраничное отображение товаров.
- [x] Сохранение товаров в базу данных должно быть обернуто в транзакцию (UnitOfWork).
- [x] Middleware для кэширования.
- [x] Добавление товара.
- [ ] Наличие unit-тестов в серверной части.
- [ ] Наличие unit-тестов в клиентской части.
- [x] Виртуальная прокрутка списка товаров + динамическая подгрузка данных по прокрутке (можно использовать готовые компоненты).
- [ ] Очистку только устаревшей страницы из кэша.
- [ ] Загрузку изображений в интерфейсе создания товара и показ изображения в витрине (можно использовать готовый компонент для загрузки файлов).
- [x] Адаптивную верстку (можно использовать css-фреймворк).
- [ ] docker-compose (не запустилась сборка на windows).

### Запуск клиентского приложения 

`yarn`

`yarn start`

### Подготовка БД и Redis

`docker-compose up -d`

### Для авторизации пользователей

Логин `test@one.com` или `test@two.com`

Пароль `1`

### Тесты для back-end

Перед запуском тестов должен быть запущен postgresql из docker-compose. В проекте с тестами расположен appsettings.json с настройками подключения к БД.
