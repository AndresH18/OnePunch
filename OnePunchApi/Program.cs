using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using OnePunchApi.Data;
using OnePunchApi.Data.Model;
using OnePunchApi.Data.Repository;
using OnePunchApi.Policies.Handlers;
using OnePunchApi.Policies.Requirements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HeroAssociationDb>();

builder.Services.AddSingleton<IAuthorizationHandler, RoleHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, AdminHandler>();

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
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        };
    });

builder.Services.AddAuthorization(options =>
{
    // options.AddPolicy("IsAdmin",
    //     policyBuilder => { policyBuilder.AddRequirements(new AdminHandler()); });
    options.AddPolicy("Admin",
        policyBuilder => { policyBuilder.AddRequirements(new RoleRequirement("Administrator")); });
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