using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("AspNetRoles", Schema = "dbo")]
  public partial class AspNetRole
  {
    [Key]
    public string Id
    {
      get;
      set;
    }

    public ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
    public ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    public string ConcurrencyStamp
    {
      get;
      set;
    }
    public string Name
    {
      get;
      set;
    }
    public string NormalizedName
    {
      get;
      set;
    }
  }
}
