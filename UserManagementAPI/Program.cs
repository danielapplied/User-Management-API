using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add services FIRST
builder.Services.AddControllers();

var app = builder.Build();

// ✅ HTTP only (HTTPS disabled)
// app.UseHttpsRedirection();

// Add this BEFORE other middleware like UseAuthorization
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();