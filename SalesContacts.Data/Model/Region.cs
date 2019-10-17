using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesContacts.Data.Model
{
    [Table("Region")]
    public class Region
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
