using UnityEngine;


public class AssaultRifle : Weapon
{
    private LayerMask allyLayer;

    private void Start()
    {
        x = firePosition.localPosition.x;
        allyLayer = gameObject.layer;
    }
    
    public override void Attack()
    {
        Bullet newBullet = PoolManager.GetObject<Bullet>(firePosition.position);
        newBullet.Shot(spriteRenderer.flipX, damage, allyLayer);
    }
}