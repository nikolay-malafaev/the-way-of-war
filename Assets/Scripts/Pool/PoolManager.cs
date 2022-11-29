using UnityEngine;

public static class PoolManager
{
    private static PoolPart[] pools;
    public static GameObject objectsParent;

    [System.Serializable]
    public struct PoolPart
    {
        public string name;
        public PoolObject prefab;
        public int count;
        public ObjectPooling objectPooling;
    }

    public static void Initialize(PoolPart[] newPools, Transform objectPoolSetup)
    {
        pools = newPools;
        for (int i = 0; i < pools.Length; i++)
        {
            if (pools[i].prefab != null)
            {
                pools[i].objectPooling = new ObjectPooling();
                pools[i].objectPooling.Initialize(pools[i].count, pools[i].prefab, objectPoolSetup);
            }
        }
    }
    
    public static T GetObject<T>(Vector3 position) where T : Object
    {
        T result = default(T);
        
        if (pools != null)
        {
            for (int i = 0; i < pools.Length; i++)
            {
                if (typeof(T).ToString() == pools[i].name)
                {
                    result = pools[i].objectPooling.GetPoolingInstance<T>(position);
                    return result;
                }
            }
        }
        
        return result;
    }
}