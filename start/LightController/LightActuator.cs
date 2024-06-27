//This code is part of an imaginary home automation system.
//It is used as part of a light controller that polls a motion sensor
//and then calls the `ActuateLights` method with the results from the
//motion sensor to determine if the lights should be turned on or off.

namespace LightController;

public partial class LightActuator(ILightSwitcher lightSwitcher, ITimePeriodHelper timePeriodHelper) : ILightActuator
{

    /// <summary>
    /// public for testing, not part of interface
    /// </summary>
    public DateTime LastMotionTime { get; set; } 

    public void ActuateLights(bool motionDetected)
    {
        DateTime time = DateTime.Now;

        // Update the time of last motion.
        if (motionDetected)
        {
            LastMotionTime = time;
        }

        // If motion was detected in the evening or at night, turn the light on.
        TimePeriod timePeriod = timePeriodHelper.GetTimePeriod(time);
        if (motionDetected && (timePeriod == TimePeriod.Evening || timePeriod == TimePeriod.Night))
        {
            lightSwitcher.TurnOn();
        }
        // If no motion is detected for one minute, or if it is morning or day, turn the light off.
        else if (time.Subtract(LastMotionTime) > TimeSpan.FromMinutes(1)
            || (timePeriod == TimePeriod.Morning || timePeriod == TimePeriod.Afternoon))
        {
            lightSwitcher.TurnOff();
        }
    }
}
