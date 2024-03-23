using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class ProjectilesManager : Node2D
{
	private Texture2D _projectileTexture;
	private List<Projectile> _projectiles = new();
	public Rid _shape;

	public class Projectile
	{
		public Vector2 GlobalPosition = new();
		public Vector2 Direction = new();
		public float Speed = 400.0f;
		public int Damage = 10;
		public int Lifetime = 600;
		public Rid Body = new();
	}

	public override void _Ready()
	{
		_projectileTexture = ResourceLoader.Load<Texture2D>("res://assets/sprites/projectiles/bullet.png");
		_shape = PhysicsServer2D.CircleShapeCreate();
		PhysicsServer2D.ShapeSetData(_shape, 8.0f);
	}

	public void SpawnProjectile(Vector2 spawnPosition, Vector2 direction)
	{
        Projectile projectile = new()
        {
			GlobalPosition = spawnPosition,
			Direction = direction,
            Body = PhysicsServer2D.BodyCreate()
        };

        PhysicsServer2D.BodySetSpace(projectile.Body, GetWorld2D().Space);
		PhysicsServer2D.BodyAddShape(projectile.Body, _shape);
		PhysicsServer2D.BodySetCollisionLayer(projectile.Body, 0b00000000_00000000_00000100_00000000);
		PhysicsServer2D.BodySetCollisionMask(projectile.Body,  0b00000000_00000000_00000010_00000000);

        Transform2D transform2D = new()
        {
            Origin = projectile.GlobalPosition
        };
        PhysicsServer2D.BodySetState(projectile.Body, PhysicsServer2D.BodyState.Transform, transform2D);

		_projectiles.Add(projectile);
	}

	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	public override void _PhysicsProcess(double delta)
	{
		Transform2D transform2D = new();
		for(int i = _projectiles.Count - 1; i >= 0; i--)
		{
			var projectile = _projectiles[i];
			projectile.Lifetime--;
			if (projectile.Lifetime <= 0)
			{
				PhysicsServer2D.BodyRemoveShape(projectile.Body, 0);
				PhysicsServer2D.FreeRid(projectile.Body);
				_projectiles.RemoveAt(i);
				continue;
			}

			projectile.GlobalPosition += projectile.Direction.Normalized() * projectile.Speed * (float)delta;
			transform2D.Origin = projectile.GlobalPosition;
			PhysicsServer2D.BodySetState(projectile.Body, PhysicsServer2D.BodyState.Transform, transform2D);
		}
	}

	public override void _Draw()
	{
		Vector2 offset = -_projectileTexture.GetSize() * 0.5f;
		foreach (var projectile in _projectiles)
		{
			DrawTexture(_projectileTexture, projectile.GlobalPosition + offset);
			ZIndex = 10;
		}
	}

	public override void _ExitTree()
	{
		foreach (var projectile in _projectiles)
		{
			PhysicsServer2D.BodyRemoveShape(projectile.Body, 0);
			PhysicsServer2D.FreeRid(projectile.Body);
		}

		PhysicsServer2D.FreeRid(_shape);
		_projectiles.Clear();
	}
}
