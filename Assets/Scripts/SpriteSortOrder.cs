using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isStatic = true;
    [SerializeField] private float offsetY = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)((transform.position.y-offsetY) * -5);
        if (isStatic)
        {
            Destroy(this);
        }
    }

}
