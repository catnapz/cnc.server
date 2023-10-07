using System.Text.Json.Serialization;

namespace Cnc.Server.Rest.Model;

public class DiceRollRequestDto
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "d20";
    
    [JsonPropertyName("num_dice")]
    public int NumDice { get; set; } = 1;
}