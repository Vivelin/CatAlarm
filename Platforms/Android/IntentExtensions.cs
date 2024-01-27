using Android.Content;

namespace MauiCatAlarm.Platforms.Android;

public static class IntentExtensions
{
    public static T? GetParcelableExtra<T>(this Intent intent, string? name) where T : Java.Lang.Object
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(33))
        {
            var extra = intent.GetParcelableExtra(name, Java.Lang.Class.FromType(typeof(T)));
            return (T?)extra;
        }
        else
        {
            return (T?)intent.GetParcelableExtra(name);
        }
    }
}
