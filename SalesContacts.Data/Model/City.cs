using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesContacts.Data.Model
{
    [Table("City")]
    public class City
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }
    }
}
