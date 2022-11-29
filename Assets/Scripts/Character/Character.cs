using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float speedMove;
    [SerializeField] protected float forceJump;
    
    [Header("SerializedObjects")] 
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected CapsuleCollider2D collider;
    [SerializeField] protected Rigidbody2D rigidbody2D;
    [SerializeField] protected Health health;

    private LayerMask groundLayer;
    private float maxSpeed = 2.7f;

    protected void InitFields()
    {
        groundLayer = LayerMask.GetMask("Ground");
        health.SendTakeDamage.AddListener(TakeDamage);
    }
    
    protected void Move()
    {
        rigidbody2D.AddForce(MovementVector * speedMove, ForceMode2D.Impulse);
        rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.x, -MaxSpeed, MaxSpeed), rigidbody2D.velocity.y);
    }
    
    protected virtual void Jump()
    {
        if (isGrounded)
        {
            rigidbody2D.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        }
    }
    
    protected virtual Vector2 MovementVector { get; }
    
    protected bool isGrounded
    {
        get { return collider.IsTouchingLayers(groundLayer.value); }
    }
    
    public bool Orientation
    {
        get { return spriteRenderer.flipX;}
        set { spriteRenderer.flipX = value;}
    }

    protected virtual float MaxSpeed
    {
        get { return maxSpeed;}
    }
    
    public Vector3 DirectionCastGrenade { get; protected set; }
    
    protected abstract void TakeDamage();
}


interface IMilitary
{
    void CastGrenade(Vector3 direction);
    void ShotBullet();
}