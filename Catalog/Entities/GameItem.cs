//DB entity for game items. Structure should look like:
//Game name ,Item category ,Item name, Item Image, Item price

public class GameItem{
    public int Id { get; set; }
    public string GameName { get; set;}
    public string Category{ get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }

}