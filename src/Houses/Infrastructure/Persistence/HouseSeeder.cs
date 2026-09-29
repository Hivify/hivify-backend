using Houses.Domain.Houses;
using Microsoft.EntityFrameworkCore;

namespace Houses.Infrastructure.Persistence;

public static class HouseSeeder
{
    public static async Task SeedAsync(HouseDbContext context)
    {
        if (await context.Houses.AnyAsync())
            return;

        var houses = new[]
        {
            CreateHouse("Ansjön", "6"),
            CreateHouse("Tvärstigen", "6"),
            CreateHouse("Backstigen", "4"),
            CreateHouse("Ansjöstigen", "5"),
            CreateHouse("Tvärstigen", "7"),
            CreateHouse("Torpstigen", "16"),
            CreateHouse("Torpstigen", "19"),
            CreateHouse("Kortstigen", "6"),
            CreateHouse("Torpstigen", "1"),
            CreateHouse("Torpstigen", "7"),
            CreateHouse("Backstigen", "3"),
            CreateHouse("Kortstigen", "7"),
            CreateHouse("Kortstigen", "5"),
            CreateHouse("Skogsstigen", "3"),
            CreateHouse("Torpstigen", "10"),
            CreateHouse("Torpstigen", "24"),
            CreateHouse("Skogsstigen", "5"),
            CreateHouse("Backstigen", "6"),
            CreateHouse("Tvärstigen", "3"),
            CreateHouse("Tvärstigen", "11"),
            CreateHouse("Tvärstigen", "5"),
            CreateHouse("Skogsstigen", "1"),
            CreateHouse("Tvärstigen", "4"),
            CreateHouse("Skogsstigen", "2"),
            CreateHouse("Skogsstigen", "6"),
            CreateHouse("Ansjöstigen", "2"),
            CreateHouse("Torpstigen", "9"),
            CreateHouse("Ansjöstigen", "15"),
            CreateHouse("Kortstigen", "3"),
            CreateHouse("Ansjöstigen", "4"),
            CreateHouse("Ansjöstigen", "24"),
            CreateHouse("Ansjöstigen", "21"),
            CreateHouse("Ansjöstigen", "6"),
            CreateHouse("Skogsstigen", "12"),
            CreateHouse("Torpstigen", "11"),
            CreateHouse("Torpstigen", "14"),
            CreateHouse("Ansjöstigen", "3"),
            CreateHouse("Torpstigen", "21"),
            CreateHouse("Torpstigen", "12"),
            CreateHouse("Kortstigen", "8"),
            CreateHouse("Skogsstigen", "11"),
            CreateHouse("Torpstigen", "26"),
            CreateHouse("Skogsstigen", "15"),
            CreateHouse("Torpstigen", "18"),
            CreateHouse("Skogsstigen", "4"),
            CreateHouse("Tvärstigen", "9"),
            CreateHouse("Ansjöstigen", "11"),
            CreateHouse("Torpstigen", "22"),
            CreateHouse("Torpstigen", "5"),
            CreateHouse("Torpstigen", "25"),
            CreateHouse("Backstigen", "8"),
            CreateHouse("Skogsstigen", "7"),
            CreateHouse("Torpstigen", "20"),
            CreateHouse("Backstigen", "2"),
            CreateHouse("Torpstigen", "15"),
            CreateHouse("Torpstigen", "23"),
            CreateHouse("Torpstigen", "27"),
            CreateHouse("Skogsstigen", "13"),
            CreateHouse("Torpstigen", "3")
        };

        context.Houses.AddRange(houses);
        await context.SaveChangesAsync();
    }

    private static House CreateHouse(string address, string houseNumber)
    {
        return House.Create(
            new Address(address),
            new HouseNumber(houseNumber),
            new PostalCode("21120"));
    }
}