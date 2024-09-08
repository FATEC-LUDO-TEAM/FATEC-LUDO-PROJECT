using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("user_cosmetics")]
public class UserCosmetics {
    [Key]
    public string user_id { get; set; }
    public string[] available_cosmetics { get; set; }
    public string[] wishlist_cosmetics { get; set;}
}