using LBSJourney.Models;
using LBSJourney.Models.Dao;
using LBSJourney.Models.Entity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfigurationSection authSection = builder.Configuration.GetSection("authPath");
AuthorizationPath authPath = new AuthorizationPath();
authSection.Bind(authPath);

AppSecurity appSecurity = new AppSecurity(authPath.salt);
builder.Services.AddSingleton(appSecurity);

// Add services to the container.
builder.Services.AddControllersWithViews();

ILoggingBuilder loggingBuilder = builder.Logging;
loggingBuilder.ClearProviders();
loggingBuilder.AddConsole();

builder.Services.AddHttpClient();
builder.Services.AddSession(
    (options) =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.Name = "LBSJourney.Session";
        options.IdleTimeout = TimeSpan.FromMinutes(30);
    }
);

ServiceURL url = new ServiceURL();
IConfigurationSection section = builder.Configuration.GetSection("ServiceURL");
section.Bind(url);
builder.Services.AddSingleton(url);

AxiosBaseURL axiosBaseURL = new AxiosBaseURL();
IConfigurationSection axiosSection = builder.Configuration.GetSection("AxiosBaseURL");
axiosSection.Bind(axiosBaseURL);
builder.Services.AddSingleton(axiosBaseURL);

// 連接字串
String connectionString = builder.Configuration.GetConnectionString("LBS");

ConnectionFactory factory = new ConnectionFactory();
factory.ConnectionString = connectionString;
builder.Services.AddSingleton(factory);

UsersDao usersDao = new UsersDao();
usersDao.Factory = factory;
builder.Services.AddSingleton(usersDao);

builder.Services.AddDbContext<LbsDB>(
    (optionBuilder) =>
    {
        optionBuilder.UseSqlServer(connectionString);
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
