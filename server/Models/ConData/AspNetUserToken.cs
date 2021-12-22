using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("AspNetUserTokens", Schema = "dbo")]
  public partial class AspNetUserToken
  {
    [Key]
    public string UserId
    {
      get;
      set;
    }
    [Key]
    public string LoginProvider
    {
      get;
      set;
    }
    [Key]
    public string Name
    {
      get;
      set;
    }
    public string Value
    {
      get;
      set;
    }
  }
}
