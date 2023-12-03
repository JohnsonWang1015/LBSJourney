using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBSJourney.Models.Entity
{
    [Table(name:"Users")]
    public class Users
    {
        [KeyAttribute]
        [Column(name:"UserName")]
        [MaxLength(128)]
        public String? UserName { get; set;}
        [Column(name:"Email")]
        [MaxLength(50)]
        public String? Email { get; set; }
        [Column(name:"Password")]
        [MaxLength(128)]
        public String? Password { get; set; }
        [Column(name:"CreateTime")]
        public DateTime? CreateTime { get; set; }
    }
}
