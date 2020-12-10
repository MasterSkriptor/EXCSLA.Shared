using BlazorStrap;
using Microsoft.AspNetCore.Components;

namespace EXCSLA.Shared.UI.Blazor.Modal
{
    public partial class TemplatedModal<TItem>
    {
        protected BSModal Modal { get; set; }

        [Parameter] public bool IsOpen { get; set; } = false;
        [Parameter] public string Title { get; set; }
        [Parameter] public TItem Item { get; set; }
        [Parameter] public RenderFragment<TItem> ItemTemplate { get; set; }
        [Parameter] public EventCallback<object> OnSave { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [Parameter] public Size Size { get; set; } = Size.Medium;
        [Parameter] public Color SaveColor { get; set; } = Color.Primary;
        [Parameter] public string SaveText { get; set; } = "Save";


        public void HandleCancel()
        {
            OnCancel.InvokeAsync(null);
            IsOpen = false;

        }

        public void HandleSave(TItem item)
        {
            OnSave.InvokeAsync(item);
            IsOpen = false;

        }

        public void Show()
        {
            IsOpen = true;
        }

        public void Hide()
        {
            IsOpen = false;
        }

    }
}
