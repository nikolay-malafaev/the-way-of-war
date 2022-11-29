using System;
using UnityEngine;

public class GrenadeController : Weapon
{
    [NamesElementsArray(typeof(Grenade.TypeGrenade))] [SerializeField] private Sprite[] grenadesSprites;
    [SerializeField] private Grenade grenade;
    public Grenade.TypeGrenade CurrenTypeGrenade { private get; set;}
    public Vector3 DirectionCastGrenade { private get; set; }
    private float srx;

    private void Start()
    {
        srx = spriteRenderer.transform.localPosition.x;
        EventManager.ChangeTypeWeapon.AddListener(ChangeTypeWeapon);
    }

    public override void Attack()
    {
        Grenade newGrenade = PoolManager.GetObject<ShardGrenade>(firePosition.position); //bug
        newGrenade.DirectionCastGrenade = DirectionCastGrenade;
        newGrenade.Attack();
    }

    private void ChangeTypeWeapon(TypeWeapon typeWeapon)
    {
        if (typeWeapon != TypeWeapon.Grenade) return;
        else if (!UIManager.Instance.ButtonsControlActiveSelf)
        {
            spriteRenderer.sprite = null;
            return;
        }
        spriteRenderer.sprite = grenadesSprites[(int) CurrenTypeGrenade];
    }

    private void ChangeTypeGrenade()
    {
        CurrenTypeGrenade = UIManager.currenTypeGrenade;
    }

    public override void Flip(bool condition)
    {
        spriteRenderer.flipX = condition;
        Vector3 fp = firePosition.transform.localPosition;
        Vector3 srp = spriteRenderer.transform.localPosition;
        firePosition.transform.localPosition = condition ? new Vector3(-x, fp.y, fp.z) : new Vector3(x, fp.y, fp.z);
        spriteRenderer.transform.localPosition = condition ? new Vector3(-srx, srp.y, srp.z) : new Vector3(srx, srp.y, srp.z);
    }
}