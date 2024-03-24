using System.Collections.Generic;
using Scripts.Enums;

public class Rarity
{
    public RarityType Type { get; private set; }
    public string Color { get; private set; }
    public double DropChance { get; private set; }

    private Rarity(RarityType type, string color, double dropChance)
    {
        Type = type;
        Color = color;
        DropChance = dropChance;
    }

    public static readonly Rarity Common = new(RarityType.Common, "#ffffff", 0.75);
    public static readonly Rarity Uncommon = new(RarityType.Uncommon, "#1eff00", 0.2);
    public static readonly Rarity Rare = new(RarityType.Rare, "#0070dd", 0.04);
    public static readonly Rarity Epic = new(RarityType.Epic, "#a335ee", 0.009);
    public static readonly Rarity Legendary = new(RarityType.Legendary, "#ff8000", 0.001);
    public static readonly Rarity Artifact = new(RarityType.Artifact, "#e6cc80", 0.0001);

    public static readonly IReadOnlyDictionary<RarityType, Rarity> All = new Dictionary<RarityType, Rarity>
    {
        { RarityType.Common, Common },
        { RarityType.Uncommon, Uncommon },
        { RarityType.Rare, Rare },
        { RarityType.Epic, Epic },
        { RarityType.Legendary, Legendary },
        { RarityType.Artifact, Artifact }
    };
}
