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
- [x] Приложение для клиента (react + redux).
- [x] Сервис для авторизации пользователей (IdentityServer4).
- [x] Сервис для постраничного отображения товаров.
- [x] Сохранение товаров в базу данных должно быть обернуто в транзакцию (UnitOfWork).
- [x] Middleware для кэширования.
- [x] Добавление товара.
- [X] Наличие unit-тестов в серверной части.
- [ ] Наличие unit-тестов в клиентской части.
- [x] Виртуальная прокрутка списка товаров + динамическая подгрузка данных по прокрутке (можно использовать готовые компоненты).
- [ ] Очистку только устаревшей страницы из кэша.
- [ ] Загрузку изображений в интерфейсе создания товара и показ изображения в витрине (можно использовать готовый компонент для загрузки файлов).
- [x] Адаптивную верстку (можно использовать css-фреймворк).
- [X] docker-compose (автозапуск сервисов).

### Подготовка БД, Redis и Back-end сервисов и приложения клиента
Из корневой папки репозитория запустить:

`docker-compose up -d`

Для остановки сервисов выполнить:

`docker-compose down`

После успешного запуска будут доступны сервисы:
- postgres
- redis
- product-facade-api
- user-manager-service
- client-app

Настройки клиента расположены в файле `client-app/.env` и `client-app/nginx.conf`, конфиг подключения к сервисам в файле `client-app/public/config.js`.

### Начало работы 
После успешного запуска сервисов докером, по адресу `http:localhost:4000` будет доступно рабочее приложение клиента.

### Для авторизации пользователей
За хранение и авторизацию пользователей отвечает `UserManagerService`.
Для входа можно использовать следующую информацию:
Логин `test@one.com` или `test@two.com`
Пароль `1`

### Тесты для back-end

Перед запуском тестов должен быть запущен `postgresql` из `docker-compose.yml`.
В проекте `ProductFacadeApi` есть файл `Dockerfile.tests` для запуска тестов. Сейчас он для `docker-compose.yml` не используется.
