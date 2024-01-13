using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Buildings")]
public class BuildingsTypeSo : ScriptableObject
{
    public Transform prefab;
    public BuildingClass buildingClass;
    public PriceList constructPriceList;
    public Sprite buildingSprite;
    public float blockConstracionRadius = 10;
}

public enum BuildingClass
{
    Defens,
    Production
}

[System.Serializable]
public struct PriceList
{
    public int wood;
    public int stone;
    public int gold;
}
