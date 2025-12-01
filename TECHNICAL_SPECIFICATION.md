# ByteDigest - Technical Specification

## Table of Contents
1. [Overview](#overview)
2. [Technology Stack](#technology-stack)
3. [Backend Architecture](#backend-architecture)
4. [Frontend Architecture](#frontend-architecture)
5. [Database Schema](#database-schema)
6. [API Endpoints](#api-endpoints)
7. [Authentication & Authorization](#authentication--authorization)
8. [Project Structure](#project-structure)
9. [Setup Instructions](#setup-instructions)

---

## Overview

ByteDigest is a modern blog platform designed for publishing software and technology content. The platform features role-based access control with Admin and User roles, where administrators can manage all content while regular users and anonymous visitors can only read published posts and pages.

### Key Features
- JWT-based authentication
- Role-based authorization (Admin/User)
- CRUD operations for posts and pages
- Pagination and search functionality
- Responsive Angular frontend
- RESTful API with Swagger documentation
- Turkish language UI

---

## Technology Stack

### Backend
- **Framework**: ASP.NET Core Web API (.NET 8)
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core 8.0
- **Authentication**: JWT Bearer Tokens
- **Documentation**: Swagger/OpenAPI (Swashbuckle)
- **Identity**: ASP.NET Core Identity

### Frontend
- **Framework**: Angular 18 (LTS)
- **Language**: TypeScript 5.4
- **HTTP Client**: Angular HttpClient
- **Routing**: Angular Router
- **State Management**: RxJS with BehaviorSubject

---

## Backend Architecture

The backend follows a clean, layered architecture pattern:

### 1. Domain Layer (ByteDigest.Domain)
**Purpose**: Contains core business entities and repository interfaces.

**Entities**:
- `Post`: Blog post with slug, title, excerpt, content, category, tags, publish status
- `Page`: Static page with slug, title, content, publish status
- `ApplicationUser`: User entity extending IdentityUser

**Interfaces**:
- `IRepository<T>`: Generic repository pattern
- `IPostRepository`: Post-specific repository operations
- `IPageRepository`: Page-specific repository operations

**Code Style**:
```csharp
/// <summary>
/// Represents a blog post entity.
/// </summary>
public class Post
{
    /// <summary>
    /// Gets or sets the unique identifier for the post.
    /// </summary>
    public int Id { get; set; }
    
    // Note: No var keyword, XML summaries on all members, no inline comments
}
```

### 2. Application Layer (ByteDigest.Application)
**Purpose**: Contains business logic, DTOs, and service interfaces.

**DTOs**:
- `LoginRequestDto`, `LoginResponseDto`: Authentication
- `PostDto`, `CreatePostDto`, `UpdatePostDto`: Post operations
- `PageDto`, `CreatePageDto`, `UpdatePageDto`: Page operations
- `PaginatedResultDto<T>`: Generic pagination wrapper

**Services**:
- `AuthService`: JWT token generation and user authentication
- `PostService`: Post CRUD operations and business logic
- `PageService`: Page CRUD operations and business logic

**Key Service Example**:
```csharp
/// <summary>
/// Implements post-related business logic and operations.
/// </summary>
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    /// <summary>
    /// Gets a paginated list of published posts with optional filtering.
    /// </summary>
    public async Task<PaginatedResultDto<PostDto>> GetPublishedPostsAsync(
        int page, 
        int pageSize, 
        string? search = null, 
        string? category = null)
    {
        // Implementation...
    }
}
```

### 3. Infrastructure Layer (ByteDigest.Infrastructure)
**Purpose**: Implements data access and external concerns.

**Components**:
- `ApplicationDbContext`: EF Core DbContext with Identity integration
- `Repository<T>`: Generic repository implementation
- `PostRepository`: Post-specific repository
- `PageRepository`: Page-specific repository
- `DbSeeder`: Database initialization and seed data

**DbContext Configuration**:
```csharp
/// <summary>
/// Represents the application database context.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Page> Pages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => p.Slug).IsUnique();
            entity.Property(p => p.Slug).IsRequired().HasMaxLength(200);
            // Additional configuration...
        });
    }
}
```

### 4. API Layer (ByteDigest.Api)
**Purpose**: Exposes HTTP endpoints and handles requests.

**Controllers**:
- `AuthController`: Login endpoint (AllowAnonymous)
- `PostsController`: Public post endpoints (AllowAnonymous)
- `PagesController`: Public page endpoints (AllowAnonymous)
- `AdminPostsController`: Admin post management (Authorize: Admin)
- `AdminPagesController`: Admin page management (Authorize: Admin)

**Program.cs Configuration**:
- SQL Server connection
- Identity configuration
- JWT authentication
- Swagger with JWT bearer support
- CORS for Angular app
- Database seeding on startup

---

## Frontend Architecture

The Angular frontend follows a modular architecture with lazy-loaded routes.

### Module Structure

#### 1. Core Module
**Location**: `src/app/core/`

**Services**:
- `AuthService`: Authentication, token management, user state
- `PostService`: Post HTTP operations
- `PageService`: Page HTTP operations

**Models**:
- `auth.model.ts`: LoginRequest, LoginResponse, User
- `post.model.ts`: Post, CreatePost, UpdatePost, PaginatedResult
- `page.model.ts`: Page, CreatePage, UpdatePage

**AuthService Example**:
```typescript
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<User | null>;
  public currentUser: Observable<User | null>;

  login(userNameOrEmail: string, password: string): Observable<LoginResponse> {
    // Stores JWT in localStorage
    // Updates currentUser BehaviorSubject
  }

  logout(): void {
    // Clears localStorage and user state
  }

  isAuthenticated(): boolean { }
  isAdmin(): boolean { }
  getToken(): string | null { }
}
```

#### 2. Auth Module
**Location**: `src/app/auth/`

**Components**:
- `LoginComponent`: Turkish login form with form validation

**Features**:
- Form validation
- Error handling
- Redirect to returnUrl after login
- Admin users redirected to /admin

#### 3. Public Module
**Location**: `src/app/public/`

**Components**:
- `HomeComponent`: Post listing with search, category filter, pagination
- `PostDetailComponent`: Full post display with HTML content
- `PageDetailComponent`: Static page display

**Features**:
- Responsive grid layout
- Real-time search
- Category filtering
- Pagination controls
- Turkish UI text

#### 4. Admin Module
**Location**: `src/app/admin/`

**Components**:
- `AdminDashboardComponent`: Layout with navigation
- `AdminHomeComponent`: Dashboard home
- `AdminPostsComponent`: Post management table
- `AdminPagesComponent`: Page management table

**Features**:
- Protected by `adminGuard`
- CRUD operations
- Status badges (Published/Draft)
- Confirmation dialogs for delete
- Turkish UI text

#### 5. Shared Module
**Location**: `src/app/shared/`

**Guards**:
- `authGuard`: Requires authentication
- `adminGuard`: Requires Admin role

**Interceptors**:
- `jwtInterceptor`: Attaches JWT to requests

### Routing Structure

```typescript
export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./public/public.routes')
  },
  {
    path: 'giris',
    loadChildren: () => import('./auth/auth.routes')
  },
  {
    path: 'admin',
    canActivate: [adminGuard],
    loadChildren: () => import('./admin/admin.routes')
  }
];
```

**Public Routes**:
- `/` - Home (post listing)
- `/yazi/:slug` - Post detail
- `/sayfa/:slug` - Page detail

**Admin Routes**:
- `/admin` - Dashboard home
- `/admin/yazilar` - Posts management
- `/admin/sayfalar` - Pages management

---

## Database Schema

### Tables

#### Posts
```sql
CREATE TABLE Posts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Slug NVARCHAR(200) NOT NULL UNIQUE,
    Title NVARCHAR(500) NOT NULL,
    Excerpt NVARCHAR(1000),
    Content NVARCHAR(MAX) NOT NULL,
    Category NVARCHAR(100),
    Tags NVARCHAR(500),
    IsPublished BIT NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NULL
);

CREATE UNIQUE INDEX IX_Posts_Slug ON Posts(Slug);
```

#### Pages
```sql
CREATE TABLE Pages (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Slug NVARCHAR(200) NOT NULL UNIQUE,
    Title NVARCHAR(500) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    IsPublished BIT NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NULL
);

CREATE UNIQUE INDEX IX_Pages_Slug ON Pages(Slug);
```

#### AspNetUsers (Identity)
- Standard ASP.NET Core Identity tables
- Extended with `FullName` and `CreatedAt` fields

### Seed Data
Default admin user created on startup:
- Email: admin@bytedigest.com
- Username: admin
- Password: Admin123!
- Role: Admin

---

## API Endpoints

### Authentication

#### POST /api/auth/login
**Access**: AllowAnonymous

**Request**:
```json
{
  "userNameOrEmail": "admin",
  "password": "Admin123!"
}
```

**Response**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "userName": "admin",
  "email": "admin@bytedigest.com",
  "role": "Admin"
}
```

### Public Posts

#### GET /api/posts
**Access**: AllowAnonymous

**Query Parameters**:
- `page` (int, default: 1)
- `pageSize` (int, default: 10)
- `search` (string, optional)
- `category` (string, optional)

**Response**:
```json
{
  "items": [
    {
      "id": 1,
      "slug": "dotnet-8-yenilikleri",
      "title": ".NET 8 Yenilikleri",
      "excerpt": "...",
      "content": "...",
      "category": "Yazılım",
      "tags": "dotnet,csharp",
      "isPublished": true,
      "createdAt": "2024-01-15T10:00:00Z",
      "updatedAt": null
    }
  ],
  "totalCount": 25,
  "page": 1,
  "pageSize": 10,
  "totalPages": 3,
  "hasPreviousPage": false,
  "hasNextPage": true
}
```

#### GET /api/posts/{slug}
**Access**: AllowAnonymous

**Response**: Single Post object or 404

### Public Pages

#### GET /api/pages/{slug}
**Access**: AllowAnonymous

**Response**: Single Page object or 404

### Admin Posts

#### GET /api/admin/posts
**Access**: Authorize(Roles = "Admin")
**Response**: Array of all posts (published and unpublished)

#### GET /api/admin/posts/{id}
**Access**: Authorize(Roles = "Admin")
**Response**: Single post by ID

#### POST /api/admin/posts
**Access**: Authorize(Roles = "Admin")

**Request**:
```json
{
  "slug": "angular-18-features",
  "title": "Angular 18 Özellikleri",
  "excerpt": "Kısa açıklama",
  "content": "<p>HTML içerik</p>",
  "category": "Web",
  "tags": "angular,typescript",
  "isPublished": true
}
```

**Response**: Created post with 201 status

#### PUT /api/admin/posts/{id}
**Access**: Authorize(Roles = "Admin")
**Request**: UpdatePostDto
**Response**: Updated post or 404

#### DELETE /api/admin/posts/{id}
**Access**: Authorize(Roles = "Admin")
**Response**: 204 No Content or 404

### Admin Pages

Similar structure to Admin Posts:
- GET /api/admin/pages
- GET /api/admin/pages/{id}
- POST /api/admin/pages
- PUT /api/admin/pages/{id}
- DELETE /api/admin/pages/{id}

---

## Authentication & Authorization

### JWT Configuration

**appsettings.json**:
```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyForJwtTokenGenerationMinimum32Characters",
    "Issuer": "ByteDigest",
    "Audience": "ByteDigestUsers"
  }
}
```

**Token Payload**:
- NameIdentifier: User ID
- Name: Username
- Email: User email
- Role: User role (Admin/User)
- Expiration: 7 days

### Authorization Levels

1. **AllowAnonymous**:
   - Login endpoint
   - Public posts (GET)
   - Public pages (GET)

2. **Authorize(Roles = "Admin")**:
   - All admin endpoints
   - POST, PUT, DELETE operations

### Frontend Protection

**JWT Interceptor**: Automatically attaches token to all HTTP requests

**Route Guards**:
- `authGuard`: Checks authentication, redirects to /giris
- `adminGuard`: Checks Admin role, redirects to /

---

## Project Structure

```
ByteDigest/
├── src/
│   ├── Backend/
│   │   ├── ByteDigest.sln
│   │   ├── ByteDigest.Domain/
│   │   │   ├── Entities/
│   │   │   │   ├── Post.cs
│   │   │   │   ├── Page.cs
│   │   │   │   └── ApplicationUser.cs
│   │   │   └── Interfaces/
│   │   │       ├── IRepository.cs
│   │   │       ├── IPostRepository.cs
│   │   │       └── IPageRepository.cs
│   │   ├── ByteDigest.Application/
│   │   │   ├── DTOs/
│   │   │   │   ├── LoginRequestDto.cs
│   │   │   │   ├── LoginResponseDto.cs
│   │   │   │   ├── PostDto.cs
│   │   │   │   ├── CreatePostDto.cs
│   │   │   │   ├── UpdatePostDto.cs
│   │   │   │   ├── PageDto.cs
│   │   │   │   ├── CreatePageDto.cs
│   │   │   │   ├── UpdatePageDto.cs
│   │   │   │   └── PaginatedResultDto.cs
│   │   │   ├── Interfaces/
│   │   │   │   ├── IAuthService.cs
│   │   │   │   ├── IPostService.cs
│   │   │   │   └── IPageService.cs
│   │   │   └── Services/
│   │   │       ├── AuthService.cs
│   │   │       ├── PostService.cs
│   │   │       └── PageService.cs
│   │   ├── ByteDigest.Infrastructure/
│   │   │   ├── Data/
│   │   │   │   ├── ApplicationDbContext.cs
│   │   │   │   └── DbSeeder.cs
│   │   │   └── Repositories/
│   │   │       ├── Repository.cs
│   │   │       ├── PostRepository.cs
│   │   │       └── PageRepository.cs
│   │   └── ByteDigest.Api/
│   │       ├── Controllers/
│   │       │   ├── AuthController.cs
│   │       │   ├── PostsController.cs
│   │       │   ├── PagesController.cs
│   │       │   ├── AdminPostsController.cs
│   │       │   └── AdminPagesController.cs
│   │       ├── Properties/
│   │       │   └── launchSettings.json
│   │       ├── Program.cs
│   │       ├── appsettings.json
│   │       └── appsettings.Development.json
│   └── Frontend/
│       └── ByteDigest.Web/
│           ├── src/
│           │   ├── app/
│           │   │   ├── core/
│           │   │   │   ├── models/
│           │   │   │   │   ├── auth.model.ts
│           │   │   │   │   ├── post.model.ts
│           │   │   │   │   └── page.model.ts
│           │   │   │   └── services/
│           │   │   │       ├── auth.service.ts
│           │   │   │       ├── post.service.ts
│           │   │   │       └── page.service.ts
│           │   │   ├── shared/
│           │   │   │   ├── guards/
│           │   │   │   │   └── auth.guard.ts
│           │   │   │   └── interceptors/
│           │   │   │       └── jwt.interceptor.ts
│           │   │   ├── auth/
│           │   │   │   ├── components/
│           │   │   │   │   └── login.component.ts
│           │   │   │   └── auth.routes.ts
│           │   │   ├── public/
│           │   │   │   ├── components/
│           │   │   │   │   ├── home.component.ts
│           │   │   │   │   ├── post-detail.component.ts
│           │   │   │   │   └── page-detail.component.ts
│           │   │   │   └── public.routes.ts
│           │   │   ├── admin/
│           │   │   │   ├── components/
│           │   │   │   │   ├── admin-dashboard.component.ts
│           │   │   │   │   ├── admin-home.component.ts
│           │   │   │   │   ├── admin-posts.component.ts
│           │   │   │   │   └── admin-pages.component.ts
│           │   │   │   └── admin.routes.ts
│           │   │   ├── app.component.ts
│           │   │   ├── app.config.ts
│           │   │   └── app.routes.ts
│           │   ├── environments/
│           │   │   ├── environment.ts
│           │   │   └── environment.prod.ts
│           │   ├── index.html
│           │   ├── main.ts
│           │   └── styles.css
│           ├── angular.json
│           ├── package.json
│           ├── tsconfig.json
│           └── tsconfig.app.json
├── README.md
└── TECHNICAL_SPECIFICATION.md
```

---

## Setup Instructions

### Prerequisites
- .NET 8 SDK
- Node.js 18+ and npm
- SQL Server (Express or higher)
- Visual Studio 2022 or VS Code
- Angular CLI (`npm install -g @angular/cli`)

### Backend Setup

1. **Navigate to the API project**:
   ```bash
   cd src/Backend/ByteDigest.Api
   ```

2. **Update connection string** in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=ByteDigestDb;Trusted_Connection=true;TrustServerCertificate=true"
   }
   ```

3. **Update JWT configuration** (use a secure key in production):
   ```json
   "Jwt": {
     "Key": "YourSecureKeyHere-Minimum32Characters",
     "Issuer": "ByteDigest",
     "Audience": "ByteDigestUsers"
   }
   ```

4. **Install EF Core tools** (if not already installed):
   ```bash
   dotnet tool install --global dotnet-ef
   ```

5. **Create and apply migrations**:
   ```bash
   dotnet ef migrations add InitialCreate --project ../ByteDigest.Infrastructure --startup-project .
   dotnet ef database update
   ```

6. **Run the API**:
   ```bash
   dotnet run
   ```

   The API will be available at:
   - HTTPS: https://localhost:5001
   - HTTP: http://localhost:5000
   - Swagger: https://localhost:5001/swagger

7. **Test with default credentials**:
   - Email: admin@bytedigest.com
   - Username: admin
   - Password: Admin123!

### Frontend Setup

1. **Navigate to the Angular project**:
   ```bash
   cd src/Frontend/ByteDigest.Web
   ```

2. **Install dependencies**:
   ```bash
   npm install
   ```

3. **Update API URL** in `src/environments/environment.ts` if needed:
   ```typescript
   export const environment = {
     production: false,
     apiUrl: 'https://localhost:5001/api'
   };
   ```

4. **Run the development server**:
   ```bash
   npm start
   ```
   or
   ```bash
   ng serve
   ```

   The application will be available at: http://localhost:4200

### Testing the Application

1. **Open the frontend**: http://localhost:4200
2. **Browse public posts**: No authentication required
3. **Login as admin**:
   - Click "Giriş" or navigate to http://localhost:4200/giris
   - Username: admin
   - Password: Admin123!
4. **Access admin panel**: You'll be redirected to /admin
5. **Test Swagger API**: https://localhost:5001/swagger
   - Click "Authorize" button
   - Login via /api/auth/login endpoint
   - Copy the token from response
   - Paste in format: `Bearer YOUR_TOKEN_HERE`
   - Test protected endpoints

### Production Deployment

#### Backend
1. Update `appsettings.Production.json` with production settings
2. Use environment variables for sensitive data
3. Build in Release mode: `dotnet publish -c Release`
4. Deploy to IIS, Azure App Service, or containerize with Docker

#### Frontend
1. Update `src/environments/environment.prod.ts` with production API URL
2. Build for production: `npm run build`
3. Deploy `dist/` folder to web server (IIS, nginx, Azure Static Web Apps)

### Database Migrations

**Add new migration**:
```bash
cd src/Backend/ByteDigest.Api
dotnet ef migrations add MigrationName --project ../ByteDigest.Infrastructure --startup-project .
```

**Apply migration**:
```bash
dotnet ef database update
```

**Rollback**:
```bash
dotnet ef database update PreviousMigrationName
```

---

## Code Style Compliance

### Backend Code Style Rules

All backend code follows these strict rules:

1. **No var keyword**: Always use explicit type declarations
   ```csharp
   // Good
   Post post = await _repository.GetByIdAsync(id);
   List<Post> posts = new List<Post>();
   
   // Bad
   var post = await _repository.GetByIdAsync(id);
   var posts = new List<Post>();
   ```

2. **No inline comments**: Use XML documentation only
   ```csharp
   // Good
   /// <summary>
   /// Gets a post by its identifier.
   /// </summary>
   /// <param name="id">The post identifier.</param>
   /// <returns>The post if found; otherwise, null.</returns>
   public async Task<Post?> GetByIdAsync(int id)
   
   // Bad
   // Get post by ID
   public async Task<Post?> GetByIdAsync(int id)
   ```

3. **XML summaries required**: All classes, properties, and methods must have XML documentation
   ```csharp
   /// <summary>
   /// Represents a blog post entity.
   /// </summary>
   public class Post
   {
       /// <summary>
       /// Gets or sets the unique identifier for the post.
       /// </summary>
       public int Id { get; set; }
   }
   ```

4. **No emojis**: Keep code professional and emoji-free

---

## API Documentation

The complete API documentation is available via Swagger UI when the backend is running:

**URL**: https://localhost:5001/swagger

**Features**:
- Interactive API testing
- Request/response schemas
- JWT authentication support
- Grouped by endpoint categories (Auth, Public, Admin)

**Using Swagger**:
1. Start the backend API
2. Navigate to https://localhost:5001/swagger
3. To test protected endpoints:
   - Expand POST /api/auth/login
   - Click "Try it out"
   - Enter credentials
   - Copy the token from response
   - Click "Authorize" button at top
   - Enter: `Bearer YOUR_TOKEN`
   - Now you can test admin endpoints

---

## Security Considerations

1. **JWT Secret**: Change the JWT key in production to a cryptographically secure value
2. **CORS**: Update CORS policy for production domains
3. **HTTPS**: Always use HTTPS in production
4. **SQL Injection**: Protected by EF Core parameterized queries
5. **XSS**: Angular sanitizes HTML by default; use `[innerHTML]` carefully
6. **Password Policy**: Configured in Identity options (8+ chars, uppercase, lowercase, digit, special char)
7. **Token Expiration**: JWT tokens expire after 7 days

---

## Troubleshooting

### Backend Issues

**Database connection fails**:
- Verify SQL Server is running
- Check connection string in appsettings.json
- Ensure TrustServerCertificate=true for local development

**Migrations fail**:
- Ensure you're in ByteDigest.Api directory
- Check project references are correct
- Delete Migrations folder and recreate

**Swagger not showing**:
- Check you're running in Development environment
- Verify Program.cs includes `app.UseSwagger()`

### Frontend Issues

**CORS errors**:
- Verify backend CORS policy includes http://localhost:4200
- Check API is running before starting frontend

**JWT not working**:
- Verify jwtInterceptor is registered in app.config.ts
- Check token format in localStorage
- Ensure token hasn't expired

**Routes not working**:
- Check base href in index.html is "/"
- Verify route lazy loading syntax

---

## Future Enhancements

Potential features for future development:

1. **Backend**:
   - Image upload and management
   - Rich text editor API integration
   - Comment system
   - Tag management (separate table)
   - Post scheduling
   - Analytics and statistics

2. **Frontend**:
   - Rich text editor (TinyMCE, CKEditor)
   - Image upload component
   - Drag-and-drop file upload
   - Advanced filtering and sorting
   - Dashboard analytics charts
   - User profile management
   - Comment section

3. **Infrastructure**:
   - Redis caching
   - CDN integration
   - Logging (Serilog)
   - Health checks
   - Docker containerization
   - CI/CD pipeline

---

## Support and Contact

For issues or questions about this specification:

1. Review this documentation thoroughly
2. Check the code comments and XML documentation
3. Examine the Swagger API documentation
4. Review the Angular component implementations

---

**Document Version**: 1.0
**Last Updated**: December 2024
**Platform**: ByteDigest Blog Platform
**License**: MIT
