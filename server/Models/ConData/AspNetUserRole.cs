using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("AspNetUserRoles", Schema = "dbo")]
  public partial class AspNetUserRole
  {
    [Key]
    public string UserId
    {
      get;
      set;
    }
    public AspNetUser AspNetUser { get; set; }
    [Key]
    public string RoleId
    {
      get;
      set;
    }
    public AspNetRole AspNetRole { get; set; }
  }
}
