using EXCSLA.Shared.UI.Blazor.LoadingSpinner.Enums;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EXCSLA.Shared.UI.Blazor.DataTable
{
    public partial class DataTable<TItem>
    {
        [Parameter] public RenderFragment HeadingContentTemplate { get; set; }
        [Parameter] public RenderFragment TableHeaderContentTemplate { get; set; }
        [Parameter] public RenderFragment<TItem> ItemTemplate { get; set; }
        [Parameter] public RenderFragment TableFooterContentTemplate { get; set; }
        [Parameter] public RenderFragment DeleteModalItemTemplate { get; set; }

        [Parameter] public IReadOnlyList<TItem> Items { get; set; }

        [Parameter] public EventCallback<object> OnDelete { get; set; }
        [Parameter] public EventCallback<object> OnEdit { get; set; }
        [Parameter] public EventCallback<object> OnCreate { get; set; }

        [Parameter] public bool IsDeleteModal { get; set; } = false;
        [Parameter] public bool IsStriped { get; set; } = false;
        [Parameter] public string Title { get; set; }
        [Parameter] public SpinnerType SpinnerType { get; set; } = SpinnerType.BT;

        protected override Task OnInitializedAsync()
        {

            return base.OnInitializedAsync();
        }

        protected void HandleDelete(TItem item)
        {
            OnDelete.InvokeAsync(item);
        }

        protected void HandleEdit(TItem item)
        {
            OnEdit.InvokeAsync(item);
        }

        protected void HandleCreate()
        {
            OnCreate.InvokeAsync(Activator.CreateInstance(typeof(TItem)));
        }
    }
}
