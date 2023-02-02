using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace sunrinsecurity_backend_v4.Models
{
    [PrimaryKey("ID")]
    public class Club
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Curriculum { get; set; }
        public string Image { get; set; }
        public Link[]? Link { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
