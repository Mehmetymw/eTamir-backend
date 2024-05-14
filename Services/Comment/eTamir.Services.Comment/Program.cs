using eTamir.Services.Comment.Models;
using eTamir.Services.Comment.Repository;
using eTamir.Services.Comment.Services;
using eTamir.Services.Comment.Settings;
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

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddScoped<ICommentRepository<Comment>, CommentRepository>();
builder.Services.AddScoped<IRatingRepository<Rating>, RatingRepository>();

builder.Services.AddScoped<ICommentService,CommentService>();
builder.Services.AddScoped<IRatingService,RatingService>();

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


app.MapControllers();
app.UseAuthorization();
app.UseAuthentication();

app.Run();

