using Godot;
using System.Diagnostics;

public partial class VulnerableEntity : CharacterBody2D
{
	[Export]
	public const int MaxHealth = 100;
	public int CurrentHealth { get; private set; } = MaxHealth;

	public int DamageTakenPerTick { get; private set; }
	private string itemDrop;

	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		var sprite = GetNode<AnimatedSprite2D>("EntitySprite");
		sprite.Modulate = sprite.Modulate.Lerp(new Color(1, 1, 1), 0.1f);
	}

	public void OnEntityReceivedDamage(Node2D body)
	{
		TakeDamage(1);
	}

	public void OnBodyShapeEntered(Rid bodyRid, Node2D body, int bodyShapeIndex, int localBodyShapeIndex)
	{
		GetNode<AnimatedSprite2D>("EntitySprite").Modulate = new Color(1, 0, 0);
		TakeDamage(10);
	}

	private void TakeDamage(int damage)
	{
		CurrentHealth -= damage;
		Debug.Print($"Health: {CurrentHealth}");
		if (CurrentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		// Drop the item
		if (itemDrop != null)
		{
			/*
			var item = itemDrop.Instance();
			GetParent().AddChild(item);
			item.GlobalPosition = GlobalPosition;
			*/
		}
		QueueFree();
	}

}
