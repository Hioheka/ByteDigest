# ByteDigest

ByteDigest is a modern blog platform designed for publishing software and technology content.

## Technology Stack

### Backend
- **Framework**: ASP.NET Core Web API (.NET 8)
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT Bearer Tokens
- **Documentation**: Swagger/OpenAPI

### Frontend
- **Framework**: Angular (latest LTS)
- **Language**: TypeScript
- **UI Language**: Turkish

## Architecture

### Backend Layers
1. **ByteDigest.Api** - Controllers and API endpoints
2. **ByteDigest.Application** - Services, DTOs, and business logic
3. **ByteDigest.Domain** - Entities and core domain logic
4. **ByteDigest.Infrastructure** - EF Core, DbContext, and repository implementations

### Database Entities
- **Post** - Blog posts with categorization and tagging
- **Page** - Static pages
- **User** - Authentication and authorization (Admin/User roles)

## Features

### Public Features
- Browse published posts with pagination
- Search and filter posts by category
- View post details
- Access static pages

### Admin Features
- Secure authentication
- Create, edit, publish, and delete posts
- Manage static pages
- Role-based access control

## Getting Started

### Backend Setup
1. Navigate to `src/Backend/ByteDigest.Api`
2. Update connection string in `appsettings.json`
3. Run migrations: `dotnet ef database update`
4. Run the API: `dotnet run`
5. Access Swagger at: `https://localhost:5001/swagger`

### Frontend Setup
1. Navigate to `src/Frontend/ByteDigest.Web`
2. Install dependencies: `npm install`
3. Update API URL in `environment.ts`
4. Run development server: `ng serve`
5. Access at: `http://localhost:4200`

## Default Credentials
- **Username**: admin@bytedigest.com
- **Password**: Admin123!

## License
MIT
