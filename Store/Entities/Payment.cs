using System.Text.Json.Serialization;

namespace Store.Models;

public class Payment
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonIgnore]
    public int CartId { get; set; }
    public string? PaymentType { get; set; }
    public string? CardType { get; set; }
    public decimal Amount { get; set; }
    public decimal Surcharge { get; set; }
}