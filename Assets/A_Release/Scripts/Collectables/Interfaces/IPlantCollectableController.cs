using System;
using UnityEngine;

public interface IPlantCollectableController
{
    void OnCollectingRequest(Vector2Int pos, IPlantCollectable collectable);
    
    event Action<Vector2Int> PlantCollecting;

    IObservable<PlantCollectableData> GetCollectableDataObs();
}