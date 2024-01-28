using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTypeHolder : MonoBehaviour
{
    [SerializeField] private BuildingsTypeSo holderBuilding;

    public BuildingsTypeSo GetHolderBuildingType()
    {
        return holderBuilding;
    }

    public void SetHolderBuildingType(BuildingsTypeSo buildingsTypeSo)
    {
        holderBuilding = buildingsTypeSo;
    }
}
