using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiCatAlarm.Services;

public partial class AlarmService
{
    public partial void SetAlarm(TimeSpan startTime, TimeSpan endTime, Func<bool> callback);

    public partial bool IsSet();

    public partial void DeleteAlarm();
}
