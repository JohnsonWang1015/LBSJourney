using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBSJourney.Models.Entity
{
    [Table(name:"Places")]
    public class Places
    {
        [Key]
        public Guid? PlaceId { get; set; }
        public String? Name { get; set; }
        public String? Address { get; set; }
        public String? Description { get; set; }
        public String? PictureUrl { get; set; }
        public Int32 Score { get; set; }
        public String? Latitude { get; set; }
        public String? Longitude { get; set; }
    }
}
