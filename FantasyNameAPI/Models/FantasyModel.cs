using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FantasyNameAPI.Models
{
    public class FantasyModel
    {
        [Key]
        public string Race { get; set; }
        public List<string> Names { get; set; }
        public List<string> Descriptions { get; set; }
    }
}