using UnityEngine;

namespace Runner.Features.Storage.Upgrade
{
    internal class StubUpgradeHandler : IUpgradeHandler
    {
        public static readonly IUpgradeHandler Default = new StubUpgradeHandler();

        public void Upgrade(IUpgradable upgradable) => Debug.Log($"{GetType().Name}: can't be upgrade");
    }
}
