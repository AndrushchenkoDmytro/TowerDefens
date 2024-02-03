using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component, IPoolable
{
    private GameObject prefab;
    private Transform container;
    private List<T> freeObjects;

    public ObjectPool(GameObject prefab, Transform contaier)
    {
        this.prefab = prefab;
        this.container = contaier;
        freeObjects = new List<T>();
    }

    public T GetObjectFromPool(Vector3 position)
    {
        T t;
        if (freeObjects.Count > 0)
        {
            t = freeObjects[0];
            freeObjects.RemoveAt(0);
        }
        else
        {
            t = SpawnNewObject();
        }
        t.OnPolableDestroy += ReturnObjectToPool;
        t.gameObject.SetActive(true);
        t.transform.position = position;
        return t;
    }

    private void ReturnObjectToPool(IPoolable poolable)
    {
        freeObjects.Add(poolable as T);
        poolable.OnPolableDestroy -= ReturnObjectToPool;
        poolable.Reset();
    }

    private T SpawnNewObject()
    {
        GameObject tObject = UnityEngine.Object.Instantiate(prefab, container);
        return tObject.GetComponent<T>();
    }


}
