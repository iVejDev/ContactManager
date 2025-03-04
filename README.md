# 📇 ContactManager

![.NET Core](https://img.shields.io/badge/.NET%20Core-7.0-512BD4?style=flat-square&logo=dotnet)
![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=flat-square&logo=blazor)
![License](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)

A modern and intuitive contact management application built with Blazor that helps you organize and manage your contacts efficiently.

![ContactManager Screenshot](https://via.placeholder.com/800x450?text=ContactManager+Screenshot)

## ✨ Key Features

### 🌟 User Interface
- **Intuitive Blazor GUI** - Clean, responsive design for easy navigation
- **Dark/Light Theme** - Toggle between dark and light modes for comfortable viewing
- **Detailed Contact Views** - See all your contact information at a glance

### 📱 Contact Management
- **Comprehensive Contact Details** - Store names, phone numbers, emails, and more
- **Quick Search** - Find contacts instantly as you type
- **Easy Editing** - Update or remove contacts with just a few clicks
- **Persistent Storage** - All changes are automatically saved to JSON

### 🛠️ Technical Excellence
- **SOLID Architecture** - Built following best practices in software design
- **Multiple Design Patterns** - Leverages Service Pattern and Factory Pattern
- **Dependency Injection** - Modular components for better maintainability
- **Comprehensive Testing** - Unit tests ensure reliability and stability

## 🔧 Technology Stack

### Frontend
- **Blazor WebAssembly** - Rich interactive UI running in the browser
- **CSS/Bootstrap** - Responsive design that works on all devices
- **JavaScript Interop** - For enhanced browser interactions

### Backend
- **C# / .NET Core** - Robust and modern programming framework
- **JSON Data Storage** - Lightweight persistence without database requirements
- **Service-based Architecture** - Clean separation of concerns

## 📋 Project Structure

```
ContactManager/
├── ContactManager.UI/          # Blazor WebAssembly project
│   ├── Components/             # Reusable UI components
│   ├── Pages/                  # Application pages
│   ├── Services/               # Business logic and data services
│   └── wwwroot/                # Static web assets
│
├── ContactManager.Core/        # Core business logic
│   ├── Models/                 # Domain models
│   ├── Interfaces/             # Service contracts
│   └── Services/               # Service implementations
│
├── ContactManager.Console/     # Console application version
│
└── ContactManager.Tests/       # Unit and integration tests
```

## 🚀 Getting Started

### Prerequisites
- **.NET 7.0 SDK** or later
- **Visual Studio 2022** or compatible IDE
- A modern web browser

### Installation Steps

#### Option 1: Using the Blazor Application
1. **Clone the repository**
   ```
   git clone https://github.com/iVejDev/ContactManager.git
   cd ContactManager
   ```

2. **Restore dependencies**
   ```
   dotnet restore
   ```

3. **Run the application**
   ```
   cd ContactManager.UI
   dotnet run
   ```

4. **Access the application**
   
   Open your browser and navigate to `https://localhost:5001`

#### Option 2: Using the Console Application
1. **Navigate to the console project**
   ```
   cd ContactManager.Console
   ```

2. **Run the application**
   ```
   dotnet run
   ```

## 🎯 Usage Guide

### Adding a New Contact
1. Click the "Add Contact" button in the navigation bar
2. Fill in the contact details form
3. Click "Save" to add the contact to your list

### Viewing Contacts
- All contacts are displayed on the home page
- Click on any contact to view detailed information

### Editing a Contact
1. Navigate to contact details
2. Click the "Edit" button
3. Update the information as needed
4. Click "Save" to confirm changes

### Deleting a Contact
1. Navigate to contact details
2. Click the "Delete" button
3. Confirm the deletion in the dialog

### Toggling Dark Mode
- Click the theme toggle button in the top-right corner of the application

## 🧪 Testing

The project includes comprehensive unit tests to ensure functionality and reliability:

```
dotnet test
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🔍 Implementation Notes

- **SOLID Principles** - The application follows Single Responsibility, Open-Closed, Liskov Substitution, Interface Segregation, and Dependency Inversion principles
- **Service Pattern** - Services are used to encapsulate business logic and data access
- **Factory Pattern** - Used for creating different types of contacts and service instances

## 🙏 Acknowledgements

- This project was built as a learning exercise in modern C# and Blazor development
- Parts of the project were created with AI assistance (documented in code comments)

---

&copy; 2025 Your Name. All rights reserved.
