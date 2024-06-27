namespace LightController;

public class LightSwitcherInstance : ILightSwitcher
{
    public void TurnOff()
    {
        Console.WriteLine("Setting lights to off");
    }

    public void TurnOn()
    {
        Console.WriteLine("Setting lights to on");
    }
}
