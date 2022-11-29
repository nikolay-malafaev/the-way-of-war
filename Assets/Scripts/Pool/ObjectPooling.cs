using UnityEngine;
using System.Collections.Generic;

public class ObjectPooling
{
    private GameObject _prefab;
    private List<PoolObject> objects;
    private Transform objectsParent;

    void AddObject(PoolObject sample, Transform objectsParent)
    {
        PoolObject temp = PoolObject.Instantiate(sample);
        
        temp.name = sample.name;
        temp.transform.SetParent(objectsParent);
        objects.Add(temp.GetComponent<PoolObject>());
        temp.gameObject.SetActive(false);
    }

    public void Initialize(int count, PoolObject sample, Transform objectsParent)
    {
        objects = new List<PoolObject>();
        this.objectsParent = objectsParent;
        for (int i = 0; i < count; i++)
        {
            AddObject(sample, objectsParent);
        }
    }
    
    public T GetPoolingInstance<T>(Vector3 position) where T : Object
    {
        foreach (var obj in objects)
        {
            if (obj.gameObject.activeInHierarchy == false)
            {
                return obj.GetInstance<T>(position);
            }
        }
        
        Debug.LogError("Lack of a pool!");
        AddObject(objects[0], objectsParent);
        return objects[objects.Count - 1].GetInstance<T>(position);
    }
}