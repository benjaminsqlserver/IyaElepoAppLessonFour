using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("Customers", Schema = "dbo")]
  public partial class Customer
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int64 CustomerID
    {
      get;
      set;
    }

    public ICollection<ProductSale> ProductSales { get; set; }
    public string CustomerName
    {
      get;
      set;
    }
    public int CustomerTypeID
    {
      get;
      set;
    }
    public CustomerType CustomerType { get; set; }
    public string Address
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
