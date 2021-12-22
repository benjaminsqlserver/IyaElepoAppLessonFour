using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IyaElepoApp.Models.ConData
{
  [Table("Genders", Schema = "dbo")]
  public partial class Gender
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GenderID
    {
      get;
      set;
    }
    public string GenderName
    {
      get;
      set;
    }
  }
}
