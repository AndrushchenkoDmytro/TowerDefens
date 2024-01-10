using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTypeHolder : MonoBehaviour
{
    [SerializeField] private BuildingsTypeSo holderBuilding;

    public BuildingsTypeSo GetHolderBuilding()
    {
        return holderBuilding;
    }

}
