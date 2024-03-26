using Godot;
using System;

public partial class InputManager : Node
{
	private VBoxContainer _console;

	public override void _Ready()
	{
		_console = GetNode<VBoxContainer>("/root/Main/CanvasLayer/ConsoleContainer");
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("toggle_console"))
		{
			_console.Visible = !_console.Visible;
		}
	}
}
