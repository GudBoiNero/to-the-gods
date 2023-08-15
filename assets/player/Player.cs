using Godot;
using System;

public partial class Player : LivingEntity
{
    [Export]
    public AnimationPlayer animator;
    [Export]
    public Sprite2D swordSprite;

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

    public enum SpriteSheetRows { DOWN = 0, RIGHT_DOWN = 1, RIGHT = 2, RIGHT_UP = 3, UP = 4, LEFT_UP = 5, LEFT = 6, LEFT_DOWN = 7 }

    public enum PlayerState { FREE, ROLL, ATTACK, STANCE };
    private PlayerStateMachine stateMachine;

    private int spriteSheetRow = 0;
    public int SpriteSheetRow
    {
        get
        {
            spriteSheetRow = GetSpriteSheetRow();
            return spriteSheetRow;
        }
    }
    private int swordSpriteSheetRow = 0;
    public int SwordSpriteSheetRow
    {
        get
        {
            swordSpriteSheetRow = GetSwordSpriteSheetRow();
            return swordSpriteSheetRow;
        }
    }
    public Player()
    {
        stateMachine = new(this, PlayerState.FREE);
    }
    public override void _Ready()
    {
        base._Ready();

        AddChild(stateMachine);
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

        // If left or right
        if (Math.Abs(input.X) > 0)
        {
            if (input.Y < 0)
            {
                return (int)SpriteSheetRows.RIGHT_UP; // Face up side
            }
            else if (input.Y > 0)
            {
                return (int)SpriteSheetRows.RIGHT_DOWN; // Face down side
            }
            else
            {
                return (int)SpriteSheetRows.RIGHT;
            }
        }
        else
        {
            if (input.Y < 0)
            {
                return (int)SpriteSheetRows.UP; // Face up
            }
            else if (input.Y > 0)
            {
                return (int)SpriteSheetRows.DOWN; // Face down
            }
        }

        return spriteSheetRow;
    }
    /// <summary>
    /// Refer to <c>resources/player/sword-sheet.png</c> to understand the return values.
    /// </summary>
    /// <returns></returns>
    public int GetSwordSpriteSheetRow()
    {
        Vector2 input = Input.GetVector("left", "right", "up", "down");

        // If left or right
        if (input.X > 0)
        {
            if (input.Y < 0)
            {
                return (int)SpriteSheetRows.RIGHT_UP; // Face up right
            }
            else if (input.Y > 0)
            {
                return (int)SpriteSheetRows.RIGHT_DOWN; // Face down right
            }
            else
            {
                return (int)SpriteSheetRows.RIGHT;
            }
        }
        else if (input.X < 0)
        {
            if (input.Y < 0)
            {
                return (int)SpriteSheetRows.LEFT_UP; // Face up left
            }
            else if (input.Y > 0)
            {
                return (int)SpriteSheetRows.LEFT_DOWN; // Face down left
            }
            else
            {
                return (int)SpriteSheetRows.LEFT;
            }
        }
        else
        {
            if (input.Y < 0)
            {
                return (int)SpriteSheetRows.UP; // Face up
            }
            else if (input.Y > 0)
            {
                return (int)SpriteSheetRows.DOWN; // Face down
            }
        }

        return swordSpriteSheetRow;
    }
    public override void _LivingPhysicsProcess(double delta)
    {
        // Set sprite row
        sprite.FrameCoords = new(sprite.FrameCoords.X, SpriteSheetRow);
        swordSprite.FrameCoords = new(swordSprite.FrameCoords.X, SwordSpriteSheetRow);
        swordSprite.ZIndex = (
            SwordSpriteSheetRow == (int)SpriteSheetRows.DOWN 
            || SwordSpriteSheetRow == (int)SpriteSheetRows.LEFT_DOWN 
            || SwordSpriteSheetRow == (int)SpriteSheetRows.RIGHT_DOWN
        ) ? -1 : 0;

        Vector2 velocity = Velocity;
        Vector2 direction = Input.GetVector("left", "right", "up", "down").Normalized();

        velocity.X = Mathf.Lerp(velocity.X, Speed * direction.X, Friction);
        velocity.Y = Mathf.Lerp(velocity.Y, Speed * direction.Y, Friction);

        Velocity = velocity;
        MoveAndSlide();
    }
    /// <summary>
    /// Determines left from right based off of player input.
    /// </summary>
    /// <returns></returns>
    public override int GetSpriteDirection()
    {
        float xInput = Input.GetVector("left", "right", "up", "down").X;
        return xInput != 0 ? (int)Math.Round(xInput) : GetCurrentSpriteDirection;
    }
    private partial class PlayerStateMachine : StateMachine<PlayerState>
    {
        private Player player;

        public PlayerStateMachine(Player player, PlayerState startState)
        {
            State = startState;
            this.player = player;
        }

        public override void _Enter(PlayerState oldState, PlayerState newState)
        {

        }
        public override PlayerState _ChangeState(PlayerState oldState, PlayerState newState)
        {
            return newState;
        }
        public override void _Exit(PlayerState oldState, PlayerState newState)
        {

        }
    }
}