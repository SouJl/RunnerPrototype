﻿using System.Collections.Generic;

namespace Runner.Features.Storage.Upgrade
{
    internal interface IUpgradeHandlersRepository : IRepository
    {
        IReadOnlyDictionary<string, IUpgradeHandler> Items { get; }
    }

    internal class UpgradeHandlersRepository
        : BaseRepository<string, IUpgradeHandler, UpgradeItemConfig>, IUpgradeHandlersRepository
    {
        public UpgradeHandlersRepository(IEnumerable<UpgradeItemConfig> configs) : base(configs)
        { }

        protected override string GetKey(UpgradeItemConfig config) => 
            config.Id;

        protected override IUpgradeHandler CreateItem(UpgradeItemConfig config) =>
            config.Type switch
            {
                UpgradeType.Speed => new SpeedUpgradeHandler(config.Value),
                UpgradeType.JumpHeight => new JumpHeightUpgradeHandler(config.Value),
                UpgradeType.Health => new HealthUpgradeHandler(config.Value),
                _ => StubUpgradeHandler.Default
            };
    }
}
