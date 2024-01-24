using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesDestroyerEnemy : MonoBehaviour, IPoolable
{
    public event Action<IPoolable> OnDestroy;

    public void Reset()
    {
        gameObject.SetActive(false);
    }
}
