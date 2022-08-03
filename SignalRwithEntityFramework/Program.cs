using Microsoft.EntityFrameworkCore;
using SignalRwithEntityFramework.Hubs;
using SignalRwithEntityFramework.MiddlewareExtensions;
using SignalRwithEntityFramework.Models;
using SignalRwithEntityFramework.Repos;
using SignalRwithEntityFramework.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

// DB Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SignalRWithEFContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Singleton
);

// DI
builder.Services.AddSingleton<UserRepo>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<SubscribeNotificationTableDependency>();

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapHub<NotificationHub>("/notificationHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);

app.Run();
