# ASP.NET Core 8 Online Store

A fully-featured **ASP.NET Core 8** e-commerce application with a clean architecture, robust features, and ready for both web dashboard and mobile app integration.  

The project includes repositories, services, controllers, DTOs, view models, middleware for error handling & logging, background tasks, payments, social logins, SignalR notifications, localization, Razor dashboard, and API for mobile.

---

## Table of Contents

- [Architecture](#architecture)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Folder Structure](#folder-structure)
- [Setup Instructions](#setup-instructions)
- [API Documentation](#api-documentation)
- [Security](#security)
- [Localization](#localization)
- [Unit Testing](#unit-testing)
- [Background Tasks & Queues](#background-tasks--queues)
- [Real-time Features](#real-time-features)
- [Contributing](#contributing)
- [License](#license)

---

## Architecture

The project follows a **layered clean architecture**:

Repository -> Service -> Controller -> DTO/ViewModel -> Razor View / API Response

- **Repository Layer:** Handles EF Core database access.
- **Service Layer:** Implements business logic and validation.
- **Controller Layer:** Handles HTTP requests, responses, and API endpoints.
- **DTOs & ViewModels:** Data transfer and UI binding.
- **Middleware:** Centralized error handling, logging, and exception capturing.

---

## Features

### Core Features

- Product catalog with categories, tags, variants, and translations
- Orders and order management
- Checkout, cart, and order tracking
- Stripe payment integration
- User management with **roles and permissions**
- Razor views for admin dashboard
- API for mobile app consumption

### Security

- **JWT authentication** for APIs
- **CORS** configured
- **Content Security Policy (CSP)** headers
- Input validation and sanitization
- Security headers

### Logging & Error Handling

- Middleware captures all exceptions
- Exception logs saved to database
- Multilingual exception messages (if needed)

### Background Tasks & Queues

- Email sending
- Notifications
- Scheduled tasks
- Queue-based background processing

### Real-time Features

- **SignalR** for live notifications
- Order and ticket updates in real-time

### Social Login

- Login with **Google** and **Facebook**

### Localization

- Supports **2 languages** (English & Arabic)
- Localized DTOs, view models, and messages

### Validation

- Fluent validation for DTOs and view models
- Client-side and server-side validation

---

## Technologies Used

- ASP.NET Core 8
- Entity Framework Core
- Razor Pages / Views
- SignalR
- Stripe.NET
- xUnit / NUnit & Moq (Unit Testing)
- SMTP Email Integration
- JWT Authentication
- Localization (`IStringLocalizer`)
- Background services and queues

---

## Folder Structure

/Controllers -> API and MVC controllers
/Repositories -> Database access logic
/Services -> Business logic
/Models -> Entities, Enums, DTOs, ViewModels
/Middleware -> Error handling, logging, security
/Views -> Razor views for admin dashboard
/BackgroundTasks -> Queues, scheduled tasks, email jobs
/wwwroot -> Static files


## Setup Instructions

1. Clone the repository:
```bash
git clone https://github.com/yourusername/online-store.git
```
2. Configure appsettings.json:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourDatabaseConnectionString"
  },
  "Stripe": {
    "SecretKey": "sk_test_...",
    "PublishableKey": "pk_test_..."
  },
  "JWT": {
    "Secret": "YourJWTSecretKey",
    "Issuer": "YourIssuer",
    "Audience": "YourAudience"
  },
  "SMTP": {
    "Host": "smtp.example.com",
    "Port": 587,
    "Username": "user@example.com",
    "Password": "password"
  }
}
```
3. Apply EF Core migrations:

```bash
dotnet ef database update
```
4. Run the project:

```bash
dotnet run
```
# API Documentation

Auth: /api/auth (Login, Register, Social Login)
Products: /api/products (Get all, search, filter)
Orders: /api/orders (Place, track, history)
Notifications: /api/notifications (Real-time via SignalR)
Support Tickets: /api/support (Create, update, reply)
Full Postman collection available in docs/PostmanCollection.json