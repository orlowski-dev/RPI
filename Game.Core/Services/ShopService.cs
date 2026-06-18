using System.Collections.Generic;
using Game.Core.Structs;

namespace Game.Core.Services
{
    public class ShopService
    {
        public int PlayerGold { get; private set; } = 100;
        public List<ShopItem> ShopItems { get; private set; }

        public ShopService()
        {
            ShopItems = new List<ShopItem>
            {
                new ShopItem { Name = "Żelazny miecz", Price = 30, TargetClass = "Wojownik", ItemType = "Broń", Attack = 12, Defense = 2, CritChance = 3, Luck = 0, HP = 0 },
                new ShopItem { Name = "Topór Demacjanina", Price = 50, TargetClass = "Wojownik", ItemType = "Broń", Attack = 20, Defense = -3, CritChance = 8, Luck = 1, HP = 0 },
                new ShopItem { Name = "Miecz strażnika", Price = 45, TargetClass = "Wojownik", ItemType = "Broń", Attack = 14, Defense = 5, CritChance = 2, Luck = 0, HP = 0 },

                new ShopItem { Name = "Łuk myśliwego", Price = 35, TargetClass = "Łucznik", ItemType = "Broń", Attack = 14, Defense = 1, CritChance = 10, Luck = 3, HP = 0 },
                new ShopItem { Name = "Cichy długi łuk", Price = 60, TargetClass = "Łucznik", ItemType = "Broń", Attack = 17, Defense = 0, CritChance = 14, Luck = 4, HP = 0 },
                new ShopItem { Name = "Łuk wiatru", Price = 45, TargetClass = "Łucznik", ItemType = "Broń", Attack = 13, Defense = 0, CritChance = 10, Luck = 2, HP = 0 },

                new ShopItem { Name = "Kostur adepta", Price = 40, TargetClass = "Mag", ItemType = "Broń", Attack = 16, Defense = 1, CritChance = 8, Luck = 2, HP = 0 },
                new ShopItem { Name = "Różdżka", Price = 55, TargetClass = "Mag", ItemType = "Broń", Attack = 21, Defense = 0, CritChance = 12, Luck = 3, HP = 0 },
                new ShopItem { Name = "Kostur chorążego", Price = 70, TargetClass = "Mag", ItemType = "Broń", Attack = 25, Defense = 2, CritChance = 7, Luck = 3, HP = 0 },

                new ShopItem { Name = "Skórzana zbroja", Price = 35, TargetClass = "Łucznik", ItemType = "Zbroja", Attack = 0, Defense = 8, CritChance = 2, Luck = 1, HP = 10 },
                new ShopItem { Name = "Pancerz płytowy", Price = 65, TargetClass = "Wojownik", ItemType = "Zbroja", Attack = 0, Defense = 16, CritChance = 0, Luck = 0, HP = 15 },
                new ShopItem { Name = "Szata boża", Price = 80, TargetClass = "Mag", ItemType = "Zbroja", Attack = 6, Defense = 7, CritChance = 4, Luck = 3, HP = 20 }
            };
        }

        public ShopItem? TryBuyItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= ShopItems.Count)
                return null;

            ShopItem item = ShopItems[itemIndex];

            if (PlayerGold >= item.Price)
            {
                PlayerGold -= item.Price;
                return item;
            }        
            return null;
        }

        public void AddGold(int amount)
        {
            if (amount > 0)
            {
                PlayerGold += amount;
            }
        }
    }
}