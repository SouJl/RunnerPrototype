﻿namespace Runner.Features.Storage.Upgrade
{
    internal class JumpHeightUpgradeHandler : IUpgradeHandler
    {
        private readonly float _jumpHeight;

        public JumpHeightUpgradeHandler(float jumpHeight) =>
            _jumpHeight = jumpHeight;

        public void Upgrade(IUpgradable upgradable) =>
            upgradable.JumpHeight += _jumpHeight;
    }
}
