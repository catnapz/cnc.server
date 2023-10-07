using System.Security.Cryptography;
using Cnc.Server.Rest.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cnc.Server.Rest;

[ApiController]
[Route("roll")]
public class RollController : ControllerBase
{
    /// <summary>
    /// POST method that will return dice roll results
    /// </summary>
    /// <param name="diceRoll"><see cref="DiceRollRequestDto"/></param>
    /// <returns>Dice roll results</returns>
    [HttpPost]
    public ActionResult<IEnumerable<int>> GetRoll(DiceRollRequestDto? diceRoll)
    {
        diceRoll ??= new DiceRollRequestDto();
        var result = new List<int>(diceRoll.NumDice);
        var maxRoll = diceRoll.Type.ToLower() switch
        {
            "d4" => 4,
            "d6" => 6,
            "d8" => 8,
            "d10" => 10,
            "d12" => 12,
            "d20" => 20,
            _ => 20
        };
        for (var i = 0; i < diceRoll.NumDice; i++)
        {
            result.Add(RandomNumberGenerator.GetInt32(1, maxRoll));
        }
        return result;
    }
}