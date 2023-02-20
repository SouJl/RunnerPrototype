using UnityEngine;

namespace Runner.Scripts.Interfaces
{
    internal interface IPhysicsUnit
    {
        Rigidbody2D UnitRigidBody { get; }
        bool IsGround();
    }
}
