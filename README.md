# Authentication API

This project is a **.NET 8** Web API that provides user registration and JWT authentication. It stores user data in **PostgreSQL**, publishes user-related events to **RabbitMQ**, and uses a background worker to dispatch account verification emails.

## Features
- JWT-based user authentication
- Registration endpoint that enqueues verification events
- Worker service consuming RabbitMQ messages to send verification emails
- Exception-handling middleware
- Layered architecture (`application`, `domain`, `infrastructure`)

## Requirements
- .NET 8 SDK
- PostgreSQL database
- RabbitMQ server

## Getting Started
1. Restore dependencies:
   ```bash
   dotnet restore
   ```
2. Update `appsettings.json` with your PostgreSQL and RabbitMQ configuration.
3. Run the API:
   ```bash
   dotnet run
   ```

## Endpoints
| Method | Endpoint | Description |
| ------ | -------- | ----------- |
| `POST` | `/api/User/Register` | Register a new user and publish a verification message |
| `POST` | `/api/User/Login` | Authenticate a user and return a JWT token |
| `GET`  | `/api/User/healthCheck` | Verify the service is up |
| `GET`  | `/api/check/{userId}` | Confirm a user account via verification link |

## Background Worker
A separate worker service subscribes to the RabbitMQ queue. When a registration event arrives, the worker sends a verification email containing a link to `GET /api/check/{userId}` to activate the account.

## Build
```bash
dotnet build
```

## Project Structure
```
application/    DTOs, services, controllers and mappers
domain/         Entities, contracts and domain exceptions
infrastructure/ Data context, repositories and middleware
```
