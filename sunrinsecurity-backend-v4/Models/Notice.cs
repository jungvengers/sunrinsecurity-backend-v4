using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace sunrinsecurity_backend_v4.Models
{
    [PrimaryKey("ID")]
    public class Notice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
