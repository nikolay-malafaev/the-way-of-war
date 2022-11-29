using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class WeaponController : MonoBehaviour
{
    [Header("Owner")] [SerializeField] private Character owner;

    [SerializeField] private Weapon currenWeapon; // temporarily
    private Weapon lastWeapon;
    [Space]
    [SerializeField] private GrenadeController grenadeController;

    private bool canShot = true;
    public bool CanShot
    {
        get { return canShot; }
    }
    
    private void Update()
    {
        currenWeapon.Flip(owner.Orientation);
    }

    public void Attack(Weapon.TypeWeapon typeWeapon)
    {
        if (typeWeapon == Weapon.TypeWeapon.Grenade) grenadeController.DirectionCastGrenade = owner.DirectionCastGrenade;
        currenWeapon.Attack();
        StartCoroutine(StartCoolDown(currenWeapon.CoolDown));
        canShot = false;
    }

    public void ChangeTypeWeapon(Weapon.TypeWeapon typeWeapon)
    {
        if (typeWeapon == Weapon.TypeWeapon.Grenade)
        {
            if (currenWeapon != grenadeController)
            {
                lastWeapon = currenWeapon;
                currenWeapon.gameObject.SetActive(!currenWeapon.gameObject.activeSelf);
                currenWeapon = grenadeController;
            }
            else
            {
                currenWeapon = lastWeapon;
                currenWeapon.gameObject.SetActive(!currenWeapon.gameObject.activeSelf);
            }
            return;
        }

        currenWeapon.gameObject.SetActive(!currenWeapon.gameObject.activeSelf);
        currenWeapon.transform.SetParent(PoolManager.objectsParent.transform);
        GetWeapon(typeWeapon);
    }

    public void InitializeWeapon(Weapon.TypeWeapon typeWeapon)
    {
        GetWeapon(typeWeapon);
    }

    private void GetWeapon(Weapon.TypeWeapon typeWeapon)
    {
        switch (typeWeapon)
        {
            case Weapon.TypeWeapon.AssaultRifle:
                currenWeapon = PoolManager.GetObject<AssaultRifle>(transform.position);
                break;
            case Weapon.TypeWeapon.SniperRifle:
                currenWeapon = PoolManager.GetObject<SniperRifle>(transform.position);
                break;
            case Weapon.TypeWeapon.Grenade:
                break;
            case Weapon.TypeWeapon.Knife:
                break;
            case Weapon.TypeWeapon.Pistol:
                break;
        }
        currenWeapon.transform.SetParent(transform);
    }

    public float GetCoolDown(Weapon.TypeWeapon typeWeapon)
    {
        return 5;
    }
    
    private IEnumerator StartCoolDown(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        canShot = true;
    }
}
