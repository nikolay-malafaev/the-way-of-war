using System.Collections.Generic;
using UnityEngine;


public class HealthManager : MonoBehaviour
{
    private Dictionary<GameObject, Health> healthContainer;

    #region Singleton

    public static HealthManager Instance { get; private set; }

    #endregion

    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
    }

    public Health TakeInstance(GameObject key)
    {
        var health = healthContainer[key];
        return health;
    }

    public bool ContainsKey(GameObject key)
    {
        return healthContainer.ContainsKey(key);
    }

    public void SendInstance(GameObject key, Health health)
    {
        healthContainer.Add(key, health);
    }
}