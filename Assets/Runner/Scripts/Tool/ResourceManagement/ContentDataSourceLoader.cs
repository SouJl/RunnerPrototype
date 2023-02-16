using Features.Inventory.Items;
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
    }
}
