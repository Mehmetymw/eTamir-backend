using eTamir.Services.Address.Repository;
using eTamir.Services.Address.Services;
using eTamir.Services.Address.Settings;
using eTamir.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
