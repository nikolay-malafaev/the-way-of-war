using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : Character, IMilitary
{
    [SerializeField] private WeaponController weaponController;
    private Weapon.TypeWeapon currenTypeWeapon;
    
    [Space]
    [SerializeField] private int countBullet;
    
    [Space]
    [NamesElementsArray(typeof(Grenade.TypeGrenade))] 
    [SerializeField] private int[] countGrenade;

    private int CountBullet
    {
        get { return countBullet; }
        set
        {
            if (value < 0) countBullet = 0;
            else countBullet = value;
        }
    }
    public int[] CountGrenade
    {
        get { return countGrenade; }
        private set
        {
            countGrenade = value;
        }
    }
    
    private bool canMove = true;
    public bool CanMove
    {
        private get { return canMove; }
        set { canMove = value; }
    }
    public Weapon.TypeWeapon lastTypeWeapon { get; private set; }
    private UIManager UIManager;
    

    #region Singleton
    public static Player Instance { get; private set; } 
    #endregion
    
    private void Awake()
    {
        Instance = this;
        EventManager.ChangeTypeWeapon.AddListener(ChangeTypeWeapon);
    }

    private void Start()
    {
        InitFields();
        UIManager = UIManager.Instance;
        UIManager.CountBulletPLayer = CountBullet;
        EventManager.CastGrenade.AddListener(CastGrenade);
        InitializeWeapon();
    }

    private void FixedUpdate()
    {
        if (!CanMove) return;
        Move();
        Jump();
    }
    
    protected override void Jump()
    {
        if (isGrounded && (Input.GetAxis("Jump") > 0))
        {
            rigidbody2D.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        }
    }
    
    protected override Vector2 MovementVector
    {
        get
        {
            var horizontal = Input.GetAxis("Horizontal");
            if (horizontal < 0) spriteRenderer.flipX = true;
            else if (horizontal > 0) spriteRenderer.flipX = false;
            return new Vector2(horizontal, 0.0f);
        }
    }

    private void ChangeTypeWeapon(Weapon.TypeWeapon typeWeapon)
    {
        weaponController.ChangeTypeWeapon(typeWeapon);
        if (currenTypeWeapon != Weapon.TypeWeapon.Grenade) lastTypeWeapon = currenTypeWeapon;
        if (typeWeapon == Weapon.TypeWeapon.Grenade && !UIManager.ButtonsControlActiveSelf)
        {
            currenTypeWeapon = lastTypeWeapon;
            return;
        }
        currenTypeWeapon = typeWeapon;
    }

    private void InitializeWeapon()
    {
        //currenWeapon = GetSaveWeapon
        currenTypeWeapon = Weapon.TypeWeapon.AssaultRifle;
        lastTypeWeapon = currenTypeWeapon;
        weaponController.InitializeWeapon(currenTypeWeapon);
    }

    public void ShotBullet()
    {
        if (CountBullet == 0 || !weaponController.CanShot) return;
        
        weaponController.Attack(currenTypeWeapon);
        CountBullet -= 1;
        UIManager.CountBulletPLayer = CountBullet;
        UIManager.StartDrawCoolDown(weaponController.GetCoolDown(currenTypeWeapon));
    }
    
    public void CastGrenade(Vector3 direction)
    {
        DirectionCastGrenade = direction;
        CountGrenade[(int)UIManager.currenTypeGrenade]--;
        weaponController.Attack(Weapon.TypeWeapon.Grenade);
    }

    protected override void TakeDamage()
    {
        UIManager.HealthPointsPlayer = health.HeathPoints;
    }
}