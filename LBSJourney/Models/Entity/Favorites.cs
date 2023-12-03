using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBSJourney.Models.Entity
{
    [Table(name:"Favorites")]
    public class Favorites
    {
        [Key]
        public String? FavoriteId { get; set; }
        public String? UserName { get; set; }
        public String? PlaceId { get; set; }
    }
}
