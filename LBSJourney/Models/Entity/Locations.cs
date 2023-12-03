using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBSJourney.Models.Entity
{
    [Table(name:"Locations")]
    public class Locations
    {
        [Key]
        public Guid? LocationId { get; set; }
        public String? Latitude { get; set; }
        public String? Longitude { get; set; }
        public DateTime? CreateTime { get; set; }
        public String? UserName { get; set; }
    }
}
