namespace LightController;

public class LightController
{
    private readonly IMotionSensor MotionSensor;
    private readonly ILightActuator LightActuator;
    private readonly Timer timer;

    public LightController(IMotionSensor motionSensor, ILightActuator lightActuator)
    {
        MotionSensor = motionSensor;
        LightActuator = lightActuator;

        timer = new Timer
        {
            Enabled = true,
            Interval = 1000 // ms
        };
        timer.Elapsed += Poll;
    }

    public void Poll(object? source, EventArgs? e)
    {
        LightActuator.ActuateLights(MotionSensor.DetectMotion());
    }
}
