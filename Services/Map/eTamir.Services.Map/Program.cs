using eTamir.Services.Map.Models;
using eTamir.Services.Map.Repository;
using eTamir.Services.Map.Services;
using eTamir.Services.Map.Settings;
using eTamir.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServer:Url"];
        options.Audience = builder.Configuration["IdentityServer:Audience"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidIssuers = [builder.Configuration["IdentityServer:Url"], "http://10.0.2.2:5001"]
        };
    });
builder.Services.AddControllers();

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

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

builder.Services.AddScoped<MapService>();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddScoped<IDatabaseSettings, DatabaseSettings>();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddScoped<IAddressRepository<Address>, AddressRepository>();
builder.Services.AddScoped<IMapRepository<Location>, MapRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IMapService, MapService>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseAuthorization();
app.UseAuthentication();

app.Run();
