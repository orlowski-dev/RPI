using System.Collections.Generic;

using Game.Core.Services;

namespace Game.Core.Services
{
    public struct ShopItem
    {
    public string Name { get; set; }
    public int Price { get; set; }
    public string TargetClass { get; set; }
}
public class ShopService
{
    public int PlayerGold {get; private set; } = 100;
    public List<ShopItem> ShopItems {get; private set;}
    public ShopService()
    {
        ShopItems = new List<ShopItem>
        {
            new ShopItem { Name = "Żelazny Miecz", Price = 30, TargetClass = "Wojownik"},
            new ShopItem { Name = "Stalowa Tarcza", Price = 45, TargetClass = "Wojownik"},
            new ShopItem { Name = "Kostur Iskrzenia", Price = 35, TargetClass = "Mag"},
            new ShopItem { Name = "Szata Mana", Price = 50, TargetClass = "Mag"},
            new ShopItem { Name = "Długi Łuk", Price = 35, TargetClass = "Łucznik"},
            new ShopItem { Name = "Lekki Pancerz", Price = 55, TargetClass = "Łucznik"},
        };
    }

    public ShopItem? TryBuyItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= ShopItems.Count)
            return null;

            ShopItem item = ShopItems[itemIndex]
;

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
