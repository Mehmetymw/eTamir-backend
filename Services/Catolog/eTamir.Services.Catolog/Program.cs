using eTamir.Services.Catolog.Settings;
using Microsoft.Extensions.Options;
using Serilog.Formatting.Json;
using Serilog;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Serilog.Sinks.RollingFile;
using eTamir.Services.Catolog.Services;
using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Repository;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<MechanicRepository>();

builder.Services.AddScoped<ICategoryService<CategoryDto>, CategoryService>();
builder.Services.AddScoped<IMechanicService<MechanicDto>, MechanicService>();

Assembly assembly = Assembly.GetExecutingAssembly();
FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
string assemblyFolder = AppDomain.CurrentDomain.BaseDirectory;

Log.Logger = new LoggerConfiguration()
     .Enrich.WithProperty("version", fvi.FileVersion)
     .WriteTo.Sink(new RollingFileSink(assemblyFolder + "\\logs\\log-{Date}.txt", new JsonFormatter(renderMessage: true), null, null, Encoding.UTF8))
     .CreateLogger();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
