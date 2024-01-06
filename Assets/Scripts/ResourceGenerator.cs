using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private Resource resourceType;
    [SerializeField] private int resourcePerTimeInterval = 2;
    [SerializeField] private float timeInterval = 2;
    private float currentTime = 0;

    private void Update()
    {
        if(currentTime < timeInterval)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            ResourceManager.Instance.AddResource(resourceType, resourcePerTimeInterval);
        }
    }
}
