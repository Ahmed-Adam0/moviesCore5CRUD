using System.ComponentModel.DataAnnotations;

namespace moviesCore5CRUD.Models
{
    public class movie
    {
        
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string Title { get; set; }
        public int year { get; set; }
        public double Rate { get; set; }
        public string StoreLine { get; set; }
        public byte[] poster { get; set; }
        public byte GeneraId { get; set; }
        public Genera Genera { get; set; } 

    }
}
