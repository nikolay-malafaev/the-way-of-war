using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : UIObjects
{
    private bool cdVisualIcon;
    private float period;
    private double nextActionTime;
    private Player player;

    #region Singleton

    public static UIManager Instance { get; private set; }

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = Player.Instance;
        shotButton.onClick.AddListener(player.ShotBullet);
    }

    void Update()
    {
        if (cdVisualIcon)
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                cdIcon.fillAmount += 0.01f;
                if (cdIcon.fillAmount >= 1)
                {
                    cdVisualIcon = false;
                    cdIcon.fillAmount = 0;
                }
            }
        }
    }

    public float HealthPointsPlayer { set { healthBar.fillAmount = value / 100; } }
    public int CountBulletPLayer { set { countBulletText.text = value.ToString(); } }
    public bool ButtonsControlActiveSelf {  get; private set; }

    public Button ChangeWeaponButton
    {
        get { return changeWeaponButton; }
        set { changeWeaponButton = value; }
    }
    

    public Weapon.TypeWeapon LyingWeapon { private get; set; }

    public void ChangeWeapon()
    {
        EventManager.ChangeTypeWeapon.Invoke(LyingWeapon);
        Debug.Log(LyingWeapon);
    }

    public void StartDrawCoolDown(float value)
    {
        cdVisualIcon = true;
        period = value / 100;
        nextActionTime = Time.time;
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void GrenadeCastEvent()
    {
        int countUnNull = 0;
        foreach (var grenade in player.CountGrenade)
        {
            if (grenade > 0) countUnNull++;
        }

        if (countUnNull == 0)
        {
            // вызывать звук ошибки. Мб показать текст ошибки
            return;
        }

        if (countUnNull > 1) arrowsGrenades.SetActive(true);

        foreach (var grenade in player.CountGrenade)
        {
            if (player.CountGrenade[(int) currenTypeGrenade] == 0)
            {
                currenTypeGrenade++;
            }
            else break;
        }

        buttonsControl.SetActive(false);
        buttonsGrenades.SetActive(true);

        grenadeTypeIcon[(int) currenTypeGrenade].SetActive(true);
        ButtonsControlActiveSelf = true;
        EventManager.ChangeTypeWeapon.Invoke(Weapon.TypeWeapon.Grenade);
    }

    public void GrenadeCastEventClose()
    {
        buttonsGrenades.SetActive(false);
        arrowsGrenades.SetActive(false);
        grenadeTypeIcon[(int) currenTypeGrenade].SetActive(false);
        buttonsControl.SetActive(true);
        ButtonsControlActiveSelf = false;
        EventManager.ChangeTypeWeapon.Invoke(Weapon.TypeWeapon.Grenade);
    }

    public void ChangeTypeGrenade(int side)
    {
        grenadeTypeIcon[(int) currenTypeGrenade].SetActive(false);
        
        currenTypeGrenade += side;
        for (int i = 0; i < player.CountGrenade.Length; i++)
        {
            if ((int) currenTypeGrenade == 4) currenTypeGrenade = (Grenade.TypeGrenade) 0;
            if ((int) currenTypeGrenade == -1) currenTypeGrenade = (Grenade.TypeGrenade) 3;
            if (player.CountGrenade[(int) currenTypeGrenade] == 0) currenTypeGrenade += side;
        }

        grenadeTypeIcon[(int) currenTypeGrenade].SetActive(true);
        //EventManager.ChangeTypeGrenade.Invoke(currenTypeGrenade);
    }
}

