using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Buildings")]
public class BuildingsTypeSo : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public Sprite buildingSprite;

}
