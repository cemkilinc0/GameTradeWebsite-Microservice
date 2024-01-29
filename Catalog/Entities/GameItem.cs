//DB entity for game items. Structure should look like:
//Game name ,Item category ,Item name, Item Image, Item price

using System.ComponentModel.DataAnnotations;

namespace Catalog.Entities
{
    public class GameItem{
        [Key]
        public int ItemId { get; set; }
        [Required]
        public string? GameName { get; set;}
        public string? Category{ get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        [Required]
        public string? UserId { get; set;}
    }
}
