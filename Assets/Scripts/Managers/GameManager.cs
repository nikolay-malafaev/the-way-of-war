using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private EventManager eventManager;
    public bool test;
    public GameObject[] destructibleObjects;
    
    #region Singleton
    public static GameManager Instance { get; private set; } 
    #endregion
    
    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        
#if UNITY_EDITOR
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Editor");
        foreach (var go in gameObjects)
        {
            DestroyImmediate(go);
        }
#endif
    }
    
    private void FindDestructibleObjects()
    {
        destructibleObjects = GameObject.FindGameObjectsWithTag("Destructible");
        if (destructibleObjects.Length == 0) SceneManager.LoadScene(0);
    }
}
