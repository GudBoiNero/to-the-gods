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

    private int spriteSheetRow = 0;
    public int SpriteSheetRow
    {
        get
        {
            spriteSheetRow = GetSpriteSheetRow();
            return spriteSheetRow;
        }
    }

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        // Handle rolling and attacking here
    }

    /// <summary>
    /// Refer to <c>resources/player/player-sheet.png</c> to understand the return values.
    /// </summary>
    /// <returns></returns>
    public int GetSpriteSheetRow()
    {
        Vector2 input = Input.GetVector("left", "right", "up", "down");

        GD.Print(input);
        // If left or right
        if (Math.Abs(input.X) > 0)
        {
            if (input.Y < 0)
            {
                return 3; // Face up side
            }
            else if (input.Y > 0)
            {
                return 1; // Face down side
            }
            else
            {
                return 2;
            }
        }
        else
        {
            if (input.Y < 0)
            {
                return 4; // Face up
            }
            else if (input.Y > 0)
            {
                return 0; // Face down
            }
        }

        return spriteSheetRow;
    }

    public override void _LivingPhysicsProcess(double delta)
    {
        // Set sprite row
        sprite.FrameCoords = new(sprite.FrameCoords.X, SpriteSheetRow);

        Vector2 velocity = Velocity;
        Vector2 direction = Input.GetVector("left", "right", "up", "down").Normalized();

        velocity.X = Mathf.Lerp(velocity.X, Speed * direction.X, Friction);
        velocity.Y = Mathf.Lerp(velocity.Y, Speed * direction.Y, Friction);

        Velocity = velocity;
        MoveAndSlide();
    }

    public override int GetSpriteDirection()
    {
        float xInput = Input.GetVector("left", "right", "up", "down").X;
        return xInput != 0 ? (int)Math.Round(xInput) : GetCurrentSpriteDirection;
    }
}