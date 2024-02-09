//DB entity for game items. Structure should look like:
//Game name ,Item category ,Item name, Item Image, Item price

using System.ComponentModel.DataAnnotations;

namespace Catalog.Entities
{
    public class GameItem{
    [Key]
    public int ItemId { get; set; }

    [Required]
    [StringLength(100)]
    public string GameName { get; set; }

    [Required]
    [StringLength(100)]
    public string ItemName { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [StringLength(50)]
    public string Category { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int OwnerUserId { get; set; }

    [StringLength(2083)] // Standard max length for URLs
    public string ImageURL { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }

    public DateTime? LastUpdatedDate { get; set; }

    [Required]
    public int QuantityAvailable { get; set; }

    [Required]
    [StringLength(50)]
    public string GamePlatform { get; set; }
    }
}
