using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("AspNetUserClaims", Schema = "dbo")]
  public partial class AspNetUserClaim
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }
    public string ClaimType
    {
      get;
      set;
    }
    public string ClaimValue
    {
      get;
      set;
    }
    public string UserId
    {
      get;
      set;
    }
    public AspNetUser AspNetUser { get; set; }
  }
}
