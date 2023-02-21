using Runner.Interfaces;

namespace Features.AbilitySystem.Abilities
{
    internal interface IAbility
    {
        void Apply(IAbilityActivator activator);
    }
}
