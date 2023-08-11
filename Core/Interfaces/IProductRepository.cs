﻿using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);

        Task<IReadOnlyList<Product>> GetProductsAsync(); // only readable list

        Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetProductsTypesAsync();
    }
}
