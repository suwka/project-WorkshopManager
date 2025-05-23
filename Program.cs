var builder = WebApplication.CreateBuilder(args);

// Rejestracja usług
builder.Services.AddControllersWithViews(); // ← zmień to, jeśli używasz widoków
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

// Mapowanie kontrolerów i domyślnej trasy
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();