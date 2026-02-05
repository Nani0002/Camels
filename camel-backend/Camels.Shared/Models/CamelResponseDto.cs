using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camels.Shared.Models
{
    public class CamelResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Color { get; set; }
        public int HumpCount { get; set; }
        public DateTime? LastFed { get; set; }
    }
}
