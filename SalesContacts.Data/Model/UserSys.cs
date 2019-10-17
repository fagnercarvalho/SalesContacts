using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesContacts.Data.Model
{
    [Table("UserSys")]
    public class UserSys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [ForeignKey("UserRoleId")]
        public UserRole UserRole { get; set; }

        public int UserRoleId { get; set; }
    }
}
