using Bogus;
using Inventory.Data.Settings;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Identity.Hashing;
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
        var operationClaimCollection = database.GetCollection<OperationClaim>(nameof(OperationClaim));
        var userOperationClaimCollection = database.GetCollection<UserOperationClaim>(nameof(UserOperationClaim));

        var deviceCategories = GetDeviceCategories();
        var deviceCategoryIds = deviceCategories.Select(dC => dC.Id);
        deviceCategoryCollection.InsertManyAsync(deviceCategories);

        var brands = GetBrands();
        var brandIds = brands.Select(b => b.Id);
        brandCollection.InsertManyAsync(brands);

        var operationClaims = GetOperationClaims();
        var operationClaimsIds = operationClaims.Select(u => u.Id);
        operationClaimCollection.InsertManyAsync(operationClaims);

        var users = GetUsers();
        var userIds = users.Select(u => u.Id);
        userCollection.InsertManyAsync(users);


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

        var userOperationClaims = new Faker<UserOperationClaim>("tr")
            .RuleFor(u => u.UserId, uOC => uOC.PickRandom(userIds))
            .RuleFor(u => u.OperationClaimId, uOC => uOC.PickRandom(operationClaimsIds))
            .Generate(5);

        userOperationClaimCollection.InsertManyAsync(userOperationClaims);
    }

    private static List<OperationClaim> GetOperationClaims()
    {
        return new List<OperationClaim>()
        {
            new OperationClaim()
            {
                Name = "Admin"
            },
            new OperationClaim()
            {
                Name = "Manager"
            },
            new OperationClaim()
            {
                Name = "Default"
            }
        };
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
        HashingHelper.CreatePasswordHash("123qwe", out var passwordHash, out var passwordSalt);

        var result = new Faker<User>("tr")
            .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(i => i.FirstName, i => i.Person.FirstName)
            .RuleFor(i => i.LastName, i => i.Person.LastName)
            .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
            .RuleFor(i => i.Department, i => i.Commerce.Department())
            .RuleFor(i => i.PasswordHash, i => passwordHash)
            .RuleFor(i => i.PasswordSalt, i => passwordSalt)
            .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
            .Generate(10);

        return result;
    }
}