using Godot;
using Scripts.Enums;

namespace Scripts.Attachments;

public partial class Ultimate : AttachmentBase
{
    protected AnimatedSprite2D _sprite;

    protected int _Damage;
    protected DamageType _damageType = DamageType.Energy;
    protected int _speed;
    protected int _lifetime;
    protected int accuracy;
    protected int _overheatCost;

    public override void _Ready()
    {
        _type = AttachmentType.Ultimate;
    }
}