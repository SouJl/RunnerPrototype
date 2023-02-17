
namespace Features.Storage.Upgrade
{
    internal interface IUpgradeHandler
    {
        void Upgrade(IUpgradable upgradable);
    }
}
