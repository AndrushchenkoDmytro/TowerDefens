using UnityEngine;

public static class UtilsClass {

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
