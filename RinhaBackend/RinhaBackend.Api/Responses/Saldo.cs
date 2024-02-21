using System.Text.Json.Serialization;

namespace RinhaBackend.Api.Responses;

public class Saldo
{
    [JsonPropertyName("total")]
    public double Total { get; set; }
    [JsonPropertyName("data_extrato")]
    public DateTime Data { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("limite")]
    public double Limite { get; set; }
}