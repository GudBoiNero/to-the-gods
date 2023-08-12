using Godot;
using System;

public partial class Player : LivingEntity
{
    [ExportGroup("Movement")]
    [Export]
    public float Speed = 120.0f;
    [Export]
    public float Friction = 0.65f;
    [Export]
    public float RollSpeed = 240f;
    [Export]
    public float RollDuration = 0.5f;
    [Export]
    public float RollFriction = 0.2f;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _UnhandledInput(InputEvent inputEvent) {
        // Handle rolling and attacking here
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        Vector2 direction = Input.GetVector("left", "right", "up", "down").Normalized();

        velocity.X = Mathf.Lerp(velocity.X, Speed * direction.X, Friction);
        velocity.Y = Mathf.Lerp(velocity.Y, Speed * direction.Y, Friction);

        Velocity = velocity;
        MoveAndSlide();
    }
}