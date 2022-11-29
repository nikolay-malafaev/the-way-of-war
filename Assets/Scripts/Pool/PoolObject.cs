using UnityEngine;

[AddComponentMenu("Pool/PoolObject")]
public class PoolObject : MonoBehaviour
{
    [SerializeField] private Object pooledObject;
    
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
    
    public T GetInstance<T>(Vector3 position) where T : Object
    {
        gameObject.SetActive(true);
        transform.position = position;
        return (T) pooledObject;
    }
}