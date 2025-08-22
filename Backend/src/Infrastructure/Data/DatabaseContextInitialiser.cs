using Bogus;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<DatabaseContextInitialiser>();

        await initialiser.SeedGadgetsAsync();
    }
}

public class DatabaseContextInitialiser
{
    private readonly DatabaseContext _context;


    public DatabaseContextInitialiser(DatabaseContext context)
    {
        _context = context;
    }

    public async Task SeedGadgetsAsync()
    {
        if (!_context.Gadgets.Any())
        {
            var faker = new Faker<Gadget>()
                .RuleFor(g => g.Name, f => f.Commerce.ProductName())
                .RuleFor(g => g.Brand, f => f.Company.CompanyName())
                .RuleFor(g => g.Category, f => f.Commerce.Categories(1)[0])
                .RuleFor(g => g.ReleaseDate, f => f.Date.Past(3))
                .RuleFor(g => g.Price, f => f.Random.Decimal(50, 2000))
                .RuleFor(g => g.IsAvailable, f => f.Random.Bool());

            var gadgets = faker.Generate(100);

            _context.Gadgets.AddRange(gadgets);
            await _context.SaveChangesAsync();
        }
    }
}
