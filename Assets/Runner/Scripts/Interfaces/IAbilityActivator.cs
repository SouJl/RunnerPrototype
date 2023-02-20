using UnityEngine;

namespace Runner.Scripts.Interfaces
{
    internal interface IAbilityActivator
    {
        float JumpHeight { get; }
        IPhysicsUnit PhysicsUnit { get; }

        GameObject ViewGameObject { get; }
    }
}
