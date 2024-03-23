using Godot;
using System;
using System.Diagnostics;

public partial class PlayerAnimation : Node2D
{
	private Node2D _skeleton;
	private Node2D _hands;
	private Node2D _feet;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_skeleton = this;
		_hands = GetNode<Node2D>("Hands");
		_feet = GetNode<Node2D>("Feet");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		AnimatePlayer();
		if (Input.IsActionPressed("move_up") || Input.IsActionPressed("move_down") || Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right"))
		{
			AnimateMovement((float) delta);
		}
	}

	private void AnimatePlayer()
	{
		// Modify hand position to look at the mouse
		var angleToMouse = (GetGlobalMousePosition() - _hands.GlobalPosition).Angle();
		_hands.LookAt(GetGlobalMousePosition());

		var handLeft = _hands.GetNode<Sprite2D>("HandLeft");
		var handRight = _hands.GetNode<Sprite2D>("HandRight");
		if (handRight.GlobalPosition.Y > _hands.GlobalPosition.Y)
		{
			handLeft.ZIndex = -5;
			handRight.ZIndex = 5;
		}
		else
		{
			handLeft.ZIndex = 5;
			handRight.ZIndex = -5;
		}
		// Flip the player sprite based on the mouse position
		if (GetGlobalMousePosition().X < _skeleton.GlobalPosition.X)
		{
			_skeleton.GetNode<Node2D>("ConnectedBody").Scale = new Vector2(-1, 1);
			_skeleton.GetNode<Node2D>("Feet").Scale = new Vector2(-1, 1);
			_skeleton.GetNode<Node2D>("Hands/HandRight").Scale = new Vector2(1, -1);
			_skeleton.GetNode<Node2D>("Hands/HandLeft").Scale = new Vector2(1, -1);
		}
		else if (GetGlobalMousePosition().X >= _skeleton.GlobalPosition.X)
		{
			_skeleton.GetNode<Node2D>("ConnectedBody").Scale = new Vector2(1, 1);
			_skeleton.GetNode<Node2D>("Feet").Scale = new Vector2(1, 1);
			_skeleton.GetNode<Node2D>("Hands/HandRight").Scale = new Vector2(1, 1);
			_skeleton.GetNode<Node2D>("Hands/HandLeft").Scale = new Vector2(1, 1);
		}

		/*
		if (Input.IsActionPressed("move_up"))
		{
			_playerSprite.Animation = "idle_back";
		}
		else if (Input.IsActionPressed("move_down"))
		{
			_playerSprite.Animation = "idle_front";
		}
		*/
	}

	private void AnimateMovement(float delta)
	{
		var ankleLeft = _feet.GetNode<Node2D>("AnkleLeft");
		var ankleRight = _feet.GetNode<Node2D>("AnkleRight");

		if (ankleLeft.RotationDegrees > 30)
		{
			ankleLeft.Rotate(Mathf.DegToRad(-360 * delta));
			ankleRight.Rotate(Mathf.DegToRad(360 * delta));
			return;
		}
		else if (ankleLeft.RotationDegrees < -30)
		{
			ankleLeft.Rotate(Mathf.DegToRad(360 * delta));
			ankleRight.Rotate(Mathf.DegToRad(-360 * delta));
			return;
		}
		ankleLeft.Rotate(Mathf.DegToRad(360 * delta));
		ankleRight.Rotate(Mathf.DegToRad(-360 * delta));
	}
}
