using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent FindDestructibleObjects;
    public static UnityEvent<Vector3> CastGrenade = new UnityEvent<Vector3>();
    public static UnityEvent<Weapon.TypeWeapon> ChangeTypeWeapon = new UnityEvent<Weapon.TypeWeapon>();
}
