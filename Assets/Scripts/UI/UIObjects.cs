using UnityEngine;
using UnityEngine.UI;

public abstract class UIObjects : MonoBehaviour
{
    [Header("Player Elements")]
    [SerializeField] protected Image healthBar;
    [SerializeField] protected Text countBulletText;
    [SerializeField] protected Image cdIcon;
    
    [NamesElementsArray(typeof(Grenade.TypeGrenade))] [SerializeField] protected Text[] countGrenadeText;
    [Header("Player Buttons")]
    [SerializeField] protected Button shotButton;
    [SerializeField] protected Button changeWeaponButton;
    

    [Space]
    [SerializeField] protected GameObject buttonsControl;
    [SerializeField] protected GameObject panelGameOver;
    
    [Header("Grenades Objects")]
    [SerializeField] protected GameObject buttonsGrenades;
    [SerializeField] protected GameObject arrowsGrenades;
    [NamesElementsArray(typeof(Grenade.TypeGrenade))] [SerializeField] protected GameObject[] grenadeTypeIcon;
    public static Grenade.TypeGrenade currenTypeGrenade;
}