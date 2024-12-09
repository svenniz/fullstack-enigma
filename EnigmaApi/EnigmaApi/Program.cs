using EnigmaApi.Data_Access;
using EnigmaApi.Repositories;
using EnigmaApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

// Injecting IN-MEMORY Database
builder.Services.AddDbContext<EnigmaDbContext>
    (options=>options.UseInMemoryDatabase("InMemoryDb"));

// Injeting Sqlite Database
//var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SqliteDbs");
//Directory.CreateDirectory(folder);
//var path = Path.Combine(folder, "");

//builder.Services.AddDbContext<EnigmaDbContext>
//    (options => options.UseSqlite($"Data Source = {path}"));


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
    if (context.Database.IsInMemory())
    {
        context.Database.EnsureCreated(); // Ensure the database is created
    }
    else
    {
        // For SQL Server or in this case Sqlite
        // Do not call EnsureCreated for databases that use migrations.
        // EnsureCreated() will try to actually also create a schema
        Console.WriteLine("Database migrations are required for this database provider.");
    }
}

app.Run();
