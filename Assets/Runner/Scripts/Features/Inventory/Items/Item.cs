using UnityEngine;

namespace Features.Inventory.Items
{
    internal interface IItem
    {
        string Id { get; }
        ItemInfo Info { get; }
    }

    internal readonly struct ItemInfo
    {
        public string Title { get; }
        public Sprite Icon { get; }

        public string Info { get; }

        public ItemInfo(string title, Sprite icon, string info)
        {
            Title = title;
            Icon = icon;
            Info = info;
        }
    }

    internal class Item : IItem
    {
        public string Id { get; }
        public ItemInfo Info { get; }

        public Item(string id, ItemInfo info)
        {
            Id = id;
            Info = info;
        }
    }


}
