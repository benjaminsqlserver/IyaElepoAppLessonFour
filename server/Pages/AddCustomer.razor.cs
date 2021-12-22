using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;

namespace IyaElepoApp.Pages
{
    public partial class AddCustomerComponent
    {
        private async System.Threading.Tasks.Task AddNewCustomerAsync()
        {
            try
            {
               IyaElepoApp.Models.ConData.Customer conDataCreateCustomerResult = await ConData.CreateCustomer(customer);

                if(conDataCreateCustomerResult!=null)
                {
                    if(conDataCreateCustomerResult.CustomerID > 0)//successful customer creation
                    {
                       // string[] roles = { "Customer" };
                        var newUser = new Models.ApplicationUser { UserName = customer.Email, Email = customer.Email, Password = "Password_1", ConfirmPassword = "Password_1" };
                        newUser.RoleNames = new List<string>();
                        newUser.RoleNames.Append("Customer");
                        var createUserResult = await Security.CreateUser(newUser);

                        if(!string.IsNullOrEmpty(createUserResult.Id))
                        {
                            string[] customerNames = customer.CustomerName.Split(" ");
                            var existingUser = await ConData.GetAspNetUserById(createUserResult.Id);
                            if(existingUser!=null)
                            {
                                if(!string.IsNullOrEmpty(existingUser.Id))
                                {
                                    existingUser.GenderID = GenderID;
                                    if(customerNames.Length > 0 && customerNames.Length < 2)
                                    {
                                        existingUser.FirstName = customerNames[0];
                                    }
                                   else if (customerNames.Length > 0 && customerNames.Length < 3)
                                    {
                                        existingUser.FirstName = customerNames[0];
                                        existingUser.Surname = customerNames[1];
                                    }
                                    else if (customerNames.Length > 2)
                                    {
                                        existingUser.FirstName = customerNames[0];
                                        existingUser.MiddleName = customerNames[1];
                                        existingUser.Surname = customerNames[2];
                                    }

                                    existingUser.CustomerID = conDataCreateCustomerResult.CustomerID;
                                    
                                    var updateResult = await ConData.UpdateAspNetUser(createUserResult.Id, existingUser);
                                }
                                else
                                {
                                    NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = "Customer Creation Error", Duration = 7000 });
                                }
                            }
                            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Success, Summary = $"Successful Customer Creation ", Detail = "Successful Customer Creation", Duration = 7000 });
                            // DialogService.Close(customer);
                            UriHelper.NavigateTo("customers", true);
                        }
                        else
            {
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = "Customer Creation Error", Duration = 7000 });
            }

        }
                    else
                    {
                        NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = "Customer Creation Error", Duration = 7000 });
                    }
                }
               
            }
            catch (System.Exception conDataCreateCustomerException)
            {
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail =conDataCreateCustomerException.Message,Duration=7000 });
            }
        }
    }
}
