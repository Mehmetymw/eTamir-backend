using eTamir.Services.Favorites.Services;
using eTamir.Services.Favorites.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using eTamir.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServer:Url"];
        options.Audience = builder.Configuration["IdentityServer:Audience"];
    });



builder.Services.AddHttpContextAccessor();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection(nameof(RedisSettings)));
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddScoped<IFavService, FavService>();

builder.Services.AddSingleton<IRedisSettings>(sp =>
    sp.GetRequiredService<IOptions<RedisSettings>>().Value);

builder.Services.AddSingleton<IRedisService, RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IRedisSettings>();
    var redis = new RedisService(redisSettings.Host, redisSettings.Port);
    redis.Connect();

    return redis;
});




builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthorization();
app.UseAuthentication();
app.Run();
