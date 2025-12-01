# ByteDigest - Quick Start Guide

This guide will help you get ByteDigest up and running in minutes.

## Prerequisites

Install these tools before starting:

- **.NET 8 SDK**: https://dotnet.microsoft.com/download/dotnet/8.0
- **Node.js 18+**: https://nodejs.org/
- **SQL Server**: Express edition or higher
- **Angular CLI**: Run `npm install -g @angular/cli`

## Quick Setup (5 Minutes)

### Step 1: Database Setup (1 minute)

1. Ensure SQL Server is running
2. Open `src/Backend/ByteDigest.Api/appsettings.json`
3. Update the connection string with your SQL Server instance:
   ```json
   "DefaultConnection": "Server=localhost;Database=ByteDigestDb;Trusted_Connection=true;TrustServerCertificate=true"
   ```

### Step 2: Backend Setup (2 minutes)

```bash
# Navigate to API project
cd src/Backend/ByteDigest.Api

# Create database and apply migrations
dotnet ef migrations add InitialCreate --project ../ByteDigest.Infrastructure --startup-project .
dotnet ef database update

# Run the API
dotnet run
```

The API will start at `https://localhost:5001`

Swagger documentation: `https://localhost:5001/swagger`

### Step 3: Frontend Setup (2 minutes)

Open a new terminal:

```bash
# Navigate to Angular project
cd src/Frontend/ByteDigest.Web

# Install dependencies
npm install

# Run the development server
npm start
```

The app will open at `http://localhost:4200`

## Default Admin Credentials

- **Email**: admin@bytedigest.com
- **Username**: admin
- **Password**: Admin123!

## Testing the Application

### 1. Test Public Access

- Open http://localhost:4200
- Browse the home page (no posts yet)
- Try search and filters

### 2. Login as Admin

- Click on the login link or go to http://localhost:4200/giris
- Login with default credentials
- You'll be redirected to `/admin`

### 3. Create Your First Post

1. Go to "Yazılar" in the admin panel
2. Click "+ Yeni Yazı"
3. Fill in the form (note: create/edit forms need to be implemented for full CRUD)
4. The post will appear on the home page if published

### 4. Test the API with Swagger

1. Open https://localhost:5001/swagger
2. Try the login endpoint:
   - Expand `POST /api/auth/login`
   - Click "Try it out"
   - Enter:
     ```json
     {
       "userNameOrEmail": "admin",
       "password": "Admin123!"
     }
     ```
   - Click "Execute"
   - Copy the token from the response

3. Authorize Swagger:
   - Click the "Authorize" button (top right)
   - Enter: `Bearer YOUR_TOKEN_HERE`
   - Click "Authorize"

4. Test protected endpoints:
   - Try `GET /api/admin/posts`
   - Try creating a post via `POST /api/admin/posts`

## Project Structure Overview

```
ByteDigest/
├── src/
│   ├── Backend/              # ASP.NET Core Web API
│   │   ├── ByteDigest.Domain       # Entities and interfaces
│   │   ├── ByteDigest.Application  # Services and DTOs
│   │   ├── ByteDigest.Infrastructure # EF Core and repositories
│   │   └── ByteDigest.Api          # Controllers and configuration
│   └── Frontend/             # Angular Application
│       └── ByteDigest.Web/
│           └── src/app/
│               ├── core/     # Services and models
│               ├── shared/   # Guards and interceptors
│               ├── auth/     # Login components
│               ├── public/   # Public blog components
│               └── admin/    # Admin panel components
```

## Key URLs

- **Frontend**: http://localhost:4200
- **Login Page**: http://localhost:4200/giris
- **Admin Panel**: http://localhost:4200/admin
- **Backend API**: https://localhost:5001
- **Swagger Docs**: https://localhost:5001/swagger

## API Endpoints Summary

### Public (No Auth Required)
- `GET /api/posts` - Get published posts (with pagination)
- `GET /api/posts/{slug}` - Get a post by slug
- `GET /api/pages/{slug}` - Get a page by slug
- `POST /api/auth/login` - Login

### Admin Only (Requires JWT Token)
- `GET /api/admin/posts` - Get all posts
- `POST /api/admin/posts` - Create post
- `PUT /api/admin/posts/{id}` - Update post
- `DELETE /api/admin/posts/{id}` - Delete post
- Similar endpoints for pages: `/api/admin/pages`

## Creating Sample Data

You can create posts via Swagger or directly via SQL:

```sql
INSERT INTO Posts (Slug, Title, Excerpt, Content, Category, Tags, IsPublished, CreatedAt)
VALUES 
('dotnet-8-yenilikleri', 
 '.NET 8 Yenilikleri', 
 '.NET 8 ile gelen başlıca yenilikler',
 '<h2>Başlık</h2><p>İçerik burada...</p>',
 'Yazılım',
 'dotnet,csharp',
 1,
 GETUTCDATE());
```

Or via Swagger:
1. Authorize with admin token
2. Use `POST /api/admin/posts`
3. Send:
```json
{
  "slug": "angular-18-rehberi",
  "title": "Angular 18 Rehberi",
  "excerpt": "Angular 18 ile modern web uygulamaları geliştirme",
  "content": "<h2>Giriş</h2><p>Angular 18 ile...</p>",
  "category": "Web",
  "tags": "angular,typescript,web",
  "isPublished": true
}
```

## Common Issues

### "Cannot connect to database"
- Verify SQL Server is running
- Check connection string in `appsettings.json`
- Try connecting via SQL Server Management Studio first

### "CORS error in browser"
- Ensure backend is running before frontend
- Verify backend CORS policy includes `http://localhost:4200`
- Check browser console for exact error

### "Unauthorized 401 error"
- Check JWT token is valid and not expired
- Ensure token is in format: `Bearer YOUR_TOKEN`
- Login again to get a new token

### Frontend won't build
- Run `npm install` again
- Clear node_modules: `rm -rf node_modules && npm install`
- Check Node.js version is 18 or higher

### Migrations error
- Ensure you're in the `ByteDigest.Api` directory
- Check all project references are correct
- Try deleting the Migrations folder and recreating

## Next Steps

1. **Read the Full Specification**: See `TECHNICAL_SPECIFICATION.md`
2. **Implement Create/Edit Forms**: Add full CRUD forms in admin panel
3. **Add Rich Text Editor**: Integrate TinyMCE or CKEditor
4. **Customize Styling**: Update CSS to match your brand
5. **Add More Features**: Comments, categories, image upload, etc.

## Code Style Rules (Backend)

Remember these strict rules when modifying backend code:

1. **No `var` keyword** - Always use explicit types
2. **No inline comments** - Use XML documentation only
3. **XML summaries required** - On all classes, properties, and methods
4. **No emojis** - Keep code professional

Example:
```csharp
/// <summary>
/// Gets a post by its identifier.
/// </summary>
/// <param name="id">The post identifier.</param>
/// <returns>The post if found; otherwise, null.</returns>
public async Task<Post?> GetPostByIdAsync(int id)
{
    Post? post = await _postRepository.GetByIdAsync(id);
    return post;
}
```

## Getting Help

- Review `TECHNICAL_SPECIFICATION.md` for detailed documentation
- Check Swagger API docs at https://localhost:5001/swagger
- Examine the code - it's fully documented with XML summaries
- Check the browser console for frontend errors
- Check the API terminal output for backend errors

## Production Deployment

When ready for production:

1. **Backend**:
   - Update `appsettings.Production.json`
   - Use secure JWT key
   - Configure production database
   - Build: `dotnet publish -c Release`

2. **Frontend**:
   - Update `environment.prod.ts` with production API URL
   - Build: `npm run build`
   - Deploy `dist/` folder

3. **Database**:
   - Apply migrations to production database
   - Update connection string with production credentials

---

**Happy Coding!** 🚀

For detailed information, see `TECHNICAL_SPECIFICATION.md`
