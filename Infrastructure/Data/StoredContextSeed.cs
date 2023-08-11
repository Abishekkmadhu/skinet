using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoredContextSeed  // for seeding data from a json file by deserialzing it and storing into the database
    {
        public static async Task seedAsync(StoreContext context)
        {
            if(!context.ProductBrands.Any())  // while seeding we have seed types and brands first because we have product fk referencing into both of these
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands); // we have addrangeasync but during this time db is not used by any other services so no need of async operation
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types); 
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var product = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(product); 
            }

            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}
