**Please use IIS server to run the program as it was configured that way.**


Overview

The JobApplication API is built using ASP.NET Core and follows the principles of Clean Architecture. This architecture promotes separation of concerns, making the application more maintainable and testable. The API manages a job applicants data, including names, emails, phone numbers, Github and linkedinUrls and preferred call times.

Clean Architecture

The application is structured into several layers, each with its own responsibility:

Domain Layer: Contains the core business logic and entities, such as the Applicant entity.
Application Layer: Hosts the service interfaces and implementation that interact with the domain layer. It is responsible for business rules and orchestrating calls between the repository and the API layer.

Infrastructure Layer: Handles data access, including Entity Framework Core configurations and the DbContext. This layer also contains repository implementations.

API Layer: The presentation layer where the controllers reside, accepting HTTP requests and returning HTTP responses.

This architecture allows for easy testing, as you can mock dependencies in the application layer while keeping the domain and infrastructure separate.

Connection String
The application currently uses Windows Authentication to connect to the SQL Server database. You can update the connection string located in the appsettings.json file.

Default Connection String
"ConnectionStrings": {
    "Default": "Server=.; Database=ApplicantsDb; Trusted_Connection=true; TrustServerCertificate=true; Encrypt=false"
}

To update the connection string:

Open the appsettings.json file in the root of your project.

Modify the Default connection string as needed. Here are some examples:

SQL Server with SQL Authentication:

"Default": "Server=your_server; Database=ApplicantsDb; User Id=your_username; Password=your_password; Encrypt=true; TrustServerCertificate=true;"
Azure SQL Database:

"Default": "Server=tcp:your_server.database.windows.net,1433; Initial Catalog=ApplicantsDb; Persist Security Info=False; User ID=your_username; Password=your_password; MultipleActiveResultSets=False; Encrypt=True; TrustServerCertificate=False; Connection Timeout=30;"

Running Migrations with EF Core

To manage your database schema using Entity Framework Core migrations, follow these steps:

Open the Package Manager Console in Visual Studio or use the terminal.
Update-Database
Verify the Migration: Check the database to ensure the changes have been applied correctly.

Troubleshooting
If you encounter errors such as missing references or DbContext issues, ensure that:

Your project references the required Microsoft.EntityFrameworkCore.Design package.
The DbContext is correctly configured in the Startup.cs file.

Tests : 
Unit tests are added to ensure the creation and edition part based on the UID email, invalid inputs, and null required types.

Conclusion
This project is designed with Clean Architecture principles, making it scalable and maintainable. Feel free to customize the connection strings to fit your environment and use the migration commands to manage your database schema.

For further information or to report issues, please reach out via the repository.
