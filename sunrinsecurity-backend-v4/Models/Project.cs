using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace sunrinsecurity_backend_v4.Models
{
    [PrimaryKey("ID")]
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string Type { get; set; }
        public string Participant { get; set; }
    }
}
