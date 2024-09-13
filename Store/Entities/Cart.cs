using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Store.Models;

public class Cart
{
    [JsonIgnore]
    public int Id { get; set; }
    public int Number { get; set; }
    public DateTime Created { get; set; }
    public DateTime Finished { get; set; }
    [AllowNull]
    public List<Good> Goods { get; set; }
    [AllowNull]
    public List<Payment> Payments { get; set; }
}
