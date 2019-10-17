using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesContacts.Data.Model
{
    [Table("Classification")]
    public class Classification
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
