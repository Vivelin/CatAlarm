using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiCatAlarm.Services;

public partial class AlarmService
{
    public partial void SetAlarm(TimeSpan startTime);

    public partial bool IsSet();

    public partial bool IsEnabled();

    public partial void DeleteAlarm();

    public partial void DismissAlarm();

    public partial void EnsureAlarmIsSetIfEnabled();

    public partial TimeSpan? GetScheduledTime();
}
