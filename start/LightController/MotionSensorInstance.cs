namespace LightController;

public class MotionSensorInstance : IMotionSensor
{
    private bool motion;
    public bool DetectMotion()
    {
        motion = !motion;
        return motion;
    }
}
