using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace sunrinsecurity_backend_v4.Models
{
    [PrimaryKey("ID")]
    public class Link
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; }
        public string? image { get; set; }
        public string? name { get; set; }
        public string link { get; set; }
    }
}
