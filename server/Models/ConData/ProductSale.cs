using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("ProductSales", Schema = "dbo")]
  public partial class ProductSale
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int64 ProductSaleID
    {
      get;
      set;
    }
    public DateTime SalesDate
    {
      get;
      set;
    }
    public Int64 CustomerID
    {
      get;
      set;
    }
    public Customer Customer { get; set; }
    public Int64 ProductID
    {
      get;
      set;
    }
    public Product Product { get; set; }
    public decimal QuantityInLitres
    {
      get;
      set;
    }
  }
}
