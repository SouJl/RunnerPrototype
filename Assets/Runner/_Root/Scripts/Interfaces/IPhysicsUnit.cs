using UnityEngine;

namespace Runner.Interfaces
{
    internal interface IPhysicsUnit
    {
        Rigidbody2D UnitRigidBody { get; }
        bool IsGround();
    }
}
