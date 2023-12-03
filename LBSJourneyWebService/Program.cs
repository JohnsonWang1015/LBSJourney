using System.Reflection;
using LBSJourneyWebService.Models;
using LBSJourneyWebService.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    (options) =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo()
        {
            Version = "v1",
            Title = "Location-Base Services API",
            Description = "Location-Base Services Web API",
        });
        var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        String path = Path.Combine(AppContext.BaseDirectory, xmlFileName);
        options.IncludeXmlComments(path);
    }
    );

ConfigurationManager manager = builder.Configuration;
String connectionString = manager.GetConnectionString("localDB");

Salts salts = new Salts();
IConfigurationSection saltSection = manager.GetSection("salts");
saltSection.Bind(salts);
builder.Services.AddSingleton(salts);

LineServiceAPI lineServiceAPI = new LineServiceAPI();
IConfigurationSection lineServiceAPISection = manager.GetSection("lineAPI");
lineServiceAPISection.Bind(lineServiceAPI);
builder.Services.AddSingleton(lineServiceAPI);

Dropbox dropbox = new Dropbox();
IConfigurationSection dropboxSection = manager.GetSection("dropbox");
dropboxSection.Bind(dropbox);
builder.Services.AddSingleton(dropbox);

builder.Services.AddDbContext<LbsDb>(
    (optionBuilder)=>
    {
        optionBuilder.UseSqlServer(connectionString);
    });

builder.Services.AddCors(
    (options) =>
    {
        options.AddDefaultPolicy(
            (builder) =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
