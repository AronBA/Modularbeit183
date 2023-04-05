using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Modularbeit183.Models;





[Table("tbl_User")]
public class UserModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public char? Username { get; set; }
    [Required]
    public char? Sname { get; set; }
    [Required]
    public char? Lname { get; set; }
    public char? Passwort { get; set; }
    public bool? Verified { get; set; }
    public bool? Deleted { get; set; }
    public char? Email { get; set; }
}