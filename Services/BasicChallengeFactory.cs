namespace MauiCatAlarm.Services;

public class BasicChallengeFactory : IChallengeFactory
{
    public Challenge CreateChallenge()
    {
        var luck = Random.Shared.NextDouble();
        return luck switch
        {
            < 0.01 => MathChallenge.CreateDivisionChallenge(Difficulty.Insane),
            < 0.05 => MathChallenge.CreateMultiplicationChallenge(Difficulty.Normal),
            _ => MathChallenge.CreateMultiplicationChallenge(Difficulty.Easy)
        };
    }
}
