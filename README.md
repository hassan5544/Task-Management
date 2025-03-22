# Task Management System

## Overview
The Task Management System is a web-based application built with ASP.NET Core using Clean Architecture principles. It provides task tracking, notifications, and user authentication with JWT. The system supports PostgreSQL as the database and integrates background jobs using Hangfire or Quartz.

## Features
- **Task Management:** Create, update, delete, and assign tasks.
- **User Authentication:** JWT-based authentication and role-based access control.
- **Notifications:** Background job processing for sending notifications.
- **Dockerized Deployment:** Fully containerized using Docker.


## Technologies Used
- **Backend:** ASP.NET Core, EF Core, MediatR, Rich Domain Model
- **Database:** PostgreSQL
- **Authentication:** JWT
- **Background Jobs:** Hangfire / Quartz
- **Containerization:** Docker

## System Architecture
The system follows Clean Architecture principles:
1. **Domain Layer:** Contains core business logic.
2. **Application Layer:** Includes CQRS with MediatR.
3. **Infrastructure Layer:** Handles database access (EF Core), authentication, and external services.
4. **Presentation Layer:** API endpoints for interaction.

## Installation & Setup
### Prerequisites
- .NET SDK 9.0
- PostgreSQL
- Docker (for containerized deployment)

### Running the System
#### Locally
1. Clone the repository:
   ```sh
   git clone https://github.com/hassan5544/task-management.git
   cd task-management
   ```
2. Configure the database connection in `appsettings.json`.
3. Apply migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the application:
   ```sh
   dotnet run
   ```

#### Using Docker
1. Build and run the Docker container:
   ```sh
   docker-compose up --build
   ```
2. The API will be accessible at `http://localhost:5000`.

## API Endpoints
### Authentication
- **POST** `/api/auth/login` - Login with email and password
- **POST** `/api/auth/register` - Register a new user

### Tasks
- **GET** `/api/tasks` - Get all tasks
- **POST** `/api/tasks` - Create a new task
- **PUT** `/api/tasks/{id}` - Update a task
- **DELETE** `/api/tasks/{id}` - Delete a task

## Background Jobs
- Uses Hangfire/Quartz to send task reminders and notifications.


