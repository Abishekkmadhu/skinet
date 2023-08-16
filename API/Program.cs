using API.Errors;
using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); //for running the application . reads from configurations

// Add services to the container.   // Dependency injection // all the services is added in this part

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices(builder.Configuration); // calling the extension service for adding the application services 

var app = builder.Build();  // => after adding the services to the application we build the app

// Configure the HTTP request pipeline.  // => swagger is the way of documenting about our endpoints in our api-localhost:5001/swagger/index.html
app.UseMiddleware<ExceptionMiddleware>(); // this is used for api error handling for any exceptions 

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseSwagger();  // swagger documentation
app.UseSwaggerUI();

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
catch (Exception ex)
{
    logger.LogError(ex, "An error has occured during migration or while seeding data...");
}

app.Run();
