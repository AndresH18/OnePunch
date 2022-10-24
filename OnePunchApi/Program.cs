using OnePunchApi.Data;
using OnePunchApi.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HeroAssociationDb>();

builder.Services.AddScoped<HeroesRepository>();
builder.Services.AddScoped<MonsterRepository>();
builder.Services.AddScoped<SponsorRepository>();
builder.Services.AddScoped<MonsterCellRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();