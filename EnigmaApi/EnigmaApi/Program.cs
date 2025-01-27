using EnigmaApi.Boosters.Services;
using EnigmaApi.Cards.Repositories;
using EnigmaApi.Cards.Services;
using EnigmaApi.Data_Access;
using EnigmaApi.Decks.Repositories;
using EnigmaApi.Decks.Services;
using EnigmaApi.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);

// appsettings.json variable for whether or not using inmemory
var isInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

// Add services to the container.

// Register HttpClient
builder.Services.AddHttpClient<IScryfallCardService, ScryfallCardService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(MappingProfile));

// Injecting Services and Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericEfCoreRepository<>));
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IDeckRepository, DeckRepository>();
builder.Services.AddScoped<IScryfallCardService, ScryfallCardService>();
builder.Services.AddScoped<ICardFileService, CardFileService>();
builder.Services.AddScoped<IDeckService, DeckService>();
builder.Services.AddScoped<IBoosterService, BoosterService>();

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow any origin
              .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader(); // Allow any header
    });
});

builder.Services.AddDbContext<EnigmaDbContext>(options =>
{
    if (isInMemoryDatabase)
    {
        // Injecting IN-MEMORY Database
        options.UseInMemoryDatabase("InMemoryDb");
        Console.WriteLine("Database is InMemory");
    }
    else
    {
        // Injecting MySql Database
        options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), new MySqlServerVersion(new Version(8, 0, 36)));
        Console.WriteLine("Database is MySql.");

        // Injecting Sqlite Database
        //options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
        //Console.WriteLine("Database is Sqlite.");
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS policy
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EnigmaDbContext>();
    var deckService = scope.ServiceProvider.GetRequiredService<IDeckService>();

    if (isInMemoryDatabase)
    {
        Console.WriteLine("Seeding Test Data now:");
        await SeedData.SeedRealData(context, deckService);
        context.Database.EnsureCreated(); // Ensure the database is created
        Console.WriteLine("In-Memory Database created and seeded.");
    }
    else
    {
        // For SQL Server or in this case Sqlite
        // Do not call EnsureCreated for databases that use migrations.
        // EnsureCreated() will try to actually also create a schema
        Console.WriteLine("Seeding Real Data now:");
        if (!context.Cards.Any())
        {
            await SeedData.SeedRealData(context, deckService);
        }
        // Apply migrations for SQLite/MySQL databases
        Console.WriteLine("Applying Database Migrations...");
        context.Database.Migrate();
        Console.WriteLine("Database migrations are required for this database provider.");
    }
}

app.Run();