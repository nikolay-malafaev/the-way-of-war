using Tools;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Transform firePosition;
    [SerializeField] protected float coolDown;
    [SerializeField] protected int damage;
    protected float x;

    public TypeWeapon type;
    public Vector3 weaponPosition;

    public float CoolDown { get { return coolDown; } }

    public abstract void Attack();

    public virtual void Flip(bool condition)
    {
        spriteRenderer.flipX = condition;
        Vector3 fp = firePosition.transform.localPosition;
        firePosition.transform.localPosition = condition ? new Vector3(-x, fp.y, fp.z) : new Vector3(x, fp.y, fp.z);
    }

    public enum TypeWeapon
    {
        AssaultRifle,
        SniperRifle,
        Grenade,
        Knife,
        Pistol
    }
}