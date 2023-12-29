namespace MauiCatAlarm;

public abstract class Challenge
{
    public abstract string Prompt { get; }

    public abstract bool Validate(string response);
}
