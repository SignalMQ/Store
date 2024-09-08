using System.Text.Json.Serialization;

namespace Store.Models;

public class Cart
{
    [JsonIgnore]
    public int Id { get; set; }
    public int Number { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Finished { get; set; }
    public List<Good> Goods { get; set; }
    public List<Payment> Payments { get; set; }
}
