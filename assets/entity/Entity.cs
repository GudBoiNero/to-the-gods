using Godot;
using System;

public partial class Entity : CharacterBody2D
{
	[Export]
	public Sprite2D sprite;
	[Export]
	public CollisionShape2D collider;

	public override void _Ready()
	{
		CollisionMask = GetCollisionMask();
		CollisionLayer = GetCollisionLayer();
	}

	public virtual uint GetCollisionMask()
	{
		return (uint)Math.Pow(2, 1 - 1);
	}

	public virtual uint GetCollisionLayer()
	{
		return (uint)Math.Pow(2, 1 - 1);
	}
}
