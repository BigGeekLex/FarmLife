using System;

public interface IPlantCollectable
{
    void Initialize(IPlant plant);
    void ShowCollectView();
    
    event Action<PlantType, IPlantCollectable> Collected;
}