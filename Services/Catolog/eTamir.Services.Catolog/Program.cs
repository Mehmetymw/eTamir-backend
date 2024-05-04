using eTamir.Services.Catolog.Settings;
using Microsoft.Extensions.Options;
using Serilog.Formatting.Json;
using Serilog;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using eTamir.Services.Catolog.Services;
using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using eTamir.Services.Catolog.Models;
using eTamir.Shared.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServer:Url"];
        options.Audience = builder.Configuration["IdentityServer:Audience"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false
        };

    });

builder.Services.AddControllers();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddScoped<CatalogRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<MechanicRepository>();

builder.Services.AddScoped<ICatalogService<CatalogDto>, CatalogService>();
builder.Services.AddScoped<ICategoryService<CategoryDto>, CategoryService>();
builder.Services.AddScoped<IMechanicService<MechanicDto>, MechanicService>();

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();

Assembly assembly = Assembly.GetExecutingAssembly();
FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
string assemblyFolder = AppDomain.CurrentDomain.BaseDirectory;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
