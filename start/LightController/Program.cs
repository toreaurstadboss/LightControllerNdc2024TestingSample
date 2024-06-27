using Microsoft.Extensions.DependencyInjection;

namespace LightController;

public class Program
{
    public static void Main()
    {

        var services = new ServiceCollection();

        services.AddSingleton<ILightSwitcher, LightSwitcherInstance>();
        services.AddSingleton<ITimePeriodHelper, ITimePeriodHelper>();
        services.AddSingleton<IMotionSensor, MotionSensorInstance>();
        services.AddSingleton<ILightActuator, LightActuator>();
        services.AddSingleton<LightController, LightController>();

        var app = services.BuildServiceProvider();

        var lightController = app.GetRequiredService<LightController>(); 



        while(true)
        {
            Thread.Sleep(100);
        }
    }
}
