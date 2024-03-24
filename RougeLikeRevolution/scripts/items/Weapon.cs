using Godot;
using Godot.Collections;
using Scipts.Attachments;
using Scripts.Enums;

public partial class Weapon : Node2D
{
	public string WeaponName;
	public Texture2D Icon;
	public int SlotCount;
	public Array<AttachmentBase> Attachments;

	public int OverheatLimit;
	public int OverheatRegenerationPerSecond;
	private int _currentOverheat;
	public float ReloadTime;
	public float FireRate;

	public WeaponPosition WeaponPosition = WeaponPosition.NotEquipped;

	public Weapon()
	{
		Attachments = new Array<AttachmentBase>();
	}

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{

	}

	public void SetAttachment(AttachmentBase attachment, int slot)
	{
		if (slot < Attachments.Count)
		{
			Attachments[slot] = attachment;
		}
	}

	public Projectile GetNextProjectile()
	{
		foreach (AttachmentBase attachment in Attachments)
		{
			if (attachment is Projectile projectile)
			{
				return projectile;
			}
		}
		return null;
	}
}
