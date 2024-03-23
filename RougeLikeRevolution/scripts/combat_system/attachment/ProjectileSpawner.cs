using Godot;
using System.Collections.Generic;
using System.Diagnostics;

public partial class ProjectileSpawner : Node2D
{
	private Texture2D projectileImage;
	private List<Projectile> projectiles = new List<Projectile>();
	private Rid shape;

	public override void _Ready()
	{
		projectileImage = (Texture2D)GD.Load("res://assets/sprites/projectiles/bullet.png");
		shape = PhysicsServer2D.CircleShapeCreate();
		PhysicsServer2D.ShapeSetData(shape, 80);

		// Assuming 'PlayerAttack' is the signal emitted when the player shoots
		// Connect this signal to the `OnPlayerAttack` method
		// This requires that the signal is properly emitted by the player's code
	}

	public void OnPlayerAttack(Vector2 shootPosition)
	{
		var player = GetNode<CharacterBody2D>("../..");
		Debug.Print($"Spawner Pos {player.GlobalPosition}");
		Debug.Print("Player is shooting");
		Projectile projectile = new Projectile
		{
			Position = Vector2.Zero, // Assuming shooting from the global position of this node
			Speed = 300,
			Damage = 2,
			Body = PhysicsServer2D.BodyCreate()
		};

		PhysicsServer2D.BodySetSpace(projectile.Body, GetWorld2D().Space);
		PhysicsServer2D.BodyAddShape(projectile.Body, shape);
		//PhysicsServer2D.BodySetCollisionLayer(projectile.Body, 10);
		PhysicsServer2D.BodySetCollisionMask(projectile.Body, 0);

		Vector2 direction = (shootPosition - GlobalPosition).Normalized();
		projectile.Direction = direction;

		Debug.Print($"Direction: {direction}, Position: {projectile.Position}");

		projectiles.Add(projectile);
		Debug.Print($"Projectils:  {projectiles.Count}");

		var transform2D = new Transform2D(0, projectile.Position);
		PhysicsServer2D.BodySetState(projectile.Body, PhysicsServer2D.BodyState.Transform, transform2D);
	}

	public override void _Process(double delta)
	{
		UpdateProjectiles((float)delta);
		QueueRedraw(); // Ensure the draw function is called
	}

	private void UpdateProjectiles(float delta)
	{
		foreach (var projectile in projectiles)
		{
			projectile.Position += projectile.Direction * projectile.Speed * delta;
			var transform2D = new Transform2D(0, projectile.Position);
			PhysicsServer2D.BodySetState(projectile.Body, PhysicsServer2D.BodyState.Transform, transform2D);
		}
	}

	public override void _Draw()
	{
		var offset = -projectileImage.GetSize() * 0.5f;
		foreach (var projectile in projectiles)
		{
			DrawTexture(projectileImage, projectile.Position + offset);
		}
	}

	public override void _ExitTree()
	{
		foreach (var projectile in projectiles)
		{
			PhysicsServer2D.FreeRid(projectile.Body);
		}

		PhysicsServer2D.FreeRid(shape);
		projectiles.Clear();
	}

	public class Projectile
	{
		public Vector2 Position { get; set; }
		public Vector2 Direction { get; set; }
		public float Speed { get; set; }
		public int Damage { get; set; }
		public Rid Body { get; set; }
	}
}
