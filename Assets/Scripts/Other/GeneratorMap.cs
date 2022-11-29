using System;
using System.IO;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.JsonUtility;

public class GeneratorMap : MonoBehaviour
{
    public Tilemap tileMapBase;
    private TileBase[] tilesInLevel;
    private BoundsInt boundsInLevel;
    public Tilemap saveTileMap;
    public Level level = new Level();
    
    public GameObject enemies;
    private GameManager gameManager;
    [SerializeField] [Range(0, 10)] private int currenLevel;
    

#if UNITY_EDITOR
    [EditorButton("SaveLevel")]
    public void SaveLevel()
    {
        boundsInLevel = tileMapBase.cellBounds;
        tilesInLevel = tileMapBase.GetTilesBlock(boundsInLevel);
        var childCount = enemies.transform.childCount;
        Vector3[] positionEnemies = new Vector3[childCount];
        int[] levelEnemies = new int[childCount];
        for (int i = 0; i < enemies.transform.childCount; i++)
        {
            positionEnemies[i] =
                enemies.transform.GetChild(i).transform.position;
            //levelEnemies[i] =
             //   enemies.transform.GetChild(i).GetComponent<EnemyAI>().enemyLevel;
        }
        level.tileBaseLayer = tilesInLevel;
        level.boundsBaseLayer = boundsInLevel;
        level.positionEnemies = positionEnemies;
        level.levelEnemies = levelEnemies;
        string result = ToJson(level);
        File.WriteAllText(Application.streamingAssetsPath + @"\level1.json", result);
    }
#endif

    public void FromInfoJson()
    {
        var path = File.ReadAllText (Application.streamingAssetsPath + @"\level1.json");
        level = FromJson<Level>(path);
        tileMapBase.SetTilesBlock(level.boundsBaseLayer, level.tileBaseLayer);
    }

    public void GenerateLevel(int level)
    {
        
    }
}


[Serializable]
public class Level
{
    public TileBase[] tileBaseLayer;
    public BoundsInt boundsBaseLayer;
    public TileBase[] tileDecorLayer;
    public BoundsInt boundsDecorLayer;
    
    public Vector3[] positionEnemies;
    public int[] levelEnemies;
    public Vector3 positionEntity;
}