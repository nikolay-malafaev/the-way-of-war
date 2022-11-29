using System;
using UnityEngine;

public abstract class Grenade : MonoBehaviour
{
    [SerializeField] protected TypeGrenade typeGrenade;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    public Vector3 DirectionCastGrenade { get; set; }
    public enum TypeGrenade { Shard, Smoke, Flash, Molotov }
    public abstract void Attack();
}