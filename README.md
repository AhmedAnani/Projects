# Library Management System

A database-driven C# console application for managing a library. The system supports books, eBooks, magazines, users, borrowing, purchasing, notifications, categories, and role-based permissions.

This project was upgraded for Project 2 by replacing file persistence with Entity Framework Core, applying the Repository Pattern, using Code First migrations, and keeping the original object-oriented design clean and structured.

## Project Domain

Domain: Library Management System

The application models a real library workflow where users can view items, borrow physical books, buy available items, return borrowed books, and receive notifications. Admins and employees can manage library data based on their permissions.

## Main Features

### Authentication / Current User Selection

- Choose a role from a startup menu: User, Admin, or Employee.
- Enter name and email.
- The system validates that the selected role matches the stored user account.
- The displayed menu changes based on the current user's role.

### Library Item Features

- View all library items.
- Get item by ID.
- Search items by title.
- View available items.
- View items by category.
- View items ordered by title.
- Add new books, eBooks, and magazines.
- Update existing items.
- Delete items.
- Display item category name with item details.

### Category Features

- Add categories.
- Update category names.
- Delete categories if no items are using them.
- View categories ordered by name.

### User Features

- Add users.
- Update user information.
- Delete users.
- View all users.
- Search user by ID.
- Search user by email.
- Prevent users from deleting their own account.
- Prevent duplicate emails.

### Borrowing Features

- Borrow physical books.
- Maximum active borrow limit: 3 books per user.
- Borrow period: 14 days.
- Return borrowed books.
- Calculate late return fine.
- Fine amount: 10 EGP per late day.
- Create borrow records in the database.
- Send notifications after borrowing and returning.

### Buying Features

- Buy available books, eBooks, or magazines.
- Create purchase records in the database.
- Send purchase notifications.
- Roll back item status if purchase record saving fails.

### Notification Features

- Create notifications for borrowing, returning, late fines, and purchases.
- Supports notification channels such as Email and In-App notifications.
- Uses soft delete for notifications.

## User Roles And Permissions

### Admin

- Manage users.
- Add, update, delete, and view library items.
- Add, update, delete, and view categories.
- View report-style information.

### Employee

- Add, update, delete, and view library items.
- Add, update, delete, and view categories.
- Borrow books.
- Buy items.

### Regular User

- View library items.
- Search library items.
- Borrow books.
- Return books.
- Buy items.

## Object-Oriented Programming Concepts

The project demonstrates the required OOP concepts:

- Encapsulation: Models use private setters and behavior methods such as Rename, ChangeEmail, ChangeCategory, MarkAsBorrowed, and MarkAsSold.
- Inheritance: Book, EBook, and Magazine inherit from LibraryItem. Book and EBook also inherit from BookItem.
- Abstraction: LibraryItem and BookItem are abstract base classes.
- Interfaces: Examples include IBorrowable, IBuyable, IRepository<T>, repository interfaces, and service interfaces.
- Polymorphism: Different item types override or implement behavior differently.
- Collections: The system uses collections for query results, navigation properties, and display operations.

## SOLID Principles

- Single Responsibility Principle: Models, repositories, services, controllers, and console printers have separated responsibilities.
- Open / Closed Principle: New item types or services can be added without rewriting the full system.
- Liskov Substitution Principle: Derived library items can be treated as LibraryItem.
- Interface Segregation Principle: Specific interfaces exist for user, category, item, borrowing, purchase, and notification behavior.
- Dependency Inversion Principle: Services depend on interfaces, not concrete repository implementations.

## Architecture

```text
Program / LibraryApp
        |
        v
Managers / Controllers
        |
        v
Services
        |
        v
Repositories
        |
        v
Entity Framework Core / SQL Server
```

### Layers

- Models: Domain entities such as User, LibraryItem, Book, EBook, Magazine, Category, BorrowRecord, PurchaseRecord, and Notification.
- Interfaces: Contracts for services and repositories.
- Repositories: EF Core data access through IRepository<T> and concrete repository classes.
- Services: Business logic, validation, authorization checks, borrowing, buying, notifications, and user/item management.
- Controllers / Managers: Console-facing coordination and printing.
- Data: AppDbContext and SeedData.

## Entity Framework Core

The project uses Entity Framework Core with a Code First approach.

### Database Features

- SQL Server database.
- Code First entity classes.
- EF Core migrations.
- TPH inheritance mapping for library items.
- Related tables with foreign keys.
- Unique email index for users.
- Soft delete query filter for notifications.

### Main Tables

- Users
- LibraryItems
- Categories
- BorrowRecords
- PurchaseRecords
- Notifications

### Relationships

- One category has many library items.
- One user has many borrow records.
- One user has many purchase records.
- One user has many notifications.
- One library item has many borrow records.
- One library item has many purchase records.

## Repository Pattern

The project includes a generic repository:

```csharp
IRepository<T>
GenericRepository<T>
```

It also includes specific repositories:

- IUserRepository
- ILibraryItemRepository
- ICategoryRepository
- IBorrowRecordRepository
- IPurchaseRecordRepository
- INotificationRepository

These repositories separate database access from business logic.

## LINQ Queries

The system uses LINQ with EF Core for database queries, including:

- Search items by title.
- Get available items.
- Get items ordered by title.
- Get items by category.
- Get users by email.
- Get active borrow records.
- Get overdue borrow records.
- Get purchases by user.
- Get notifications by user.

## Console UI

The console UI includes:

- Boxed menus.
- Role-based menu options.
- Clean output formatting.
- Separate console printer classes:
  - ConsolePrinter
  - UserConsolePrinter
  - LibraryItemConsolePrinter
  - CategoryConsolePrinter

The services do not print directly. They return data or success messages, and the controllers print the output.

## How To Run

1. Clone the repository.

```bash
git clone <repository-url>
```

2. Open the project in Visual Studio.

3. Check the SQL Server connection string in AppDbContext.

Example:

```csharp
Server=.;Database=LibraryManagementDb;Trusted_Connection=True;TrustServerCertificate=True;
```

4. Restore NuGet packages.

```bash
dotnet restore
```

5. Apply migrations.

```bash
dotnet ef database update
```

Or from Visual Studio Package Manager Console:

```powershell
Update-Database
```

6. Run the console application.

```bash
dotnet run
```

## Seeded Users

The project includes seed data for testing. Use one of the seeded users from SeedData when the app asks for role, name, and email.

Example seeded roles may include:

- Admin
- User
- Employee

## Project 2 Requirements Covered

- C# console application.
- Database-driven application.
- Entity Framework Core.
- Code First approach.
- EF Core migrations.
- LINQ queries.
- Repository Pattern.
- IRepository<T> interface.
- Concrete repository classes.
- Related database tables.
- Full CRUD operations.
- OOP concepts.
- SOLID principles.
- Exception handling.
- Role-based menu.
- GitHub-ready structure.

## Team Members

- Team member 1: Mohamed Ahmed.
- Team member 2: Gihad Elrwiny.

## Presentation Drive Link
https://drive.google.com/drive/folders/1YHfuwJfr0EQKbbA6St7l0qAiP32Awx9h?usp=sharing

## Notes

This project is designed for a live demo. The menu is role-based, so available options depend on the selected current user.
