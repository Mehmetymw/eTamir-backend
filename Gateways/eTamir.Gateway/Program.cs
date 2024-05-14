using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json");
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

builder.Services.AddAuthentication()
    .AddJwtBearer("GatewayAuthenticationScheme", options =>
    {
        options.Authority = builder.Configuration["IdentityServer:Url"];
        options.Audience = builder.Configuration["IdentityServer:Audience"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidIssuers = [builder.Configuration["IdentityServer:Url"], "http://10.0.2.2:5001"]
        };
    });

builder.Services.AddOcelot();

var app = builder.Build();
app.UseCors("AllowAnyOrigin");

await app.UseOcelot();

app.Run();
