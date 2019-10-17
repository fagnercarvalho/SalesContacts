using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesContacts.Data.Model
{
    [Table("Gender")]
    public class Gender
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
