using Features.AbilitySystem.Abilities;
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

        public static UpgradeItemConfig[] LoadUpgradeItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourceLoader.LoadObject<UpgradeItemsDataConfig>(resourcePath);
            return dataSource == null ? Array.Empty<UpgradeItemConfig>() : dataSource.ItemConfigs.ToArray();
        }

        public static AbilityItemConfig[] LoadAbilityItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourceLoader.LoadObject<AbilityItemsDataConfig>(resourcePath);
            return dataSource == null ? Array.Empty<AbilityItemConfig>() : dataSource.AbilityConfigs.ToArray();
        }
    }
}
