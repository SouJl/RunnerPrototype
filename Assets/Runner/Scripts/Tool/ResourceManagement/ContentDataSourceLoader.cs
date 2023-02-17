using Features.Inventory.Items;
using Features.Storage.Upgrade;
using System;
using System.Linq;

namespace Runner.Scripts.Tool
{
    internal static class ContentDataSourceLoader
    {
        public static ItemConfig[] LoadItemConfig(ResourcePath resourcePath)
        {
            var source = ResourceLoader.LoadObject<ItemsDataConfig>(resourcePath);
            return source == null ? Array.Empty<ItemConfig>() : source.ItemConfigs.ToArray();
        }

        public static UpgradeItemConfig[] LoadUpgradeItemConfig(ResourcePath resourcePath)
        {
            var dataSource = ResourceLoader.LoadObject<UpgradeItemsDataConfig>(resourcePath);
            return dataSource == null ? Array.Empty<UpgradeItemConfig>() : dataSource.ItemConfigs.ToArray();
        }

    }
}
