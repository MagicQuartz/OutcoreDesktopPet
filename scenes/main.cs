using Godot;
using System;
using System.Runtime.InteropServices;

public partial class main : Node2D
{	

	public Window _MainWindow;

	[Export]
	public Vector2I player_size = new Vector2I(128, 128);
	public int taskbar_pos;

	public override void _Ready()
	{
		_MainWindow = GetWindow();
		_MainWindow.MinSize = player_size;
		_MainWindow.Size = _MainWindow.MinSize;

		taskbar_pos = DisplayServer.ScreenGetUsableRect().Size.Y - player_size.Y;

		/*ProjectSettings.SetSetting("display/window/per_pixel_transparency/allowed", true);
		// Set the window settings - most of them can be set in the project settings
		_MainWindow.Borderless = true;
		_MainWindow.Unresizable = true;
		_MainWindow.AlwaysOnTop = true;
		_MainWindow.GuiEmbedSubwindows = false;
		_MainWindow.Transparent = true;*/
		// Settings that cannot be set in project settings
		_MainWindow.TransparentBg = true;

		_MainWindow.Position = new Vector2I(DisplayServer.ScreenGetSize().X/2 - (player_size.X/2), taskbar_pos);
	}

	public override void _Process(double delta)
	{
		/*if(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Minimized)
		{
			if(!IsFullscreenAppRunning())
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		} else if(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Windowed)
		{
			if(IsFullscreenAppRunning())
			{
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Minimized);
			} 
		}*/
	}
}
