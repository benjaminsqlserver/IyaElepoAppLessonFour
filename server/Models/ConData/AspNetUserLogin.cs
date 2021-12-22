using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("AspNetUserLogins", Schema = "dbo")]
  public partial class AspNetUserLogin
  {
    [Key]
    public string LoginProvider
    {
      get;
      set;
    }
    [Key]
    public string ProviderKey
    {
      get;
      set;
    }
    public string ProviderDisplayName
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
