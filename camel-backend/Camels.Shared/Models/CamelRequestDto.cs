using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camels.Shared.Models
{
    public class CamelRequestDto
    {
        [Required(ErrorMessage = "The camel's name is required.")]
        [MinLength(1, ErrorMessage = "Name cannot be empty.")]
        public required string Name { get; set; }

        public string? Color { get; set; }

        [Range(1, 2, ErrorMessage = "Hump count must be one or two.")]
        public int HumpCount { get; set; }

        public DateTime? LastFed { get; set; }
    }
}
