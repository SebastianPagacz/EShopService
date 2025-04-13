namespace EShopService.Models;

public class Category : BaseModel
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = default!;
}
