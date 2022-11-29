using UnityEngine;

public class ShardGrenade : Grenade
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Animator animator;

    public override void Attack()
    {
        rigidbody2D.AddForce(DirectionCastGrenade * 5, ForceMode2D.Impulse);
        animator.SetTrigger("cast");
        // blind
    }
}