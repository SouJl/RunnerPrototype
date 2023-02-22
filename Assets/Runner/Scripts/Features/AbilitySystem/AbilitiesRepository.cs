using Runner.Features.AbilitySystem.Abilities;
using Runner;
using System.Collections.Generic;

namespace Runner.Features.AbilitySystem
{
    internal interface IAbilitiesRepository : IRepository
    {
        IReadOnlyDictionary<string, IAbility> Items { get; }
    }
    internal class AbilitiesRepository : BaseRepository<string, IAbility, AbilityItemConfig>, IAbilitiesRepository
    {
        public AbilitiesRepository(IEnumerable<AbilityItemConfig> configs) : base(configs)
        { }

        protected override string GetKey(AbilityItemConfig config) => config.Id;

        protected override IAbility CreateItem(AbilityItemConfig config) =>
            config.Type switch
            {
                AbilityType.Projectile => new ProjectileAbility(config),
                AbilityType.Jump => new JumpAbility(config),
                _ => StubAbility.Default
            };
    }
}
