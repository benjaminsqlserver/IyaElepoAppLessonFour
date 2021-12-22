using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("ProductTypes", Schema = "dbo")]
  public partial class ProductType
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductTypeID
    {
      get;
      set;
    }

    public ICollection<Product> Products { get; set; }
    public string ProductTypeName
    {
      get;
      set;
    }
  }
}
