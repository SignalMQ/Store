using System.Text.Json.Serialization;

namespace Store.Models;

public class Good
{
    [JsonIgnore]
    public int CartId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Count { get; set; }
    public int VAT { get; set; }
    public decimal VATSum { get; set; }
    public string? MXIK { get; set; }
    public int UnitCode { get; set; }
    public string? UnitName { get; set; }
}
