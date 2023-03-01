using UnityEngine;

namespace Runner.Interfaces
{
    internal interface IAbilityActivator
    {
        float JumpHeight { get; }
        IPhysicsUnit PhysicsUnit { get; }

        GameObject ViewGameObject { get; }
    }
}
