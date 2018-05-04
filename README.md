# cloudpayments
Example of an online store that accepts paymets using russian cloudservice.ru service.

Solution is based on ASP.NET CORE, Entity Framework Core, and AngularJS.

These are generic installation instructions.
1. Edit database connection string in file appsettings.json;
2. Download and install .NET CORE SDK 2.1;
3. In the solution's forder execute following commands:
"dotnet restore",
"dotnet ef migrations add Initial",
"dotnet ef database update",
"dotnet build";
4. Populate table Products in database StoreVyatkin (file products.sql contains TSQL script to populate table);
5. In the solution's forder execute "dotnet run" command.

Your online store is running now on a local web-server with url http://localhost:5000
