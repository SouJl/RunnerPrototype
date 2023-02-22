using Runner.Interfaces;

namespace Runner.Features.AbilitySystem.Abilities
{
    internal interface IAbility
    {
        void Apply(IAbilityActivator activator);
    }
}
