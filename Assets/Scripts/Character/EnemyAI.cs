using System;
using UnityEngine;
using System.Collections;
using Tools;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : Character, IMilitary
{
    [SerializeField] private WeaponController weaponController;
    
    [Header("UI")]
    [SerializeField] private GameObject wary;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Sprite post;
    
    [Space]
    [Range(0, 10)][SerializeField] private float distancePatrol;

    [Space]
    [Range(1, 3)] [SerializeField] private int enemyLevel;

    [Space]
    [SerializeField] private float maxSpeedEnemy;
    [SerializeField] private float redDistance;
    [SerializeField] private Weapon.TypeWeapon typeWeapon;

    private int direction = 1;
    private bool isMove;
    private LayerMask playerLayer;
    private Vector3 startPosition;
    private Vector3 distationRayCast;
    private Vector2 directionRayCast;
    private bool chasePlayer;
    private int choiceFromLevel;
    private bool isStay;
    private float timeRemaining;
    private float maxHealthPointsEnemy;
    private float scaleHealthBar;


    private void Start()
    {
        InitFields();
        playerLayer = LayerMask.GetMask("Player");
        startPosition = transform.localPosition;
        switch (enemyLevel)
        {
            case 1:
                choiceFromLevel = 30;
                break;
            case 2:
                choiceFromLevel = 20;
                break;
            case 3:
                choiceFromLevel = 10;
                break;
        }

        maxHealthPointsEnemy = health.HeathPoints;
        scaleHealthBar = healthBar.transform.localScale.x;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        FindPlayer();
        wary.SetActive(isStay);
    }

    protected override float MaxSpeed
    {
        get { return maxSpeedEnemy;}
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
            if (chasePlayer)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    timeRemaining = 1f;
                    ChangeDirection(!CurrenDirection);
                }
            }
            if(isStay && !chasePlayer) return new Vector2(0.0f, 0.0f);
            else if(isStay && chasePlayer) return new Vector2(direction, 0.0f);
            
            if (transform.position.x > startPosition.x + distancePatrol)
            {
                ChangeDirection(true);
            }
            else if (transform.position.x < startPosition.x - distancePatrol)
            {
                ChangeDirection(false);
            }
            
            return new Vector2(direction, 0.0f);
        }
    }

    private void ChangeDirection(bool condition)
    {
        directionRayCast = condition ? Vector2.right : Vector2.left;
        distationRayCast = condition ? new Vector3(-0.2f, 0, 0) : new Vector3(0.2f, 0, 0);
        if (condition) direction = -1;
        else direction = 1;
        spriteRenderer.flipX = condition;
    }

    private bool CurrenDirection
    {
        get
        {
            var condition = direction != 1;
            return condition;
        }
    }
    
    private void FindPlayer()
    {
        //Debug.DrawRay(transform.position + distationRayCast, directionRayCast * -redDistance, Color.red);

        bool hit = Physics2D.Raycast(transform.position + distationRayCast, directionRayCast, -redDistance, playerLayer);
        if(hit)
        {
            chasePlayer = false;
            int choiceShot = UnityEngine.Random.Range(0, 100);
            if (choiceShot < choiceFromLevel)
            {
                ShotBullet();
            }
        } 
        else if (isStay)
        {
            if(!chasePlayer) StartCoroutine(WaitPlayer());
        }
        if(!chasePlayer) isStay = hit;
    }
    
    private IEnumerator WaitPlayer()
    {
        float time = 0;
        switch (enemyLevel)
        {
            case 1:
                yield break;
            case 2:
                time = 5;
                break;
            case 3:
                time = 10;
                break;
        }
        isStay = true;
        chasePlayer = true;
        yield return new WaitForSeconds(time);
        chasePlayer = false;
        isStay = false;
    }

    public void CastGrenade(Vector3 direction)
    {
        
    }
    
    public void ShotBullet()
    {
        if (!weaponController.CanShot) return;
        weaponController.Attack(typeWeapon);
    }
    
    protected override void TakeDamage()
    {
        float x = (health.HeathPoints / maxHealthPointsEnemy) * scaleHealthBar;
        healthBar.transform.localScale = new Vector3(x, 1, 1);
    }

#if UNITY_EDITOR

    [EditorButtonCreate("PutPatrolPosition")]
    public void PutPatrolPosition()
    {
        if(CheckPatrolPosition() || Application.isPlaying) return;

        GameObject post1 = new GameObject();
        GameObject post2 = new GameObject();
        
        post1.name = "Post 1";
        post2.name = "Post 2";
        
        SpriteRenderer sr1 = post1.AddComponent<SpriteRenderer>();
        SpriteRenderer sr2 = post2.AddComponent<SpriteRenderer>();

        sr1.sprite = post;
        sr2.sprite = post;
        
        post1.tag = "Editor";
        post2.tag = "Editor";

        var pos = transform.position;
        post1.transform.position = new Vector3(pos.x + distancePatrol, pos.y - 0.16f, pos.z);
        post2.transform.position = new Vector3(pos.x - distancePatrol, pos.y - 0.16f, pos.z);
    }
    
    
    [EditorButtonRemove("RemovePatrolPosition")]
    public void RemovePatrolPosition()
    {
        if(!CheckPatrolPosition()) return;
        
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Editor");
        foreach (var go in gameObjects)
        {
            DestroyImmediate(go);
        }
    }
    private bool CheckPatrolPosition()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Editor");
        return gameObjects.Length != 0;
    }
#endif
}