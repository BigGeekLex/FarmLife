using System;
using System.Collections.Generic;
using MSFD;
using UniRx;
using UnityEngine;

public class PlantCollectableController : IPlantCollectableController
{
    private ReactiveProperty<PlantCollectableData> _data = new ReactiveProperty<PlantCollectableData>();
    private IPlantCollectableController CollectableController => this;
    
    public PlantCollectableController()
    {
        _data.Value = new PlantCollectableData
        {
            Collectables = new Dictionary<PlantType, int>()
        };   
    }
    
    public void OnCollectingRequest(Vector2Int pos, IPlantCollectable collectable)
    {
        collectable.Collected += OnCollected;
        PlantCollecting?.Invoke(pos);
    }

    private void OnCollected(PlantType plantType, IPlantCollectable collectable)
    {
        collectable.Collected -= OnCollected;
        RefreshData(plantType);
    }
    
    private void RefreshData(PlantType type)
    {
        Dictionary<PlantType, int> nextData = _data.Value.Collectables;
        
        int nextCount = 0;
        int currentItemNumber = 0;
        
        if (nextData.ContainsKey(type) && nextData.TryGetValue(type, out currentItemNumber))
        {
            nextCount = currentItemNumber + 1;
                
            nextData.Remove(type);
            nextData.Add(type, nextCount);
        }
        else
        {
            nextCount += 1;
            nextData.Add(type, nextCount);
        }
        
        _data.SetValueAndForceNotify(new PlantCollectableData
        {
            Collectables = nextData
        });
    }
    
    public event Action<Vector2Int> PlantCollecting;
    
    public IObservable<PlantCollectableData> GetCollectableDataObs()
    {
        return _data;
    }
}