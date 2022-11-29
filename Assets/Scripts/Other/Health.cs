using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int heathPoints;
    [HideInInspector] public UnityEvent SendTakeDamage;

    public int HeathPoints
    {
        get { return heathPoints; }
        private set
        {
            if (value < 0) heathPoints = 0;
            else heathPoints = value;
        }
    }

    private void Start()
    {
        HealthManager.Instance.SendInstance(gameObject, this);
    }

    public void TakeDamage(int damage)
    {
        HeathPoints -= damage;
        SendTakeDamage.Invoke();
    }
}