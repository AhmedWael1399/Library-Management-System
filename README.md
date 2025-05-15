# 📚 Library Management System

A complete Library Management System built with **ASP.NET Core 8.0 MVC**, following an **N-Tier Architecture** and using **Entity Framework Core** and **SQL Server**.

---

## 📖 Description

This system allows administrators to manage authors, books, and borrowing transactions with a clean interface and layered design. It supports:

- Author creation, editing, and deletion
- Book management (genre, status, description)
- Borrowing and returning books
- Availability check and real-time status display
- Pagination, filtering, and client-side validation

---

## 🚀 Features

- 🔍 Search & filter authors and transactions
- 📄 Paginated lists for authors, books, and transactions
- ✅ Full CRUD operations with validation
- 🔁 Borrowing history and availability tracking
- 🧩 Partial views for reusable UI components
- ⚡ Real-time availability check via AJAX

---

## 🧱 Architecture

This project follows the **N-Tier Architecture**:

Presentation (ASP.NET Core MVC)
│
├── Business Logic Layer (Services, Interfaces)
│
├── Data Access Layer (Repositories, EF DbContext)
│
└── Database (SQL Server with EF Core Migrations)



- **ViewModels** are used to separate UI validation and logic
- **DTOs** are used between BLL and DAL for clean mapping
- **Repositories** isolate database logic using EF Core
- **Services** provide reusable business logic

---

## ⚙️ Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server (local or remote)
- Bootstrap 5 (for styling)
- jQuery + AJAX (for dynamic features)
- C#, LINQ, Razor Pages

---

## 🔧 Setup Instructions

> Make sure you have [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) and SQL Server installed

1. **Clone the repository**
   ```bash
   git clone https://github.com/AhmedWael1399/LibraryManagementSystem.git
   cd LibraryManagementSystem
   
2. Update the connection string
In appsettings.json, set your SQL Server connection string under:

"ConnectionStrings": {
  "DefaultConnection": "Your_SQL_Server_Connection_String"
}


3. Apply migrations & seed the database
    
   dotnet ef database update

4. Run the app

    dotnet run

6. Access it in your browser

  https://localhost:5001

## 📁 Folder Structure

LibraryManagementSystem/
│
├── Controllers/         → MVC Controllers
├── Views/               → Razor Views and Partials
├── ViewModels/          → UI-specific models
├── BLL/                 → Services, Interfaces, DTOs
├── DAL/                 → EF Models, DbContext, Repositories
├── wwwroot/             → Static assets (CSS, JS)
└── appsettings.json     → Configuration (connection string etc.)

## 👤 Author

Made by Ahmed Wael
Feel free to reach out or open issues to contribute!
