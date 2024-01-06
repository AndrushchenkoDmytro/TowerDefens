using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private int woodAmount = 0;
    private int stoneAmount = 0;
    private int goldAmount = 0;

    public void AddWoodAmount(int value)
    {
        woodAmount += value;
    }

    public void AddStoneAmount(int value) 
    { 
        stoneAmount += value;
    }

    public void GetWoodAmount(int value)
    {
        goldAmount += value;
    }
}
