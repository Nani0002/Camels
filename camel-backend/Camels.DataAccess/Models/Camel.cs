using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camels.DataAccess.Models
{
    public class Camel
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Color { get; set; }

        [Range(1, 2, ErrorMessage = "Hump count must be one or two.")]
        public int HumpCount { get; set; }

        public DateTime? LastFed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
