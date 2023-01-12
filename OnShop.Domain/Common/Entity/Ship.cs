using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnShop.Domain.Common.Entity
{
    public class Ship
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        [Required]
        public string Src { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Content { get; set; }
    }
}