using System;
using System.Threading.Tasks;
using ProductManagement.Categories;
using ProductManagement.Products;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Data;

public class ProductManagementDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Category, Guid> _categoryRepository;
    private readonly IRepository<Product, Guid> _productRepository;

    public ProductManagementDataSeedContributor(IRepository<Product, Guid> productRepository, IRepository<Category, Guid> categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }


    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _categoryRepository.CountAsync() > 0)
        {
            return;
        }

        var monitors = new Category { Name = "Monitors" };
        var printers = new Category { Name = "Printers" };
        await _categoryRepository.InsertManyAsync(new[] { monitors, printers });

        var monitor1 = new Product
        {
            Category = monitors,
            Name = "XP VH240a Full HD 1080p IPS LED Monitor",
            Price = 189,
            ReleaseDate = new DateTime(2022, 02, 10),
            StockState = ProductStockState.InStock
        };
        
        var monitor2 = new Product
        {
            Category = monitors,
            Name = "Clips 328E1CA 32-Inch Curved Monitor, 4K UHD",
            Price = 890,
            ReleaseDate = new DateTime(2023, 1, 10),
            StockState = ProductStockState.PreOrder
        };

        var printer1 = new Product
        {
            Category = printers,
            Name = "Acme Monochrome Laser Printer, Compact All-In-One",
            Price = 546,
            ReleaseDate = new DateTime(2020, 1, 19),
            StockState = ProductStockState.NotAvailable
        };

        await _productRepository.InsertManyAsync(new[] { monitor1, monitor2, printer1 });
    }
}