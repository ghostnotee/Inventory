using Bogus;
using Inventory.Data.Settings;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using MongoDB.Driver;

namespace Inventory.Data.Context;

public class InventoryDbSeed
{
    public static void SeedAsync(IMongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        var brandCollection = database.GetCollection<Brand>(nameof(Brand));
        var deviceCategoryCollection = database.GetCollection<Category>(nameof(Category));
        var productCollection = database.GetCollection<Product>(nameof(Product));

        var existDeviceCategory = deviceCategoryCollection.Find(dC => true).Any();
        if (existDeviceCategory) return;


        var deviceCategories = GetDeviceCategories();
        var deviceCategoryIds = deviceCategories.Select(dC => dC.Id);
        deviceCategoryCollection.InsertManyAsync(deviceCategories);

        var brands = GetBrands();
        var brandIds = brands.Select(b => b.Id);
        brandCollection.InsertManyAsync(brands);

        var products = new Faker<Product>("tr")
            .RuleFor(p => p.CategoryId, p => p.PickRandom(deviceCategoryIds))
            .RuleFor(p => p.BrandId, p => p.PickRandom(brandIds))
            .RuleFor(p => p.Name, p => p.Commerce.ProductName())
            .RuleFor(dC => dC.Model, dC => dC.Commerce.ProductMaterial())
            .RuleFor(dC => dC.Company, dC => dC.Random.Enum<Companies>())
            .RuleFor(dC => dC.Description, dC => dC.Commerce.ProductDescription())
            .RuleFor(dC => dC.DebitTicket, dC =>
                new DebitTicket()
                {
                    UserName = dC.Name.FullName(),
                    Department = dC.Name.JobArea(),
                    UserEmail = dC.Internet.Email(),
                    Location = dC.Address.City(),
                    DebitTicketUrl = dC.Image.PicsumUrl()
                })
            .Generate(50);

        productCollection.InsertManyAsync(products);
    }

    private static List<Category> GetDeviceCategories()
    {
        var faker = new Faker<Category>("tr")
            .RuleFor(dC => dC.Name, dC => dC.Commerce.Department())
            .Generate(5);

        return faker;
    }

    private static List<Brand> GetBrands()
    {
        var faker = new Faker<Brand>("tr")
            .RuleFor(b => b.CreateDate, b => b.Date.Past())
            .RuleFor(b => b.Name, b => b.Company.CompanyName())
            .Generate(10);
        return faker;
    }
}