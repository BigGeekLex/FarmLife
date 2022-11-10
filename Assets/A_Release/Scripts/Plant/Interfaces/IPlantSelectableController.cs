using System;
using UnityEngine;


public interface IPlantSelectableController
{
    void OnRequest(Vector2Int pos); 
    
    event Action<PlantType, Vector2Int> PlantSelected;
}
