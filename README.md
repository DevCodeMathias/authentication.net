# Authentication API

This repository contains a **.NET 8** Web API for user authentication. It issues JWT tokens, stores data in PostgreSQL using Entity Framework Core, and publishes user events through RabbitMQ.

## Features
- JWT-based authentication
- User registration and login
- RabbitMQ integration for user events
- Worker service consuming the queue to send registration confirmation emails
- Exception handling middleware
- Layered architecture (`application`, `domain`, `infrastructure`)

## Requirements
- .NET 8 SDK
- PostgreSQL database
- RabbitMQ server

## Getting started
1. Restore dependencies:
   ```bash
   dotnet restore
   ```
2. Update `appsettings.json` with your connection string and RabbitMQ settings.
3. Run the API:
   ```bash
   dotnet run
   ```

## Build
```bash
dotnet build
```
