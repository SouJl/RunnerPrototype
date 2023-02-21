using System.ComponentModel;

namespace Runner.Enums
{
    internal enum InputType
    {
        [Description("KeyboardMove")]
        Keyboard,
        [Description("AccelerationMove")]
        Acceleration,
    }
}
