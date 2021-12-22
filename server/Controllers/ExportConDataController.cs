using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IyaElepoApp.Data;

namespace IyaElepoApp
{
    public partial class ExportConDataController : ExportController
    {
        private readonly ConDataContext context;
        private readonly ConDataService service;
        public ExportConDataController(ConDataContext context, ConDataService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/ConData/aspnetroles/csv")]
        [HttpGet("/export/ConData/aspnetroles/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetRoles(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetroles/excel")]
        [HttpGet("/export/ConData/aspnetroles/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetRoles(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/aspnetroleclaims/csv")]
        [HttpGet("/export/ConData/aspnetroleclaims/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetRoleClaimsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetRoleClaims(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetroleclaims/excel")]
        [HttpGet("/export/ConData/aspnetroleclaims/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetRoleClaimsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetRoleClaims(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/aspnetusers/csv")]
        [HttpGet("/export/ConData/aspnetusers/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUsers(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetusers/excel")]
        [HttpGet("/export/ConData/aspnetusers/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUsers(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/aspnetuserclaims/csv")]
        [HttpGet("/export/ConData/aspnetuserclaims/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetUserClaimsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserClaims(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetuserclaims/excel")]
        [HttpGet("/export/ConData/aspnetuserclaims/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetUserClaimsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserClaims(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/aspnetuserlogins/csv")]
        [HttpGet("/export/ConData/aspnetuserlogins/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetUserLoginsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserLogins(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetuserlogins/excel")]
        [HttpGet("/export/ConData/aspnetuserlogins/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetUserLoginsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserLogins(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/aspnetuserroles/csv")]
        [HttpGet("/export/ConData/aspnetuserroles/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetUserRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserRoles(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetuserroles/excel")]
        [HttpGet("/export/ConData/aspnetuserroles/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetUserRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserRoles(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/aspnetusertokens/csv")]
        [HttpGet("/export/ConData/aspnetusertokens/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetUserTokensToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserTokens(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetusertokens/excel")]
        [HttpGet("/export/ConData/aspnetusertokens/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAspNetUserTokensToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserTokens(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/customers/csv")]
        [HttpGet("/export/ConData/customers/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCustomersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCustomers(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/customers/excel")]
        [HttpGet("/export/ConData/customers/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCustomersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCustomers(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/customertypes/csv")]
        [HttpGet("/export/ConData/customertypes/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCustomerTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCustomerTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/customertypes/excel")]
        [HttpGet("/export/ConData/customertypes/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCustomerTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCustomerTypes(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/genders/csv")]
        [HttpGet("/export/ConData/genders/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportGendersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetGenders(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/genders/excel")]
        [HttpGet("/export/ConData/genders/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportGendersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetGenders(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/products/csv")]
        [HttpGet("/export/ConData/products/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProducts(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/products/excel")]
        [HttpGet("/export/ConData/products/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProducts(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/productsales/csv")]
        [HttpGet("/export/ConData/productsales/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportProductSalesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProductSales(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/productsales/excel")]
        [HttpGet("/export/ConData/productsales/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportProductSalesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProductSales(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/productsuppliers/csv")]
        [HttpGet("/export/ConData/productsuppliers/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportProductSuppliersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProductSuppliers(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/productsuppliers/excel")]
        [HttpGet("/export/ConData/productsuppliers/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportProductSuppliersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProductSuppliers(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/productsupplies/csv")]
        [HttpGet("/export/ConData/productsupplies/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportProductSuppliesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProductSupplies(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/productsupplies/excel")]
        [HttpGet("/export/ConData/productsupplies/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportProductSuppliesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProductSupplies(), Request.Query), fileName);
        }
        [HttpGet("/export/ConData/producttypes/csv")]
        [HttpGet("/export/ConData/producttypes/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportProductTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProductTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/producttypes/excel")]
        [HttpGet("/export/ConData/producttypes/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportProductTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProductTypes(), Request.Query), fileName);
        }
    }
}
