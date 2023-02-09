using System.ComponentModel;

namespace Runner.Scripts.Enums
{
    internal enum InputType
    {
        [Description("KeyboardMove")]
        Keyboard,
        [Description("AccelerationMove")]
        Acceleration,
    }
}
