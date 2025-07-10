# Professional Management System

## Overview
The Professional Management System is a comprehensive web application built with ASP.NET Core MVC that provides a complete enterprise-level management platform. It features automated database creation, modular architecture, and both frontend and backend functionality for managing users, roles, permissions, and system modules.

## 🚀 Key Highlights
- **Automatic Database Creation**: Database and tables are created automatically on first run
- **Seeded Data**: Comes with pre-populated modules, permissions, and admin user
- **Modular Architecture**: Organized with modules and sub-modules for scalability
- **Role-Based Access Control**: Complete RBAC implementation
- **Professional UI**: Modern, responsive design with FontAwesome icons
- **Enterprise Ready**: Built with enterprise-level patterns and practices

## Features

- **User Management**: Create, update, delete, and view users. Assign roles to users and manage their permissions.
- **Role Management**: Define roles within the system that users can be assigned to.
- **Permission Management**: Create and assign permissions to roles. Manage permissions that govern access to different parts of the system.
- **Module Management**: Define and configure system-level modules and sub-modules, providing a hierarchical structure for functionality.
- **Dashboard**: A professional, user-friendly dashboard that provides a summary and quick actions to manage the system effectively.

## Technologies Used
- **ASP.NET Core MVC**: For developing a robust web application with Model-View-Controller architecture.
- **Entity Framework Core**: For object-relational mapping and database operations. Uses SQL Server as the database provider.
- **BCrypt.Net**: For secure password hashing.
- **Bootstrap**: For designing responsive and attractive UI components.
- **FontAwesome**: For vector icons and social logos.

## Getting Started

### Prerequisites
Ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation
1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-repo/ProManagementSystem.git
   cd ProManagementSystem
   ```

2. **Setup the database**:
   Update the `appsettings.json` file with your SQL Server connection details.

3. **Build and Run the application**:
   ```bash
   dotnet build
   dotnet run
   ```

4. **Access the application**:
   Open a browser and go to [http://localhost:5136](http://localhost:5136)

## 📚 Project Structure
```
ProManagementSystem/
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs  # Dashboard controller
│   ├── ModulesController.cs
│   ├── UsersController.cs
│   ├── RolesController.cs
│   └── PermissionsController.cs
├── Data/                  # Database context
│   └── ApplicationDbContext.cs
├── Models/               # Entity models
│   └── Entities/
│       ├── User.cs
│       ├── Role.cs
│       ├── Permission.cs
│       ├── Module.cs
│       └── SubModule.cs
├── Services/             # Business logic
│   ├── Interfaces/
│   └── Implementations/
├── Views/                # Razor views
│   ├── Home/
│   ├── Modules/
│   ├── Users/
│   ├── Roles/
│   └── Permissions/
└── wwwroot/             # Static files
```

## 🔑 Default Credentials
- **Email**: admin@promanagementsystem.com
- **Password**: Admin123!

## 📊 Database Schema
The system automatically creates the following tables:
- **Users**: User information and authentication
- **Roles**: System roles (Administrator, Manager, User)
- **Permissions**: Granular permissions for each sub-module
- **Modules**: Main system modules (User Management, Module Management, Reports, Settings)
- **SubModules**: Sub-functionalities within each module
- **UserRoles**: Many-to-many relationship between users and roles
- **RolePermissions**: Many-to-many relationship between roles and permissions

## 🚀 Features in Detail

### Dashboard
- Modern card-based layout
- Quick stats overview
- Direct access to all modules and sub-modules
- Responsive design for all devices

### User Management
- Create, edit, delete users
- Assign multiple roles to users
- Secure password hashing with BCrypt
- User status management (Active/Inactive)

### Role Management
- Define custom roles
- Assign permissions to roles
- Role-based access control
- Hierarchical permission structure

### Module Management
- Configure system modules
- Manage sub-modules
- Icon-based navigation
- Sort order management

## 🔧 API Endpoints
The system provides full CRUD operations through MVC controllers:

- **GET** `/` - Dashboard
- **GET** `/Users` - User listing
- **POST** `/Users/Create` - Create user
- **GET** `/Roles` - Role listing
- **POST** `/Roles/Create` - Create role
- **GET** `/Modules` - Module listing
- **POST** `/Modules/Create` - Create module
- **GET** `/Permissions` - Permission listing

## Contributing
Contributions are welcome! Please feel free to submit a pull request. For major changes, please open an issue first to discuss what you would like to change.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support
For support, email support@promanagementsystem.com or create an issue in the repository.

