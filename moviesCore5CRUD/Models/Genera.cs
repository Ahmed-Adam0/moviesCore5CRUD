using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moviesCore5CRUD.Models
{
    public class Genera
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte id { get; set; }
        [Required,MaxLength(100)]         
        public string name { get; set; }
    }
}
