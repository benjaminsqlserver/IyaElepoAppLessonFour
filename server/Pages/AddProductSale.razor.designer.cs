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
    public partial class AddProductSaleComponent : ComponentBase
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

        IEnumerable<IyaElepoApp.Models.ConData.Customer> _getCustomersResult;
        protected IEnumerable<IyaElepoApp.Models.ConData.Customer> getCustomersResult
        {
            get
            {
                return _getCustomersResult;
            }
            set
            {
                if (!object.Equals(_getCustomersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCustomersResult", NewValue = value, OldValue = _getCustomersResult };
                    _getCustomersResult = value;
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

        IyaElepoApp.Models.ConData.ProductSale _productsale;
        protected IyaElepoApp.Models.ConData.ProductSale productsale
        {
            get
            {
                return _productsale;
            }
            set
            {
                if (!object.Equals(_productsale, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "productsale", NewValue = value, OldValue = _productsale };
                    _productsale = value;
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
            var conDataGetCustomersResult = await ConData.GetCustomers();
            getCustomersResult = conDataGetCustomersResult;

            var conDataGetProductsResult = await ConData.GetProducts();
            getProductsResult = conDataGetProductsResult;

            productsale = new IyaElepoApp.Models.ConData.ProductSale(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(IyaElepoApp.Models.ConData.ProductSale args)
        {
            await AddSalesAsync();
        }

        protected async System.Threading.Tasks.Task SalesDateChange(DateTime? args)
        {
            await ValidateSalesDateAsync();
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
