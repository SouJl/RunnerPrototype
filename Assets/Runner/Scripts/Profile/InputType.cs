using System.ComponentModel;

namespace Runner.Profile
{
    internal enum InputType
    {
        [Description("KeyboardMove")]
        Keyboard,
        [Description("AccelerationMove")]
        Acceleration,
    }
}
