using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);

        Task<IReadOnlyList<Product>> GetProductsAsync(); // only readable list  // the three of the method is doing the same thing except the return type so we can convert it into generic types task

        Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetProductsTypesAsync();
    }
}
