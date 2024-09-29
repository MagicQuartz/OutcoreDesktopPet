using Godot;
using GodotPlugins.Game;
using System;

public partial class lumi : CharacterBody2D
{
	main parentMain;
	private bool held = false;
	private bool onGround = true;
	Vector2I windowGrabPosition;
	AnimatedSprite2D sprite;

	int verticalAccelaration = 0;
	int prevWindowX;
	bool hFlipped = false; // When flipped, sprite looks left.
	Window _MainWindow;

    public override void _Ready()
    {
		_MainWindow = GetWindow();
		parentMain = GetParent<main>();
		prevWindowX = _MainWindow.Position.X;
        sprite = GetNode<AnimatedSprite2D>("Sprite");
		windowGrabPosition = (Vector2I) GetNode<Marker2D>("GrabPos").Position;
		sprite.Play("wave");
    }

    public override void _Process(double delta)
    {
        if(held)
		{
			prevWindowX = _MainWindow.Position.X;
			Vector2I windowPosition = DisplayServer.MouseGetPosition() - windowGrabPosition;// - parentMain.player_size / 2; // Calculates against window/sprite size
			_MainWindow.Position = windowPosition;
			if(prevWindowX != windowPosition.X)
				hFlipped = prevWindowX < windowPosition.X; // hFlipped = should look left
		}
    }

    public override void _PhysicsProcess(double delta)
	{
		if(!held && !onGround)
		{
			if(_MainWindow.Position.Y < parentMain.taskbar_pos)
			{
				_MainWindow.Position += Vector2I.Down * verticalAccelaration;
				if(verticalAccelaration < 30) // Acceleration when falling
					verticalAccelaration++;
			} else // if lumi is at or below the taskbar 
			{
				sprite.Play("balancing");
				_MainWindow.Position = new Vector2I(_MainWindow.Position.X, parentMain.taskbar_pos);
				verticalAccelaration = 0;
				onGround = true;
			}
		}
	}

	private void _inputEvent(Node viewport, InputEvent @event, int shape_idx)
	{
		if(@event is InputEventMouseButton mouseButton)
		{
			if(mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsReleased())
			{
				held = false;
				sprite.FlipH = hFlipped; // Plays once lumi is let go
				if(_MainWindow.Position.Y >= parentMain.taskbar_pos) // if lumi is below the taskbar and is let go
				{
					sprite.Play("squish");
					_MainWindow.Position = new Vector2I(_MainWindow.Position.X, parentMain.taskbar_pos);
					onGround = true;
				} else {
					sprite.Play("spin");
				}
			} else if(mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsPressed())
			{
				held = true;
				sprite.FlipH = false;
				onGround = false;
				sprite.Play("mouse_grab");
				//windowGrabPosition = (Vector2I) GetViewport().GetMousePosition();
			}
		}
	}

	private void _animationFinished()
	{
		if(sprite.Animation.Equals("squish"))
			sprite.Play("balancing");
	}
}
