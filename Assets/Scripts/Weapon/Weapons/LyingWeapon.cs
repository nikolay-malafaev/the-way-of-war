using UnityEngine;

public class LyingWeapon : MonoBehaviour
{
    [SerializeField] private Weapon.TypeWeapon typeWeapon;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [NamesElementsArray(typeof(Weapon.TypeWeapon))] [SerializeField] private Sprite[] spritesWeapons;
    private LayerMask playerLayer;
    private bool isTouchPlayer;
    
    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        UIManager.Instance.ChangeWeaponButton.onClick.AddListener(ChangeTypeWeapon);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == playerLayer)
        {
            UIManager.Instance.ChangeWeaponButton.gameObject.SetActive(true);
            UIManager.Instance.LyingWeapon = typeWeapon;
            isTouchPlayer = true;
        }
    }
    
    private void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.layer == playerLayer)
        {
            UIManager.Instance.ChangeWeaponButton.gameObject.SetActive(false);
            isTouchPlayer = false;
        }
    }
    
    private void ChangeTypeWeapon()
    {
        if (!isTouchPlayer) return;
        typeWeapon = Player.Instance.lastTypeWeapon;
        UIManager.Instance.ChangeWeapon();
        ChangeSprite();
    }
    
    void ChangeSprite()
    {
        spriteRenderer.sprite = spritesWeapons[(int)typeWeapon];
    }
    
    void OnValidate()
    {
        ChangeSprite();
    }
}