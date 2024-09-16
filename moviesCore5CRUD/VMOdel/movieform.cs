using moviesCore5CRUD.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace moviesCore5CRUD.VMOdel
{
    public class movieform
    {
        public int Id { get; set; }
        [Required,StringLength(50)]
        public string Title { get; set; }
        public int year { get; set; }
        [Range (1, 10)]
        public double Rate { get; set; }
        public string StoreLine { get; set; }
        [Display(Name = "add poster...")]
        public byte[] poster { get; set; }
        [Display(Name="Genre")]
        public byte GeneraId { get; set; }
        public IEnumerable<Genera> Generas { get; set; }
    }
}
