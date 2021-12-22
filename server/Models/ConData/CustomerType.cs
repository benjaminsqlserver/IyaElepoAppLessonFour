using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("CustomerTypes", Schema = "dbo")]
  public partial class CustomerType
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerTypeID
    {
      get;
      set;
    }

    public ICollection<Customer> Customers { get; set; }
    public string CustomerTypeName
    {
      get;
      set;
    }
  }
}
