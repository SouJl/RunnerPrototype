namespace Runner.Features.Storage.Upgrade
{
    internal class HealthUpgradeHandler : IUpgradeHandler
    {
        private readonly float _health;

        public HealthUpgradeHandler(float health) =>
            _health = health;

        public void Upgrade(IUpgradable upgradable) =>
            upgradable.Health += _health;
    }
}
