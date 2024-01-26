using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass {

    private static Camera mainCamera;

    static UtilsClass()
    {
        mainCamera = Camera.main;
    }

    public static Vector3 GetMouseWorldPosition() {
        //if (mainCamera == null) mainCamera = Camera.main;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    public static Vector3 GetRandomDir() 
    {
        return new Vector3(
            Random.Range(-1f, 1f), 
            Random.Range(-1f, 1f)
        ).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector) 
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

}
