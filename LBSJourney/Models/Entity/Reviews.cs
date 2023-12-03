using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBSJourney.Models.Entity
{
    [Table(name:"Reviews")]
    public class Reviews
    {
        [Key]
        public String? ReviewId { get; set; }
        public Int32 Score { get; set; }
        public String? Comment { get; set; }
        public String? CreateTime { get; set; }
        public String? UserName { get; set; }
        public String? PlaceId { get; set; }
    }
}
