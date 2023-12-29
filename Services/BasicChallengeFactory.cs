namespace MauiCatAlarm.Services;

public class BasicChallengeFactory : IChallengeFactory
{
    public Challenge CreateChallenge()
    {
        return MathChallenge.CreateMultiplicationChallenge();
    }
}
