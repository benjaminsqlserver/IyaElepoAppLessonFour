using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using IyaElepoApp.Data;

namespace IyaElepoApp
{
    public partial class ConDataService
    {
        ConDataContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly ConDataContext context;
        private readonly NavigationManager navigationManager;

        public ConDataService(ConDataContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportAspNetRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetRolesRead(ref IQueryable<Models.ConData.AspNetRole> items);

        public async Task<IQueryable<Models.ConData.AspNetRole>> GetAspNetRoles(Query query = null)
        {
            var items = Context.AspNetRoles.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAspNetRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetRoleCreated(Models.ConData.AspNetRole item);
        partial void OnAfterAspNetRoleCreated(Models.ConData.AspNetRole item);

        public async Task<Models.ConData.AspNetRole> CreateAspNetRole(Models.ConData.AspNetRole aspNetRole)
        {
            OnAspNetRoleCreated(aspNetRole);

            var existingItem = Context.AspNetRoles
                              .Where(i => i.Id == aspNetRole.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetRoles.Add(aspNetRole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspNetRole).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetRoleCreated(aspNetRole);

            return aspNetRole;
        }
        public async Task ExportAspNetRoleClaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetRoleClaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetRoleClaimsRead(ref IQueryable<Models.ConData.AspNetRoleClaim> items);

        public async Task<IQueryable<Models.ConData.AspNetRoleClaim>> GetAspNetRoleClaims(Query query = null)
        {
            var items = Context.AspNetRoleClaims.AsQueryable();

            items = items.Include(i => i.AspNetRole);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAspNetRoleClaimsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetRoleClaimCreated(Models.ConData.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimCreated(Models.ConData.AspNetRoleClaim item);

        public async Task<Models.ConData.AspNetRoleClaim> CreateAspNetRoleClaim(Models.ConData.AspNetRoleClaim aspNetRoleClaim)
        {
            OnAspNetRoleClaimCreated(aspNetRoleClaim);

            var existingItem = Context.AspNetRoleClaims
                              .Where(i => i.Id == aspNetRoleClaim.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetRoleClaims.Add(aspNetRoleClaim);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspNetRoleClaim).State = EntityState.Detached;
                aspNetRoleClaim.AspNetRole = null;
                throw;
            }

            OnAfterAspNetRoleClaimCreated(aspNetRoleClaim);

            return aspNetRoleClaim;
        }
        public async Task ExportAspNetUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUsersRead(ref IQueryable<Models.ConData.AspNetUser> items);

        public async Task<IQueryable<Models.ConData.AspNetUser>> GetAspNetUsers(Query query = null)
        {
            var items = Context.AspNetUsers.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAspNetUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserCreated(Models.ConData.AspNetUser item);
        partial void OnAfterAspNetUserCreated(Models.ConData.AspNetUser item);

        public async Task<Models.ConData.AspNetUser> CreateAspNetUser(Models.ConData.AspNetUser aspNetUser)
        {
            OnAspNetUserCreated(aspNetUser);

            var existingItem = Context.AspNetUsers
                              .Where(i => i.Id == aspNetUser.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUsers.Add(aspNetUser);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspNetUser).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserCreated(aspNetUser);

            return aspNetUser;
        }
        public async Task ExportAspNetUserClaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserClaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserClaimsRead(ref IQueryable<Models.ConData.AspNetUserClaim> items);

        public async Task<IQueryable<Models.ConData.AspNetUserClaim>> GetAspNetUserClaims(Query query = null)
        {
            var items = Context.AspNetUserClaims.AsQueryable();

            items = items.Include(i => i.AspNetUser);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAspNetUserClaimsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserClaimCreated(Models.ConData.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimCreated(Models.ConData.AspNetUserClaim item);

        public async Task<Models.ConData.AspNetUserClaim> CreateAspNetUserClaim(Models.ConData.AspNetUserClaim aspNetUserClaim)
        {
            OnAspNetUserClaimCreated(aspNetUserClaim);

            var existingItem = Context.AspNetUserClaims
                              .Where(i => i.Id == aspNetUserClaim.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserClaims.Add(aspNetUserClaim);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspNetUserClaim).State = EntityState.Detached;
                aspNetUserClaim.AspNetUser = null;
                throw;
            }

            OnAfterAspNetUserClaimCreated(aspNetUserClaim);

            return aspNetUserClaim;
        }
        public async Task ExportAspNetUserLoginsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserLoginsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserLoginsRead(ref IQueryable<Models.ConData.AspNetUserLogin> items);

        public async Task<IQueryable<Models.ConData.AspNetUserLogin>> GetAspNetUserLogins(Query query = null)
        {
            var items = Context.AspNetUserLogins.AsQueryable();

            items = items.Include(i => i.AspNetUser);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAspNetUserLoginsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserLoginCreated(Models.ConData.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginCreated(Models.ConData.AspNetUserLogin item);

        public async Task<Models.ConData.AspNetUserLogin> CreateAspNetUserLogin(Models.ConData.AspNetUserLogin aspNetUserLogin)
        {
            OnAspNetUserLoginCreated(aspNetUserLogin);

            var existingItem = Context.AspNetUserLogins
                              .Where(i => i.LoginProvider == aspNetUserLogin.LoginProvider && i.ProviderKey == aspNetUserLogin.ProviderKey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserLogins.Add(aspNetUserLogin);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspNetUserLogin).State = EntityState.Detached;
                aspNetUserLogin.AspNetUser = null;
                throw;
            }

            OnAfterAspNetUserLoginCreated(aspNetUserLogin);

            return aspNetUserLogin;
        }
        public async Task ExportAspNetUserRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserRolesRead(ref IQueryable<Models.ConData.AspNetUserRole> items);

        public async Task<IQueryable<Models.ConData.AspNetUserRole>> GetAspNetUserRoles(Query query = null)
        {
            var items = Context.AspNetUserRoles.AsQueryable();

            items = items.Include(i => i.AspNetUser);

            items = items.Include(i => i.AspNetRole);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAspNetUserRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserRoleCreated(Models.ConData.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleCreated(Models.ConData.AspNetUserRole item);

        public async Task<Models.ConData.AspNetUserRole> CreateAspNetUserRole(Models.ConData.AspNetUserRole aspNetUserRole)
        {
            OnAspNetUserRoleCreated(aspNetUserRole);

            var existingItem = Context.AspNetUserRoles
                              .Where(i => i.UserId == aspNetUserRole.UserId && i.RoleId == aspNetUserRole.RoleId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserRoles.Add(aspNetUserRole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspNetUserRole).State = EntityState.Detached;
                aspNetUserRole.AspNetUser = null;
                aspNetUserRole.AspNetRole = null;
                throw;
            }

            OnAfterAspNetUserRoleCreated(aspNetUserRole);

            return aspNetUserRole;
        }
        public async Task ExportAspNetUserTokensToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserTokensToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserTokensRead(ref IQueryable<Models.ConData.AspNetUserToken> items);

        public async Task<IQueryable<Models.ConData.AspNetUserToken>> GetAspNetUserTokens(Query query = null)
        {
            var items = Context.AspNetUserTokens.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAspNetUserTokensRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserTokenCreated(Models.ConData.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenCreated(Models.ConData.AspNetUserToken item);

        public async Task<Models.ConData.AspNetUserToken> CreateAspNetUserToken(Models.ConData.AspNetUserToken aspNetUserToken)
        {
            OnAspNetUserTokenCreated(aspNetUserToken);

            var existingItem = Context.AspNetUserTokens
                              .Where(i => i.UserId == aspNetUserToken.UserId && i.LoginProvider == aspNetUserToken.LoginProvider && i.Name == aspNetUserToken.Name)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserTokens.Add(aspNetUserToken);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspNetUserToken).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserTokenCreated(aspNetUserToken);

            return aspNetUserToken;
        }
        public async Task ExportCustomersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/customers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/customers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCustomersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/customers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/customers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCustomersRead(ref IQueryable<Models.ConData.Customer> items);

        public async Task<IQueryable<Models.ConData.Customer>> GetCustomers(Query query = null)
        {
            var items = Context.Customers.AsQueryable();

            items = items.Include(i => i.CustomerType);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCustomersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCustomerCreated(Models.ConData.Customer item);
        partial void OnAfterCustomerCreated(Models.ConData.Customer item);

        public async Task<Models.ConData.Customer> CreateCustomer(Models.ConData.Customer customer)
        {
            OnCustomerCreated(customer);

            var existingItem = Context.Customers
                              .Where(i => i.CustomerID == customer.CustomerID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Customers.Add(customer);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(customer).State = EntityState.Detached;
                customer.CustomerType = null;
                throw;
            }

            OnAfterCustomerCreated(customer);

            return customer;
        }
        public async Task ExportCustomerTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/customertypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/customertypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCustomerTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/customertypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/customertypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCustomerTypesRead(ref IQueryable<Models.ConData.CustomerType> items);

        public async Task<IQueryable<Models.ConData.CustomerType>> GetCustomerTypes(Query query = null)
        {
            var items = Context.CustomerTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCustomerTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCustomerTypeCreated(Models.ConData.CustomerType item);
        partial void OnAfterCustomerTypeCreated(Models.ConData.CustomerType item);

        public async Task<Models.ConData.CustomerType> CreateCustomerType(Models.ConData.CustomerType customerType)
        {
            OnCustomerTypeCreated(customerType);

            var existingItem = Context.CustomerTypes
                              .Where(i => i.CustomerTypeID == customerType.CustomerTypeID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CustomerTypes.Add(customerType);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(customerType).State = EntityState.Detached;
                throw;
            }

            OnAfterCustomerTypeCreated(customerType);

            return customerType;
        }
        public async Task ExportGendersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/genders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/genders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportGendersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/genders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/genders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGendersRead(ref IQueryable<Models.ConData.Gender> items);

        public async Task<IQueryable<Models.ConData.Gender>> GetGenders(Query query = null)
        {
            var items = Context.Genders.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnGendersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGenderCreated(Models.ConData.Gender item);
        partial void OnAfterGenderCreated(Models.ConData.Gender item);

        public async Task<Models.ConData.Gender> CreateGender(Models.ConData.Gender gender)
        {
            OnGenderCreated(gender);

            var existingItem = Context.Genders
                              .Where(i => i.GenderID == gender.GenderID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Genders.Add(gender);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(gender).State = EntityState.Detached;
                throw;
            }

            OnAfterGenderCreated(gender);

            return gender;
        }
        public async Task ExportProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductsRead(ref IQueryable<Models.ConData.Product> items);

        public async Task<IQueryable<Models.ConData.Product>> GetProducts(Query query = null)
        {
            var items = Context.Products.AsQueryable();

            items = items.Include(i => i.ProductType);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnProductsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductCreated(Models.ConData.Product item);
        partial void OnAfterProductCreated(Models.ConData.Product item);

        public async Task<Models.ConData.Product> CreateProduct(Models.ConData.Product product)
        {
            OnProductCreated(product);

            var existingItem = Context.Products
                              .Where(i => i.ProductID == product.ProductID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Products.Add(product);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(product).State = EntityState.Detached;
                product.ProductType = null;
                throw;
            }

            OnAfterProductCreated(product);

            return product;
        }
        public async Task ExportProductSalesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/productsales/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/productsales/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductSalesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/productsales/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/productsales/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductSalesRead(ref IQueryable<Models.ConData.ProductSale> items);

        public async Task<IQueryable<Models.ConData.ProductSale>> GetProductSales(Query query = null)
        {
            var items = Context.ProductSales.AsQueryable();

            items = items.Include(i => i.Customer);

            items = items.Include(i => i.Product);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnProductSalesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductSaleCreated(Models.ConData.ProductSale item);
        partial void OnAfterProductSaleCreated(Models.ConData.ProductSale item);

        public async Task<Models.ConData.ProductSale> CreateProductSale(Models.ConData.ProductSale productSale)
        {
            OnProductSaleCreated(productSale);

            var existingItem = Context.ProductSales
                              .Where(i => i.ProductSaleID == productSale.ProductSaleID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ProductSales.Add(productSale);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(productSale).State = EntityState.Detached;
                productSale.Customer = null;
                productSale.Product = null;
                throw;
            }

            OnAfterProductSaleCreated(productSale);

            return productSale;
        }
        public async Task ExportProductSuppliersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/productsuppliers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/productsuppliers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductSuppliersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/productsuppliers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/productsuppliers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductSuppliersRead(ref IQueryable<Models.ConData.ProductSupplier> items);

        public async Task<IQueryable<Models.ConData.ProductSupplier>> GetProductSuppliers(Query query = null)
        {
            var items = Context.ProductSuppliers.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnProductSuppliersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductSupplierCreated(Models.ConData.ProductSupplier item);
        partial void OnAfterProductSupplierCreated(Models.ConData.ProductSupplier item);

        public async Task<Models.ConData.ProductSupplier> CreateProductSupplier(Models.ConData.ProductSupplier productSupplier)
        {
            OnProductSupplierCreated(productSupplier);

            var existingItem = Context.ProductSuppliers
                              .Where(i => i.ProductSupplierID == productSupplier.ProductSupplierID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ProductSuppliers.Add(productSupplier);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(productSupplier).State = EntityState.Detached;
                throw;
            }

            OnAfterProductSupplierCreated(productSupplier);

            return productSupplier;
        }
        public async Task ExportProductSuppliesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/productsupplies/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/productsupplies/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductSuppliesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/productsupplies/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/productsupplies/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductSuppliesRead(ref IQueryable<Models.ConData.ProductSupply> items);

        public async Task<IQueryable<Models.ConData.ProductSupply>> GetProductSupplies(Query query = null)
        {
            var items = Context.ProductSupplies.AsQueryable();

            items = items.Include(i => i.ProductSupplier);

            items = items.Include(i => i.Product);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnProductSuppliesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductSupplyCreated(Models.ConData.ProductSupply item);
        partial void OnAfterProductSupplyCreated(Models.ConData.ProductSupply item);

        public async Task<Models.ConData.ProductSupply> CreateProductSupply(Models.ConData.ProductSupply productSupply)
        {
            OnProductSupplyCreated(productSupply);

            var existingItem = Context.ProductSupplies
                              .Where(i => i.SupplyID == productSupply.SupplyID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ProductSupplies.Add(productSupply);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(productSupply).State = EntityState.Detached;
                productSupply.ProductSupplier = null;
                productSupply.Product = null;
                throw;
            }

            OnAfterProductSupplyCreated(productSupply);

            return productSupply;
        }
        public async Task ExportProductTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/producttypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/producttypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/producttypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/producttypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductTypesRead(ref IQueryable<Models.ConData.ProductType> items);

        public async Task<IQueryable<Models.ConData.ProductType>> GetProductTypes(Query query = null)
        {
            var items = Context.ProductTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnProductTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductTypeCreated(Models.ConData.ProductType item);
        partial void OnAfterProductTypeCreated(Models.ConData.ProductType item);

        public async Task<Models.ConData.ProductType> CreateProductType(Models.ConData.ProductType productType)
        {
            OnProductTypeCreated(productType);

            var existingItem = Context.ProductTypes
                              .Where(i => i.ProductTypeID == productType.ProductTypeID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ProductTypes.Add(productType);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(productType).State = EntityState.Detached;
                throw;
            }

            OnAfterProductTypeCreated(productType);

            return productType;
        }

        partial void OnAspNetRoleDeleted(Models.ConData.AspNetRole item);
        partial void OnAfterAspNetRoleDeleted(Models.ConData.AspNetRole item);

        public async Task<Models.ConData.AspNetRole> DeleteAspNetRole(string id)
        {
            var itemToDelete = Context.AspNetRoles
                              .Where(i => i.Id == id)
                              .Include(i => i.AspNetRoleClaims)
                              .Include(i => i.AspNetUserRoles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetRoleDeleted(itemToDelete);

            Context.AspNetRoles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetRoleDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnAspNetRoleGet(Models.ConData.AspNetRole item);

        public async Task<Models.ConData.AspNetRole> GetAspNetRoleById(string id)
        {
            var items = Context.AspNetRoles
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.AspNetRole> CancelAspNetRoleChanges(Models.ConData.AspNetRole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetRoleUpdated(Models.ConData.AspNetRole item);
        partial void OnAfterAspNetRoleUpdated(Models.ConData.AspNetRole item);

        public async Task<Models.ConData.AspNetRole> UpdateAspNetRole(string id, Models.ConData.AspNetRole aspNetRole)
        {
            OnAspNetRoleUpdated(aspNetRole);

            var itemToUpdate = Context.AspNetRoles
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspNetRole);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterAspNetRoleUpdated(aspNetRole);

            return aspNetRole;
        }

        partial void OnAspNetRoleClaimDeleted(Models.ConData.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimDeleted(Models.ConData.AspNetRoleClaim item);

        public async Task<Models.ConData.AspNetRoleClaim> DeleteAspNetRoleClaim(int? id)
        {
            var itemToDelete = Context.AspNetRoleClaims
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetRoleClaimDeleted(itemToDelete);

            Context.AspNetRoleClaims.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetRoleClaimDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnAspNetRoleClaimGet(Models.ConData.AspNetRoleClaim item);

        public async Task<Models.ConData.AspNetRoleClaim> GetAspNetRoleClaimById(int? id)
        {
            var items = Context.AspNetRoleClaims
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AspNetRole);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetRoleClaimGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.AspNetRoleClaim> CancelAspNetRoleClaimChanges(Models.ConData.AspNetRoleClaim item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetRoleClaimUpdated(Models.ConData.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimUpdated(Models.ConData.AspNetRoleClaim item);

        public async Task<Models.ConData.AspNetRoleClaim> UpdateAspNetRoleClaim(int? id, Models.ConData.AspNetRoleClaim aspNetRoleClaim)
        {
            OnAspNetRoleClaimUpdated(aspNetRoleClaim);

            var itemToUpdate = Context.AspNetRoleClaims
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspNetRoleClaim);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterAspNetRoleClaimUpdated(aspNetRoleClaim);

            return aspNetRoleClaim;
        }

        partial void OnAspNetUserDeleted(Models.ConData.AspNetUser item);
        partial void OnAfterAspNetUserDeleted(Models.ConData.AspNetUser item);

        public async Task<Models.ConData.AspNetUser> DeleteAspNetUser(string id)
        {
            var itemToDelete = Context.AspNetUsers
                              .Where(i => i.Id == id)
                              .Include(i => i.AspNetUserClaims)
                              .Include(i => i.AspNetUserLogins)
                              .Include(i => i.AspNetUserRoles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserDeleted(itemToDelete);

            Context.AspNetUsers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnAspNetUserGet(Models.ConData.AspNetUser item);

        public async Task<Models.ConData.AspNetUser> GetAspNetUserById(string id)
        {
            var items = Context.AspNetUsers
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.AspNetUser> CancelAspNetUserChanges(Models.ConData.AspNetUser item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserUpdated(Models.ConData.AspNetUser item);
        partial void OnAfterAspNetUserUpdated(Models.ConData.AspNetUser item);

        public async Task<Models.ConData.AspNetUser> UpdateAspNetUser(string id, Models.ConData.AspNetUser aspNetUser)
        {
            OnAspNetUserUpdated(aspNetUser);

            var itemToUpdate = Context.AspNetUsers
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspNetUser);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterAspNetUserUpdated(aspNetUser);

            return aspNetUser;
        }

        partial void OnAspNetUserClaimDeleted(Models.ConData.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimDeleted(Models.ConData.AspNetUserClaim item);

        public async Task<Models.ConData.AspNetUserClaim> DeleteAspNetUserClaim(int? id)
        {
            var itemToDelete = Context.AspNetUserClaims
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserClaimDeleted(itemToDelete);

            Context.AspNetUserClaims.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserClaimDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnAspNetUserClaimGet(Models.ConData.AspNetUserClaim item);

        public async Task<Models.ConData.AspNetUserClaim> GetAspNetUserClaimById(int? id)
        {
            var items = Context.AspNetUserClaims
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AspNetUser);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserClaimGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.AspNetUserClaim> CancelAspNetUserClaimChanges(Models.ConData.AspNetUserClaim item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserClaimUpdated(Models.ConData.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimUpdated(Models.ConData.AspNetUserClaim item);

        public async Task<Models.ConData.AspNetUserClaim> UpdateAspNetUserClaim(int? id, Models.ConData.AspNetUserClaim aspNetUserClaim)
        {
            OnAspNetUserClaimUpdated(aspNetUserClaim);

            var itemToUpdate = Context.AspNetUserClaims
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspNetUserClaim);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterAspNetUserClaimUpdated(aspNetUserClaim);

            return aspNetUserClaim;
        }

        partial void OnAspNetUserLoginDeleted(Models.ConData.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginDeleted(Models.ConData.AspNetUserLogin item);

        public async Task<Models.ConData.AspNetUserLogin> DeleteAspNetUserLogin(string loginProvider, string providerKey)
        {
            var itemToDelete = Context.AspNetUserLogins
                              .Where(i => i.LoginProvider == loginProvider && i.ProviderKey == providerKey)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserLoginDeleted(itemToDelete);

            Context.AspNetUserLogins.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserLoginDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnAspNetUserLoginGet(Models.ConData.AspNetUserLogin item);

        public async Task<Models.ConData.AspNetUserLogin> GetAspNetUserLoginByLoginProviderAndProviderKey(string loginProvider, string providerKey)
        {
            var items = Context.AspNetUserLogins
                              .AsNoTracking()
                              .Where(i => i.LoginProvider == loginProvider && i.ProviderKey == providerKey);

            items = items.Include(i => i.AspNetUser);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserLoginGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.AspNetUserLogin> CancelAspNetUserLoginChanges(Models.ConData.AspNetUserLogin item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserLoginUpdated(Models.ConData.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginUpdated(Models.ConData.AspNetUserLogin item);

        public async Task<Models.ConData.AspNetUserLogin> UpdateAspNetUserLogin(string loginProvider, string providerKey, Models.ConData.AspNetUserLogin aspNetUserLogin)
        {
            OnAspNetUserLoginUpdated(aspNetUserLogin);

            var itemToUpdate = Context.AspNetUserLogins
                              .Where(i => i.LoginProvider == loginProvider && i.ProviderKey == providerKey)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspNetUserLogin);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterAspNetUserLoginUpdated(aspNetUserLogin);

            return aspNetUserLogin;
        }

        partial void OnAspNetUserRoleDeleted(Models.ConData.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleDeleted(Models.ConData.AspNetUserRole item);

        public async Task<Models.ConData.AspNetUserRole> DeleteAspNetUserRole(string userId, string roleId)
        {
            var itemToDelete = Context.AspNetUserRoles
                              .Where(i => i.UserId == userId && i.RoleId == roleId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserRoleDeleted(itemToDelete);

            Context.AspNetUserRoles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserRoleDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnAspNetUserRoleGet(Models.ConData.AspNetUserRole item);

        public async Task<Models.ConData.AspNetUserRole> GetAspNetUserRoleByUserIdAndRoleId(string userId, string roleId)
        {
            var items = Context.AspNetUserRoles
                              .AsNoTracking()
                              .Where(i => i.UserId == userId && i.RoleId == roleId);

            items = items.Include(i => i.AspNetUser);

            items = items.Include(i => i.AspNetRole);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.AspNetUserRole> CancelAspNetUserRoleChanges(Models.ConData.AspNetUserRole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserRoleUpdated(Models.ConData.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleUpdated(Models.ConData.AspNetUserRole item);

        public async Task<Models.ConData.AspNetUserRole> UpdateAspNetUserRole(string userId, string roleId, Models.ConData.AspNetUserRole aspNetUserRole)
        {
            OnAspNetUserRoleUpdated(aspNetUserRole);

            var itemToUpdate = Context.AspNetUserRoles
                              .Where(i => i.UserId == userId && i.RoleId == roleId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspNetUserRole);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterAspNetUserRoleUpdated(aspNetUserRole);

            return aspNetUserRole;
        }

        partial void OnAspNetUserTokenDeleted(Models.ConData.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenDeleted(Models.ConData.AspNetUserToken item);

        public async Task<Models.ConData.AspNetUserToken> DeleteAspNetUserToken(string userId, string loginProvider, string name)
        {
            var itemToDelete = Context.AspNetUserTokens
                              .Where(i => i.UserId == userId && i.LoginProvider == loginProvider && i.Name == name)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserTokenDeleted(itemToDelete);

            Context.AspNetUserTokens.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserTokenDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnAspNetUserTokenGet(Models.ConData.AspNetUserToken item);

        public async Task<Models.ConData.AspNetUserToken> GetAspNetUserTokenByUserIdAndLoginProviderAndName(string userId, string loginProvider, string name)
        {
            var items = Context.AspNetUserTokens
                              .AsNoTracking()
                              .Where(i => i.UserId == userId && i.LoginProvider == loginProvider && i.Name == name);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserTokenGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.AspNetUserToken> CancelAspNetUserTokenChanges(Models.ConData.AspNetUserToken item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserTokenUpdated(Models.ConData.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenUpdated(Models.ConData.AspNetUserToken item);

        public async Task<Models.ConData.AspNetUserToken> UpdateAspNetUserToken(string userId, string loginProvider, string name, Models.ConData.AspNetUserToken aspNetUserToken)
        {
            OnAspNetUserTokenUpdated(aspNetUserToken);

            var itemToUpdate = Context.AspNetUserTokens
                              .Where(i => i.UserId == userId && i.LoginProvider == loginProvider && i.Name == name)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspNetUserToken);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterAspNetUserTokenUpdated(aspNetUserToken);

            return aspNetUserToken;
        }

        partial void OnCustomerDeleted(Models.ConData.Customer item);
        partial void OnAfterCustomerDeleted(Models.ConData.Customer item);

        public async Task<Models.ConData.Customer> DeleteCustomer(Int64? customerId)
        {
            var itemToDelete = Context.Customers
                              .Where(i => i.CustomerID == customerId)
                              .Include(i => i.ProductSales)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCustomerDeleted(itemToDelete);

            Context.Customers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCustomerDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCustomerGet(Models.ConData.Customer item);

        public async Task<Models.ConData.Customer> GetCustomerByCustomerId(Int64? customerId)
        {
            var items = Context.Customers
                              .AsNoTracking()
                              .Where(i => i.CustomerID == customerId);

            items = items.Include(i => i.CustomerType);

            var itemToReturn = items.FirstOrDefault();

            OnCustomerGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.Customer> CancelCustomerChanges(Models.ConData.Customer item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCustomerUpdated(Models.ConData.Customer item);
        partial void OnAfterCustomerUpdated(Models.ConData.Customer item);

        public async Task<Models.ConData.Customer> UpdateCustomer(Int64? customerId, Models.ConData.Customer customer)
        {
            OnCustomerUpdated(customer);

            var itemToUpdate = Context.Customers
                              .Where(i => i.CustomerID == customerId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(customer);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCustomerUpdated(customer);

            return customer;
        }

        partial void OnCustomerTypeDeleted(Models.ConData.CustomerType item);
        partial void OnAfterCustomerTypeDeleted(Models.ConData.CustomerType item);

        public async Task<Models.ConData.CustomerType> DeleteCustomerType(int? customerTypeId)
        {
            var itemToDelete = Context.CustomerTypes
                              .Where(i => i.CustomerTypeID == customerTypeId)
                              .Include(i => i.Customers)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCustomerTypeDeleted(itemToDelete);

            Context.CustomerTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCustomerTypeDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCustomerTypeGet(Models.ConData.CustomerType item);

        public async Task<Models.ConData.CustomerType> GetCustomerTypeByCustomerTypeId(int? customerTypeId)
        {
            var items = Context.CustomerTypes
                              .AsNoTracking()
                              .Where(i => i.CustomerTypeID == customerTypeId);

            var itemToReturn = items.FirstOrDefault();

            OnCustomerTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.CustomerType> CancelCustomerTypeChanges(Models.ConData.CustomerType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCustomerTypeUpdated(Models.ConData.CustomerType item);
        partial void OnAfterCustomerTypeUpdated(Models.ConData.CustomerType item);

        public async Task<Models.ConData.CustomerType> UpdateCustomerType(int? customerTypeId, Models.ConData.CustomerType customerType)
        {
            OnCustomerTypeUpdated(customerType);

            var itemToUpdate = Context.CustomerTypes
                              .Where(i => i.CustomerTypeID == customerTypeId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(customerType);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCustomerTypeUpdated(customerType);

            return customerType;
        }

        partial void OnGenderDeleted(Models.ConData.Gender item);
        partial void OnAfterGenderDeleted(Models.ConData.Gender item);

        public async Task<Models.ConData.Gender> DeleteGender(int? genderId)
        {
            var itemToDelete = Context.Genders
                              .Where(i => i.GenderID == genderId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnGenderDeleted(itemToDelete);

            Context.Genders.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterGenderDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnGenderGet(Models.ConData.Gender item);

        public async Task<Models.ConData.Gender> GetGenderByGenderId(int? genderId)
        {
            var items = Context.Genders
                              .AsNoTracking()
                              .Where(i => i.GenderID == genderId);

            var itemToReturn = items.FirstOrDefault();

            OnGenderGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.Gender> CancelGenderChanges(Models.ConData.Gender item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnGenderUpdated(Models.ConData.Gender item);
        partial void OnAfterGenderUpdated(Models.ConData.Gender item);

        public async Task<Models.ConData.Gender> UpdateGender(int? genderId, Models.ConData.Gender gender)
        {
            OnGenderUpdated(gender);

            var itemToUpdate = Context.Genders
                              .Where(i => i.GenderID == genderId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(gender);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterGenderUpdated(gender);

            return gender;
        }

        partial void OnProductDeleted(Models.ConData.Product item);
        partial void OnAfterProductDeleted(Models.ConData.Product item);

        public async Task<Models.ConData.Product> DeleteProduct(Int64? productId)
        {
            var itemToDelete = Context.Products
                              .Where(i => i.ProductID == productId)
                              .Include(i => i.ProductSupplies)
                              .Include(i => i.ProductSales)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductDeleted(itemToDelete);

            Context.Products.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProductDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnProductGet(Models.ConData.Product item);

        public async Task<Models.ConData.Product> GetProductByProductId(Int64? productId)
        {
            var items = Context.Products
                              .AsNoTracking()
                              .Where(i => i.ProductID == productId);

            items = items.Include(i => i.ProductType);

            var itemToReturn = items.FirstOrDefault();

            OnProductGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.Product> CancelProductChanges(Models.ConData.Product item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProductUpdated(Models.ConData.Product item);
        partial void OnAfterProductUpdated(Models.ConData.Product item);

        public async Task<Models.ConData.Product> UpdateProduct(Int64? productId, Models.ConData.Product product)
        {
            OnProductUpdated(product);

            var itemToUpdate = Context.Products
                              .Where(i => i.ProductID == productId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(product);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterProductUpdated(product);

            return product;
        }

        partial void OnProductSaleDeleted(Models.ConData.ProductSale item);
        partial void OnAfterProductSaleDeleted(Models.ConData.ProductSale item);

        public async Task<Models.ConData.ProductSale> DeleteProductSale(Int64? productSaleId)
        {
            var itemToDelete = Context.ProductSales
                              .Where(i => i.ProductSaleID == productSaleId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductSaleDeleted(itemToDelete);

            Context.ProductSales.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProductSaleDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnProductSaleGet(Models.ConData.ProductSale item);

        public async Task<Models.ConData.ProductSale> GetProductSaleByProductSaleId(Int64? productSaleId)
        {
            var items = Context.ProductSales
                              .AsNoTracking()
                              .Where(i => i.ProductSaleID == productSaleId);

            items = items.Include(i => i.Customer);

            items = items.Include(i => i.Product);

            var itemToReturn = items.FirstOrDefault();

            OnProductSaleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.ProductSale> CancelProductSaleChanges(Models.ConData.ProductSale item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProductSaleUpdated(Models.ConData.ProductSale item);
        partial void OnAfterProductSaleUpdated(Models.ConData.ProductSale item);

        public async Task<Models.ConData.ProductSale> UpdateProductSale(Int64? productSaleId, Models.ConData.ProductSale productSale)
        {
            OnProductSaleUpdated(productSale);

            var itemToUpdate = Context.ProductSales
                              .Where(i => i.ProductSaleID == productSaleId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(productSale);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterProductSaleUpdated(productSale);

            return productSale;
        }

        partial void OnProductSupplierDeleted(Models.ConData.ProductSupplier item);
        partial void OnAfterProductSupplierDeleted(Models.ConData.ProductSupplier item);

        public async Task<Models.ConData.ProductSupplier> DeleteProductSupplier(Int64? productSupplierId)
        {
            var itemToDelete = Context.ProductSuppliers
                              .Where(i => i.ProductSupplierID == productSupplierId)
                              .Include(i => i.ProductSupplies)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductSupplierDeleted(itemToDelete);

            Context.ProductSuppliers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProductSupplierDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnProductSupplierGet(Models.ConData.ProductSupplier item);

        public async Task<Models.ConData.ProductSupplier> GetProductSupplierByProductSupplierId(Int64? productSupplierId)
        {
            var items = Context.ProductSuppliers
                              .AsNoTracking()
                              .Where(i => i.ProductSupplierID == productSupplierId);

            var itemToReturn = items.FirstOrDefault();

            OnProductSupplierGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.ProductSupplier> CancelProductSupplierChanges(Models.ConData.ProductSupplier item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProductSupplierUpdated(Models.ConData.ProductSupplier item);
        partial void OnAfterProductSupplierUpdated(Models.ConData.ProductSupplier item);

        public async Task<Models.ConData.ProductSupplier> UpdateProductSupplier(Int64? productSupplierId, Models.ConData.ProductSupplier productSupplier)
        {
            OnProductSupplierUpdated(productSupplier);

            var itemToUpdate = Context.ProductSuppliers
                              .Where(i => i.ProductSupplierID == productSupplierId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(productSupplier);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterProductSupplierUpdated(productSupplier);

            return productSupplier;
        }

        partial void OnProductSupplyDeleted(Models.ConData.ProductSupply item);
        partial void OnAfterProductSupplyDeleted(Models.ConData.ProductSupply item);

        public async Task<Models.ConData.ProductSupply> DeleteProductSupply(Int64? supplyId)
        {
            var itemToDelete = Context.ProductSupplies
                              .Where(i => i.SupplyID == supplyId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductSupplyDeleted(itemToDelete);

            Context.ProductSupplies.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProductSupplyDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnProductSupplyGet(Models.ConData.ProductSupply item);

        public async Task<Models.ConData.ProductSupply> GetProductSupplyBySupplyId(Int64? supplyId)
        {
            var items = Context.ProductSupplies
                              .AsNoTracking()
                              .Where(i => i.SupplyID == supplyId);

            items = items.Include(i => i.ProductSupplier);

            items = items.Include(i => i.Product);

            var itemToReturn = items.FirstOrDefault();

            OnProductSupplyGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.ProductSupply> CancelProductSupplyChanges(Models.ConData.ProductSupply item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProductSupplyUpdated(Models.ConData.ProductSupply item);
        partial void OnAfterProductSupplyUpdated(Models.ConData.ProductSupply item);

        public async Task<Models.ConData.ProductSupply> UpdateProductSupply(Int64? supplyId, Models.ConData.ProductSupply productSupply)
        {
            OnProductSupplyUpdated(productSupply);

            var itemToUpdate = Context.ProductSupplies
                              .Where(i => i.SupplyID == supplyId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(productSupply);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterProductSupplyUpdated(productSupply);

            return productSupply;
        }

        partial void OnProductTypeDeleted(Models.ConData.ProductType item);
        partial void OnAfterProductTypeDeleted(Models.ConData.ProductType item);

        public async Task<Models.ConData.ProductType> DeleteProductType(int? productTypeId)
        {
            var itemToDelete = Context.ProductTypes
                              .Where(i => i.ProductTypeID == productTypeId)
                              .Include(i => i.Products)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductTypeDeleted(itemToDelete);

            Context.ProductTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProductTypeDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnProductTypeGet(Models.ConData.ProductType item);

        public async Task<Models.ConData.ProductType> GetProductTypeByProductTypeId(int? productTypeId)
        {
            var items = Context.ProductTypes
                              .AsNoTracking()
                              .Where(i => i.ProductTypeID == productTypeId);

            var itemToReturn = items.FirstOrDefault();

            OnProductTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.ConData.ProductType> CancelProductTypeChanges(Models.ConData.ProductType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProductTypeUpdated(Models.ConData.ProductType item);
        partial void OnAfterProductTypeUpdated(Models.ConData.ProductType item);

        public async Task<Models.ConData.ProductType> UpdateProductType(int? productTypeId, Models.ConData.ProductType productType)
        {
            OnProductTypeUpdated(productType);

            var itemToUpdate = Context.ProductTypes
                              .Where(i => i.ProductTypeID == productTypeId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(productType);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterProductTypeUpdated(productType);

            return productType;
        }

       
    }
}
