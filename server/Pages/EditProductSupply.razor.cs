using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;

namespace IyaElepoApp.Pages
{
    public partial class EditProductSupplyComponent
    {
        //custom method to validate products supply date
        private async Task<bool> ValidateSupplyDateAsync()
        {
            var passedValidation = false;
            try
            {
                if (productsupply.SupplyDate > DateTime.Now)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Validation Error!", "Product Supply Date Can Not Be In The Future!", 7000);

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

        //custom method to edit product supply
        private async System.Threading.Tasks.Task EditSupplyAsync()
        {
            try
            {
                if(await ValidateSupplyDateAsync()==true)
                {
                    var conDataUpdateProductSupplyResult = await ConData.UpdateProductSupply(SupplyID, productsupply);
                    //DialogService.Close(productsupply);
                    UriHelper.NavigateTo("product-supplies", true);
                }
               
            }
            catch (System.Exception conDataUpdateProductSupplyException)
            {
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to update ProductSupply" });
            }
        }
    }
}
