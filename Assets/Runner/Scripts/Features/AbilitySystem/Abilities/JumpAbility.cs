using JoostenProductions;
using Runner.Scripts.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Features.AbilitySystem.Abilities
{
    internal class JumpAbility : IAbility
    {
        private readonly AbilityItemConfig _config;
        
        private bool _isJump;
        
        private float _jumpHeight;
        private IPhysicsUnit _physicsUnit;
        private Rigidbody2D _rigidBody;

        public JumpAbility([NotNull] AbilityItemConfig config) =>
            _config = config;

        public void Apply(IAbilityActivator activator)
        {
            if (_isJump) return;

            StartAbility(activator);          
        }

        private void StartAbility(IAbilityActivator activator)
        {
            _isJump = true;

            _jumpHeight = activator.JumpHeight;
            _physicsUnit = activator.PhysicsUnit;
            _rigidBody = _physicsUnit.UnitRigidBody;

            Jump(_rigidBody, _jumpHeight);

            UpdateManager.SubscribeToFixedUpdate(Update);
        }

        private void FinishAbility()
        {
            _isJump = false;
            _jumpHeight = default;
            _physicsUnit = default;
            _rigidBody = default;
            UpdateManager.UnsubscribeFromFixedUpdate(Update);
        }

        private void Jump(Rigidbody2D rigidbody, float jumpHeight)
        {
            rigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }


        private void Update()
        {
            if (_physicsUnit.IsGround() && _rigidBody.velocity.y < _config.Value)
                FinishAbility();
        }
    }
}
