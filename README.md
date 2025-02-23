# Book Management

## Description
The **Book Management API** is a robust and scalable RESTful service designed to manage book data for libraries, bookstores, or any system that handles book inventories. It is built using **ASP.NET Core 8.0** and **SQL Server** with **Entity Framework Core (EF Core), The API supports **CRUD (Create, Read, Update, Delete) that allows users to manage book information. The API also  uses **JWT (JSON Web Token)** authorization, ensuring secure access for authenticated users.

Key Features
Book Creation: Add new books individually or in bulk.
Book Retrieval: Fetch a list of books with support for pagination and sorting (by popularity), or retrieve detailed information for a specific book by its ID.
Book Updates: Update book information with validation to prevent duplicate titles.
Bulk Deletion: Remove multiple books at once, with the ability to handle both successful and non-existent deletions.
Validation and Error Handling: The API checks that all input data is correct and provides clear, helpful error messages, making it easy for users to understand and fix any issues.

## Requirements
Before running the API, ensure you have the following installed:
* **.NET 8.0 SDK**
* **SQL Server** (Local or Cloud instance)

## Setup Instructions

### 1. Clone the Repository
Clone the project repository to your local machine
```
git clone https://github.com/your-repository/book-management-api.git
cd book-management-api
```

### 2. Configure the Database
* Set up a **SQL Server** instance (either local or cloud-based).
* Open the `appsettings.json` file in the project root and configure the connection string for your SQL Server instance.

Example:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=BookManagementDb;Trusted_Connection=True;"
}
```

### 3. Install Dependencies
Restore the NuGet packages required for the project:
```
dotnet restore
```

### 4. Set Up JWT Authentication
Ensure your project is configured for **JWT authentication** in the `Program.cs` file.
• Ensure your project is configured for JWT authentication in the `Program.cs` file.
• **Add your JWT private key** to the `appsettings.json` or any other secure location you're using to store sensitive data.


### 5. Run Database Migrations
If using **Entity Framework Core**:
* Run the following commands to create the necessary database tables:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 6. Run the API
Start the API server:
```
dotnet run
```

## Testing the API
Use swagger https://{host}:7214/swagger/index.html
