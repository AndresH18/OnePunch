using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using OnePunchApi.Data;
using OnePunchApi.Data.Model;
using OnePunchApi.Data.Repository;
using OnePunchApi.Policies.Requirements;
using OnePunchApi.Security.Models;
using OnePunchApi.Security.Policies;
using OnePunchApi.Security.Policies.Handlers;
using OnePunchApi.Services;
using RoleRequirement = OnePunchApi.Security.Policies.Requirements.RoleRequirement;

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

    options.AddPolicy(PolicyConstants.HeroS,
        policy =>
        {
            policy.AddRequirements(
                new RoleRequirement(Role.Hero),
                new RankRequirement(Rank.S));
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
// using (var scope = scopeFactory.CreateScope())
// {
//     using var db = scope.ServiceProvider.GetRequiredService<HeroAssociationDb>();
//     db.Database.EnsureDeleted();
//     if (db.Database.EnsureCreated())
//     {
//         SeedData.Initialize(db);
//     }
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();