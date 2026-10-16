namespace Mokka.Models;

/// <summary>A drink on the Mokka menu.</summary>
/// <param name="Name">Display name, also used as image alt text.</param>
/// <param name="Price">Price in euro.</param>
/// <param name="Image">Path under wwwroot, for example "img/cold-brew.png".</param>
/// <param name="Badge">Optional label on the photo, for example "Nieuw".</param>
public sealed record Product(string Name, decimal Price, string Image, string? Badge = null);

/// <summary>The customer's usual order, shown in the "Je vaste" card.</summary>
public sealed record FavouriteOrder(Product Product, string Options);

/// <summary>Opening state of the pickup location.</summary>
public sealed record StoreStatus(string OpenUntil, string Message);

public static class Format
{
    private static readonly System.Globalization.CultureInfo Be = new("nl-BE");

    /// <summary>€3,40 style prices.</summary>
    public static string Euro(decimal amount) => amount.ToString("C2", Be).Replace(" ", "").Replace(" ", "");
}
