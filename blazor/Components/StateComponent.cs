using Microsoft.AspNetCore.Components;
using Mokka.Services;

namespace Mokka.Components;

/// <summary>
/// Base for pages and components that show AppState: re-renders whenever the state changes,
/// also when the change comes from a child (for example the + button on a product card
/// updating the order bar on the menu page).
/// Pages that need start-up logic override OnParametersSet, so the subscription here stays intact.
/// </summary>
public abstract class StateComponent : ComponentBase, IDisposable
{
    [Inject] protected AppState State { get; set; } = default!;
    [Inject] protected NavigationManager Nav { get; set; } = default!;

    protected override void OnInitialized() => State.Changed += OnStateChanged;

    private void OnStateChanged() => _ = InvokeAsync(StateHasChanged);

    public virtual void Dispose()
    {
        State.Changed -= OnStateChanged;
        GC.SuppressFinalize(this);
    }
}
