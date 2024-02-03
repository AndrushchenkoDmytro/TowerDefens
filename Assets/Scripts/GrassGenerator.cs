using UnityEngine;

public class GrassGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] grassPrefab;
    private int grassCount = 0;
    Vector3 offset = Vector3.zero;
    private float r = 6;

    private void Awake()
    {
        grassCount = Random.Range(6, 13);
        for (int i = 0; i < grassCount; i++)
        {
            offset = new Vector3(Random.Range(-r, r), Random.Range(-r, r), 0);
            Instantiate(grassPrefab[Random.Range(0, grassPrefab.Length)], transform.position + offset, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
