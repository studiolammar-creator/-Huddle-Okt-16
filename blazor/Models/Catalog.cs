using System.Globalization;

namespace Mokka.Models;

public enum Size { Klein, Medium, Groot }

public enum Milk { Gewoon, Haver, Soja, Geen }

/// <summary>A drink on the Mokka menu.</summary>
public sealed record Product(
    string Slug,
    string Name,
    string Category,
    string Summary,
    string Description,
    decimal Price,
    string Image,
    string? Badge = null,
    Milk DefaultMilk = Milk.Gewoon);

/// <summary>One line in the current order.</summary>
public sealed class CartLine(Product product, Size size, Milk milk, int quantity)
{
    public Product Product { get; } = product;
    public Size Size { get; } = size;
    public Milk Milk { get; } = milk;
    public int Quantity { get; set; } = quantity;
    public decimal Total => Product.Price * Quantity;
    public string Options => $"{Size}, {Catalog.MilkLabel(Milk)}";
}

/// <summary>A finished order shown under "Eerder" on the orders screen.</summary>
public sealed record PastOrder(string When, IReadOnlyList<(Product Product, Size Size, Milk Milk)> Items)
{
    public decimal Total => Items.Sum(i => i.Product.Price);
    public string Names => string.Join(", ", Items.Select(i => i.Product.Name));
}

public static class Catalog
{
    public static readonly string[] Categories = ["Espresso", "Filter", "Cold brew", "Thee"];

    public static readonly Product[] Products =
    [
        new("cappuccino", "Cappuccino", "Espresso",
            "Espresso met warme melk en een dikke laag schuim.",
            "Espresso met warme melk en een dikke laag schuim. Zacht, rond en onze best verkochte koffie.",
            3.40m, "img/cappuccino.jpg", Badge: "Populair"),
        new("flat-white", "Flat white", "Espresso",
            "Dubbele ristretto met fluweelzachte melk.",
            "Dubbele ristretto met fluweelzachte melk. Krachtiger dan een cappuccino, met minder schuim.",
            3.60m, "img/flat-white.jpg"),
        new("cold-brew", "Cold brew", "Cold brew",
            "Koud getrokken koffie, zacht en fris met ijs.",
            "Achttien uur koud getrokken, zonder bitterheid. Zacht en fris, geserveerd met ijs.",
            3.90m, "img/cold-brew.jpg", Badge: "Nieuw", DefaultMilk: Milk.Geen),
        new("chai-latte", "Chai latte", "Thee",
            "Zwarte thee met kaneel, kardemom en warme melk.",
            "Zwarte thee met kaneel, kardemom en gember, opgegoten met warme melk. Zacht kruidig.",
            3.80m, "img/chai-latte.jpg"),
    ];

    public static Product? Find(string? slug) => Products.FirstOrDefault(p => p.Slug == slug);

    public static Product Get(string slug) => Find(slug) ?? throw new KeyNotFoundException(slug);

    public static string Anchor(string category) => category.ToLowerInvariant().Replace(' ', '-');

    public static string MilkLabel(Milk milk) => milk switch
    {
        Milk.Gewoon => "gewone melk",
        Milk.Haver => "havermelk",
        Milk.Soja => "sojamelk",
        _ => "zonder melk",
    };

    private static readonly CultureInfo Be = new("nl-BE");

    /// <summary>€3,40 style prices.</summary>
    public static string Euro(decimal amount) =>
        amount.ToString("C2", Be).Replace(" ", "").Replace(" ", "");
}
