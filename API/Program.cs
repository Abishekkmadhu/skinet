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

var app = builder.Build();  // => after adding the services to the application we build the app
 
// Configure the HTTP request pipeline.  // => swagger is the way of documenting about our endpoints in our api-localhost:5001/swagger/index.html
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); => we dont need it because it may cause warnings in our application

app.UseAuthorization(); // 

app.MapControllers(); // middleware to register api endpoints where to send the request and other things in using a controller

app.Run();
