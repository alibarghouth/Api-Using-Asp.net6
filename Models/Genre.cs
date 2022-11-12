
using System.ComponentModel.DataAnnotations.Schema;

namespace Add_Database_Model.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Movie> Movies { get; set; }


    }
}
