using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using IyaElepoApp.Models.ConData;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IyaElepoApp.Models;

namespace IyaElepoApp.Pages
{
    public partial class EditProductSupplyComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected ConDataService ConData { get; set; }

        [Parameter]
        public dynamic SupplyID { get; set; }

        IyaElepoApp.Models.ConData.ProductSupply _productsupply;
        protected IyaElepoApp.Models.ConData.ProductSupply productsupply
        {
            get
            {
                return _productsupply;
            }
            set
            {
                if (!object.Equals(_productsupply, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "productsupply", NewValue = value, OldValue = _productsupply };
                    _productsupply = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<IyaElepoApp.Models.ConData.ProductSupplier> _getProductSuppliersResult;
        protected IEnumerable<IyaElepoApp.Models.ConData.ProductSupplier> getProductSuppliersResult
        {
            get
            {
                return _getProductSuppliersResult;
            }
            set
            {
                if (!object.Equals(_getProductSuppliersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getProductSuppliersResult", NewValue = value, OldValue = _getProductSuppliersResult };
                    _getProductSuppliersResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<IyaElepoApp.Models.ConData.Product> _getProductsResult;
        protected IEnumerable<IyaElepoApp.Models.ConData.Product> getProductsResult
        {
            get
            {
                return _getProductsResult;
            }
            set
            {
                if (!object.Equals(_getProductsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getProductsResult", NewValue = value, OldValue = _getProductsResult };
                    _getProductsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var conDataGetProductSupplyBySupplyIdResult = await ConData.GetProductSupplyBySupplyId(SupplyID);
            productsupply = conDataGetProductSupplyBySupplyIdResult;

            var conDataGetProductSuppliersResult = await ConData.GetProductSuppliers();
            getProductSuppliersResult = conDataGetProductSuppliersResult;

            var conDataGetProductsResult = await ConData.GetProducts();
            getProductsResult = conDataGetProductsResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(IyaElepoApp.Models.ConData.ProductSupply args)
        {
            await EditSupplyAsync();
        }

        protected async System.Threading.Tasks.Task SupplyDateChange(DateTime? args)
        {
            await ValidateSupplyDateAsync();
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
