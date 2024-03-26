using Godot;
using Godot.Collections;
using Scripts.Naming;

namespace Scripts.Characters.Player;

public partial class PlayerVariables : Node2D
{
    public int CurrentHealth = 100;
    public int MaxHealth = 100;
    public int Shield = 0;
    public int Armor = 0;

    public int Speed = 300;
    public int CurrentWeaponOverheatLimit = 0;
    public int CurrentWeaponOverheatRegenerationPerSecond = 0;

    public int Credits = 0;
    public int CryptoTokens = 0;
    public Dictionary<string, int> CorporateStocks = new()
    {
        { Corporates.CorporateNames[0], 0 },
        { Corporates.CorporateNames[1], 0 },
        { Corporates.CorporateNames[2], 0 },
        { Corporates.CorporateNames[3], 0 }
    };

    public override void _Ready()
    {
        ConsoleMono.CreateCommand(nameof(ChangeMaxHealth), this, MethodName.ChangeMaxHealth);
        ConsoleMono.CreateCommand(nameof(ChangeCurrentHealth), this, MethodName.ChangeCurrentHealth);
        ConsoleMono.CreateCommand(nameof(ChangeShield), this, MethodName.ChangeShield);
    }

    public void ChangeMaxHealth(int amount)
    {
        ConsoleMono.Print($"{GetType().Name}: Changing max health by {amount}");
        MaxHealth += amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void ChangeCurrentHealth(int amount)
    {
        ConsoleMono.Print($"{GetType().Name}: Changing current health by {amount}");
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void ChangeShield(int amount)
    {
        ConsoleMono.Print($"{GetType().Name}: Changing shield by {amount}");
        Shield += amount;
    }


}