using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions // this method is an extension method which is used to add services here instead of adding services in the program.cs
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)  // because this is an extension method we need to use this
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));  // =====> we have to add the DBcontext service
            });
            services.AddScoped<IProductRepository, ProductRepository>(); // scope is how the classes should be disposed . it becomes alive when an http requested for this service // singlton this service is created since the starting to the end of the running of the app // transient it is long 
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // here we added typeof due to type is not defined for this scopes
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<ApiBehaviorOptions>(options =>  // for api validation errors
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
