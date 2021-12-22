using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("ProductSupplies", Schema = "dbo")]
  public partial class ProductSupply
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int64 SupplyID
    {
      get;
      set;
    }
    public DateTime SupplyDate
    {
      get;
      set;
    }
    public Int64 ProductSupplierID
    {
      get;
      set;
    }
    public ProductSupplier ProductSupplier { get; set; }
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
