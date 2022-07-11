using Bogus;
using Inventory.Data.Settings;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using MongoDB.Driver;

namespace Inventory.Data.Context;

public static class InventoryDbSeed
{
    public static void SeedAsync(IMongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        
        var deviceCategoryCollection = database.GetCollection<Category>(nameof(Category));
        var existDeviceCategory = deviceCategoryCollection.Find(dC => true).Any();
        if (existDeviceCategory) return;
        
        var brandCollection = database.GetCollection<Brand>(nameof(Brand));
        var productCollection = database.GetCollection<Product>(nameof(Product));
        var userCollection = database.GetCollection<User>(nameof(User));

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
            .RuleFor(p => p.SerialNumber, p => p.Commerce.Ean8())
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

    private static List<User> GetUsers()
    {
        var result= new Faker<User>("tr")
            .RuleFor(i=>i.CreateDate, i=>i.Date.Between(DateTime.Now.AddDays(-100),DateTime.Now))
            .RuleFor(i => i.FirstName, i => i.Person.FirstName)
            .RuleFor(i => i.LastName, i => i.Person.LastName)
            .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
            .RuleFor(i => i.Password, i => i.Internet.Password())
            .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
            .Generate(10);

        return result;
    }
}