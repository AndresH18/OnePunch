using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using OnePunch.Api.Data;
using OnePunch.Api.Data.Repository;
using OnePunch.Api.Security.Models;
using OnePunch.Api.Security.Policies;
using OnePunch.Api.Security.Policies.Handlers;
using OnePunch.Api.Security.Policies.Requirements;
using OnePunch.Api.Services;
using OnePunchApi.Security.Models;
using Shared.Data.Model;
using RoleRequirement = OnePunch.Api.Security.Policies.Requirements.RoleRequirement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HeroAssociationDb>();

builder.Services.AddScoped<IAuthorizationHandler, RoleHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RankHandler>();

builder.Services.AddScoped<UserManager>();

builder.Services.AddScoped<HeroesRepository>();
builder.Services.AddScoped<MonsterRepository>();
builder.Services.AddScoped<SponsorRepository>();
builder.Services.AddScoped<MonsterCellRepository>();
builder.Services.AddScoped<FightsRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyConstants.Admin,
        policy => { policy.AddRequirements(new RoleRequirement(Role.Admin)); });

    options.AddPolicy(PolicyConstants.HeroS, policy =>
    {
        policy.AddRequirements(
            new RoleRequirement(Role.Hero),
            new RankRequirement(Rank.S)
        );
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "OnePunchApi",
        Version = "V1"
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
// TODO: Delete when publishing
using (var scope = scopeFactory.CreateScope())
{
    using var db = scope.ServiceProvider.GetRequiredService<HeroAssociationDb>();
    db.Database.EnsureDeleted();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();