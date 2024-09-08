using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Store.Enums;

namespace Store.Models;

public class Payment
{
    [JsonIgnore]
    public int CartId { get; set; }
    public string? PaymentType { get; set; }
    public string? CardType { get; set; }
    public decimal Amount { get; set; }
    public decimal Surcharge { get; set; }
}
