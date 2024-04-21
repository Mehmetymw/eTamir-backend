using eTamir.Services.Favorites.Services;
using eTamir.Services.Favorites.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using eTamir.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection(nameof(RedisSettings)));
builder.Services.AddScoped<ISharedIdentityService,SharedIdentityService>();
builder.Services.AddScoped<IFavService,FavService>();

builder.Services.AddSingleton<IRedisSettings>(sp => 
    sp.GetRequiredService<IOptions<RedisSettings>>().Value);

builder.Services.AddSingleton<IRedisService, RedisService>(sp => 
{
    var redisSettings = sp.GetRequiredService<IRedisSettings>();
    var redis = new RedisService(redisSettings.Host, redisSettings.Port);
    redis.Connect();

    return redis;
});

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
