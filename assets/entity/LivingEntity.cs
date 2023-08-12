using Godot;
using System;
public partial class LivingEntity : Entity
{
    [Export]
    public Area2D hurtboxArea;
    [Export]
    public CollisionShape2D hurtboxCollider;

    [ExportGroup("Combat")]
    [Export]
    public float KnockbackResistance = 0.2f;

    private bool isDead;
    public bool IsDead
    {
        get { return isDead; }
    }

    private bool isInvulnerable = false;
    public bool IsInvulnerable
    {
        set { isInvulnerable = value; }
        get { return isInvulnerable; }
    }

    public LivingEntity() { }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (!IsDead) _LivingProcess(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._Process(delta);

        if (!IsDead) _LivingPhysicsProcess(delta);
    }

    public override void _Ready()
    {
        base._Ready();

        hurtboxArea.CollisionMask = GetHurtboxCollisionMask();
        hurtboxArea.CollisionLayer = GetHurtboxCollisionLayer();

        hurtboxArea.Connect(Area2D.SignalName.AreaEntered, new(this, "Hit"));
    }

    public virtual void _LivingPhysicsProcess(double delta)
    {

    }

    public virtual void _LivingProcess(double delta)
    {

    }

    public virtual void Hit()
    {
        if (IsInvulnerable) return;

        isDead = true;
        OnDeath();
    }

    public virtual void OnDeath()
    {
        // Poof
    }

    public virtual uint GetHurtboxCollisionMask()
    {
        return (uint)Math.Pow(2, 1 - 1);
    }
    
    public virtual uint GetHurtboxCollisionLayer()
    {
        return (uint)Math.Pow(2, 1 - 1);
    }
}