using Runner.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.AbilitySystem.Abilities
{
    internal class ProjectileAbility : IAbility
    {
        private readonly AbilityItemConfig _config;

        public ProjectileAbility([NotNull] AbilityItemConfig config) =>
          _config = config ?? throw new ArgumentNullException(nameof(config));

        public void Apply(IAbilityActivator activator)
        {
            var projectile = Object.Instantiate(_config.ExecuteObject, 
                activator.ViewGameObject.transform.position, 
                Quaternion.identity).GetComponent<Rigidbody2D>();
            Vector3 force = activator.ViewGameObject.transform.right * _config.Value;
            projectile.AddForce(force, ForceMode2D.Force);
        }
    }
}
