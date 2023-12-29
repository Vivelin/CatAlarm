namespace MauiCatAlarm;

public class MathChallenge(string prompt, Func<double, bool> validator) : Challenge
{
    private readonly Func<double, bool> _validator = validator;

    public override string Prompt { get; } = prompt;

    public static MathChallenge CreateAdditionChallenge()
    {
        var a = new Random().Next(1, 10);
        var b = new Random().Next(1, 10);
        return new MathChallenge(
            $"{a} + {b} = ?",
            answer => answer == a + b);
    }

    public static MathChallenge CreateMultiplicationChallenge()
    {
        var a = new Random().Next(2, 10);
        var b = new Random().Next(2, 10);
        return new MathChallenge(
            $"{a} × {b} = ?",
            answer => answer == a * b);
    }

    public override bool Validate(string response)
    {
        if (!double.TryParse(response, out var answer))
            return false;

        return _validator(answer);
    }
}
