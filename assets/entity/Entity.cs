using Godot;
using System;

public partial class Entity : CharacterBody2D
{
	[Export]
	public Sprite2D sprite;
	[Export]
	public CollisionShape2D collider;

	private int spriteDirection = 1;
    public int GetCurrentSpriteDirection
    {
        get { return spriteDirection; }
    }

	public override void _Ready()
	{
		CollisionMask = GetCollisionMask();
		CollisionLayer = GetCollisionLayer();
	}

	public override void _PhysicsProcess(double delta) {
		// Set sprite scale to face in the correct direction of velocity
		spriteDirection = GetSpriteDirection();
		sprite.Scale = new(spriteDirection, sprite.Scale.Y);
	}

	public virtual int GetSpriteDirection() {
		return 1;
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
