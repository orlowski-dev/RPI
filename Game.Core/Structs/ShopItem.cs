namespace Game.Core.Structs
{
    public struct ShopItem
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string TargetClass { get; set; }
        public string ItemType { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int CritChance { get; set; }
        public int Luck { get; set; }
        public int HP { get; set; }
    }
}
