//This code is part of an imaginary home automation system.
//It is used as part of a light controller that polls a motion sensor
//and then calls the `ActuateLights` method with the results from the
//motion sensor to determine if the lights should be turned on or off.

namespace LightController;


public class TimePeriodHelper : ITimePeriodHelper
{



    public TimePeriod GetTimePeriod(DateTime dateTime)
    {
        if (dateTime.Hour >= 0 && dateTime.Hour < 6)
        {
            return TimePeriod.Night;
        }
        if (dateTime.Hour >= 6 && dateTime.Hour < 12)
        {
            return TimePeriod.Morning;
        }
        if (dateTime.Hour >= 12 && dateTime.Hour < 18)
        {
            return TimePeriod.Afternoon;
        }
        return TimePeriod.Evening;
    }
}


public enum TimePeriod
{
    Night,
    Morning,
    Afternoon,
    Evening
}

