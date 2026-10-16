using Mokka.Models;

namespace Mokka.Services;

/// <summary>
/// Client state for one browser session (one Blazor circuit):
/// who is logged in, the current order and the toast message.
/// Mirrors the prototype variables in Figma ("Mokka prototype": vanAfrekenen, loginKop, loginTekst).
/// </summary>
public sealed class AppState
{
    public event Action? Changed;

    // ---------- Account ----------

    public bool IsLoggedIn { get; private set; }
    public string FirstName { get; private set; } = "Erhan";
    public string Email { get; private set; } = "naam@voorbeeld.be";
    public string Initial => string.IsNullOrEmpty(FirstName) ? "?" : FirstName[..1].ToUpperInvariant();

    public void LogIn(string email, string? firstName = null)
    {
        IsLoggedIn = true;
        Email = email;
        if (!string.IsNullOrWhiteSpace(firstName)) FirstName = firstName.Trim();
        Notify();
    }

    public void LogOut()
    {
        IsLoggedIn = false;
        Notify();
    }

    // ---------- Order ----------

    private readonly List<CartLine> _cart = [];
    public IReadOnlyList<CartLine> Cart => _cart;
    public int ItemCount => _cart.Sum(l => l.Quantity);
    public decimal Total => _cart.Sum(l => l.Total);
    public bool PickupAsap { get; set; } = true;
    public string Note { get; set; } = "";

    public AppState()
    {
        // Demo order so the screens match the Figma design. Remove for a real build.
        _cart.Add(new CartLine(Catalog.Get("cappuccino"), Size.Medium, Milk.Gewoon, 1));
        _cart.Add(new CartLine(Catalog.Get("flat-white"), Size.Klein, Milk.Gewoon, 1));
    }

    public void Add(Product product, Size size = Size.Medium, Milk? milk = null, int quantity = 1)
    {
        var m = milk ?? product.DefaultMilk;
        var line = _cart.FirstOrDefault(l => l.Product == product && l.Size == size && l.Milk == m);
        if (line is null) _cart.Add(new CartLine(product, size, m, quantity));
        else line.Quantity += quantity;

        ShowToast("Toegevoegd aan je bestelling", "Tik op Bekijk bestelling om af te rekenen.");
    }

    public void Increase(CartLine line)
    {
        line.Quantity++;
        Notify();
    }

    public void Decrease(CartLine line)
    {
        if (--line.Quantity <= 0) _cart.Remove(line);
        Notify();
    }

    // ---------- Placed order ----------

    public string? OrderNumber { get; private set; }
    public IReadOnlyList<CartLine> PlacedOrder { get; private set; } = [];
    public decimal PlacedTotal => PlacedOrder.Sum(l => l.Total);
    private int _nextNumber = 27;

    public int Stamps { get; private set; } = 7;
    public const int StampsPerCard = 10;

    public void PlaceOrder()
    {
        OrderNumber = $"A{_nextNumber++}";
        PlacedOrder = _cart.ToList();
        Stamps = Math.Min(StampsPerCard, Stamps + ItemCount);
        _cart.Clear();
        Note = "";
        Notify();
    }

    public readonly List<PastOrder> History =
    [
        new("Gisteren, 8:12", [(Catalog.Get("cappuccino"), Size.Medium, Milk.Gewoon)]),
        new("Ma 12 okt, 7:54", [(Catalog.Get("cappuccino"), Size.Medium, Milk.Gewoon), (Catalog.Get("cold-brew"), Size.Medium, Milk.Geen)]),
        new("Vr 9 okt, 8:03", [(Catalog.Get("flat-white"), Size.Klein, Milk.Gewoon)]),
    ];

    public void Reorder(PastOrder order)
    {
        foreach (var (product, size, milk) in order.Items) Add(product, size, milk);
    }

    // ---------- Toast (success feedback) ----------

    public (string Title, string Text)? Toast { get; private set; }
    private CancellationTokenSource? _toastTimer;

    public void ShowToast(string title, string text)
    {
        Toast = (title, text);
        Notify();

        _toastTimer?.Cancel();
        _toastTimer = new CancellationTokenSource();
        var token = _toastTimer.Token;
        _ = Task.Delay(TimeSpan.FromSeconds(5), token).ContinueWith(t =>
        {
            if (!t.IsCanceled) HideToast();
        }, TaskScheduler.Default);
    }

    public void HideToast()
    {
        Toast = null;
        Notify();
    }

    // ---------- Helpers ----------

    public static string Greeting => DateTime.Now.Hour switch
    {
        < 12 => "Goeiemorgen",
        < 18 => "Goeiemiddag",
        _ => "Goeieavond",
    };

    private void Notify() => Changed?.Invoke();
}
