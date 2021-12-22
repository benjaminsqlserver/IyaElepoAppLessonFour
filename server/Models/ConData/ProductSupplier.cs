using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("ProductSuppliers", Schema = "dbo")]
  public partial class ProductSupplier
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int64 ProductSupplierID
    {
      get;
      set;
    }

    public ICollection<ProductSupply> ProductSupplies { get; set; }
    public string ProductSupplierName
    {
      get;
      set;
    }
    public string OfficeAddress
    {
      get;
      set;
    }
    public string Email
    {
      get;
      set;
    }
    public string PhoneNumber
    {
      get;
      set;
    }
    public string ContactPerson
    {
      get;
      set;
    }
  }
}
