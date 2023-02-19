using Runner.Scripts.Interfaces;

namespace Features.AbilitySystem.Abilities
{
    internal interface IAbility
    {
        void Apply(IAbilityActivator activator);
    }
}
