using EnigmaApi.Data_Access;
using EnigmaApi.Repositories;
using EnigmaApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);

// appsettings.json variable for whether or not using inmemory
var isInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(MappingProfile));

// Injecting Services and Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericEfCoreRepository<>));




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
//// Injecting IN-MEMORY Database
//builder.Services.AddDbContext<EnigmaDbContext>
//    (options => options.UseInMemoryDatabase("InMemoryDb"));

// Injeting Sqlite Database
//var folder = Path.Combine(Environment.
//GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SqliteDbs");
//Directory.CreateDirectory(folder);
//var path = Path.Combine(folder, "enigma-db.db");

//builder.Services.AddDbContext<EnigmaDbContext>
//    (options => options.UseSqlite($"Data Source = {path}"));

// Injeting MySql Database
//builder.Services.AddDbContext<EnigmaDbContext>
//    (options => options.UseMySql(builder.Configuration.
//    GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 36))));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EnigmaDbContext>();
    if (isInMemoryDatabase)
    {
        Console.WriteLine("Seeding Test Data now:");
        SeedData.SeedTestData(context);
        context.Database.EnsureCreated(); // Ensure the database is created
        Console.WriteLine("In-Memory Database created and seeded.");
    }
    else
    {
        // For SQL Server or in this case Sqlite
        // Do not call EnsureCreated for databases that use migrations.
        // EnsureCreated() will try to actually also create a schema
        Console.WriteLine("Seeding Real Data now:");
        SeedData.SeedRealData(context);
        context.Database.Migrate();
        Console.WriteLine("Database migrations are required for this database provider.");
    }
}

app.Run();
