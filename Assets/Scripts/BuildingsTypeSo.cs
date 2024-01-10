using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Buildings")]
public class BuildingsTypeSo : ScriptableObject
{
    public Transform prefab;
    public Sprite buildingSprite;
    public float blockConstracionRadius = 10;
}
