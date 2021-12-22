using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;

namespace IyaElepoApp.Pages
{
    public partial class EditProductSaleComponent
    {
        //custom method to validate products sales date
        private async Task<bool> ValidateSalesDateAsync()
        {
            var passedValidation = false;
            try
            {
                if (productsale.SalesDate > DateTime.Now)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Validation Error!", "Product Sales Date Can Not Be In The Future!", 7000);

                }
                else//passed validation
                {
                    passedValidation = true;
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "App Error!", ex.Message, 7000);
            }

            return await Task.FromResult(passedValidation);
        }


         private async System.Threading.Tasks.Task EditSalesAsync()
        {
            try
            {
                if(await ValidateSalesDateAsync())
                {
                    var conDataUpdateProductSaleResult = await ConData.UpdateProductSale(ProductSaleID, productsale);
                    // DialogService.Close(productsale);
                    UriHelper.NavigateTo("product-sales", true);
                }
                
            }
            catch (System.Exception conDataUpdateProductSaleException)
            {
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to update ProductSale" });
            }
        }

    }
}
