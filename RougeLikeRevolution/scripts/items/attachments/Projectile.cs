using System.Collections.Generic;
using Godot;
using Scripts.Enums;

namespace Scripts.Attachments;

public partial class Projectile : AttachmentBase
{
    protected AnimatedSprite2D _sprite;

    protected int _damage;
    protected DamageType _damageType = DamageType.Energy;
    protected int _numberOfProjectiles;
    protected int _critRate;
    protected int _penetration;
    protected int _armorPiercing;
    protected float _effectRadius;

    private const int DefaultDamage = 1;
    private const int DefaultNumberOfProjectiles = 1;
    private const int DefaultCritRate = 0;
    private const int DefaultPenetration = 1;
    private const int DefaultArmorPiercing = 0;
    private const float DefaultEffectRadius = 0.0f;

    protected int _speed;
    protected int _lifetime;
    protected float _accuracy;
    protected int _overheatCost;

    private const int DefaultSpeed = 400;
    private const int DefaultLifetime = 60;
    private const float DefaultAccuracy = 1.0f;
    private const int DefaultOverheatCost = 0;

    private List<string> _customDescription;

    public Projectile()
    {
        _description = new List<string> {};
    }

    public override void _Ready()
    {
        _type = AttachmentType.Projectile;
        UpdateDescription();
    }

    public void getAllEnhancementsOnLeft()
    {
        return;
    }

    public void UpdateDescription()
    {
        // Damage is always included
        _description.Add($"Damage: {_damage}");

        // Only add to description if the value differs from the default
        if (_numberOfProjectiles != DefaultNumberOfProjectiles) _description.Add($"Number of Projectiles: {_numberOfProjectiles}");
        if (_critRate != DefaultCritRate) _description.Add($"Crit Rate: {_critRate}%");
        if (_armorPiercing != DefaultArmorPiercing) _description.Add($"Armor Piercing: {_armorPiercing}");
        if (_accuracy != DefaultAccuracy) _description.Add($"Accuracy: {_accuracy}");
        if (_effectRadius != DefaultEffectRadius) _description.Add($"Effect Radius: {_effectRadius}");

        for (int i = 0; i < _customDescription.Count; i++)
        {
            _description.Add(_customDescription[i]);
        }
    }
}