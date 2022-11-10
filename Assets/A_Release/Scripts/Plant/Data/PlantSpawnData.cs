using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/PlantsSpawnData", order = 1)]
public class PlantSpawnData : ScriptableObject
{
    [SerializeField] 
    private List<PlantData> plantData;
    public PlantData GetDataBySelectedType(PlantType selectedType)
    {
        foreach (var vegetable in plantData)
        {
            if (vegetable.type == selectedType) return vegetable;
        }
        return null;
    }
}