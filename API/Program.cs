using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); //for running the application . reads from configurations

// Add services to the container.   // Dependency injection // all the services is added in this part

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));  // =====> we have to add the DBcontext service
});
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // scope is how the classes should be disposed . it becomes alive when an http requested for this service // singlton this service is created since the starting to the end of the running of the app // transient it is long 
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // here we added typeof due to type is not defined for this scopes
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();  // => after adding the services to the application we build the app
 
// Configure the HTTP request pipeline.  // => swagger is the way of documenting about our endpoints in our api-localhost:5001/swagger/index.html
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); => we dont need it because it may cause warnings in our application

app.UseStaticFiles();  // by default system serves static files from wwwroot folder

app.UseAuthorization(); // 

app.MapControllers(); // middleware to register api endpoints where to send the request and other things in using a controller

using var scope = app.Services.CreateScope();  // Applying migrations and creating database at the app startup // scopes are disposed 
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();  
    await StoredContextSeed.seedAsync(context);
}
catch(Exception ex)
{
    logger.LogError(ex, "An error has occured during migration or while seeding data...");
}

app.Run();
