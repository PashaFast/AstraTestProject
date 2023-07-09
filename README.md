Как развернуть приложение на локальной машине: 

1) Скачать архив по ссылке: https://github.com/PashaFast/AstraTestProject
2) Настроить несколько запускаемых проектов: Solution => Properties=> Multiple startup projects: AstraTestProject, AstraTestProject.Web
3) Если на локальной машине отсутствует .NET 5 Windows Hosting Bundle, то необходимо скачать его по ссылке и установить: 
https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-5.0.17-windows-hosting-bundle-installer
4) В приложении используется локальный MS SQL Server "(localdb)\mssqllocaldb" для Windows.
Строка подключения к базе находится в AstraTestProject => appsettings.json => "ConnectionStrings".
При первом запуске приложения база данных создается автоматически.
Также происходит инициализация базы данных начальными данными в AstraTestProject.Data => AstraTestProjectContext.
5) Клиентская часть расположена в AstraTestProject.Web
6) Я не до конца понял из ТЗ, что подразумевается под столбцом Cost на веб странице, поэтому я реализовал следующую логику. 
На странице 2 столбца: Car Cost и Total Cost. 
Car Cost – подтягивается значение из Car.Cost.
Total Cost = Car Cost * Amount.
