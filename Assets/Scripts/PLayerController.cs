using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera mainCamera;
    [SerializeField] private BuildingsTypeSo building;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMousePosition();
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(building.prefab,mousePosition,Quaternion.identity);
        }
    }

    private void UpdateMousePosition()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
    }
}
