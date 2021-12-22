using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("Products", Schema = "dbo")]
  public partial class Product
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int64 ProductID
    {
      get;
      set;
    }

    public ICollection<ProductSupply> ProductSupplies { get; set; }
    public ICollection<ProductSale> ProductSales { get; set; }
    public string ProductName
    {
      get;
      set;
    }
    public int ProductTypeID
    {
      get;
      set;
    }
    public ProductType ProductType { get; set; }
    public string Picture
    {
      get;
      set;
    }
  }
}
