using System;
public interface IPlant
{
    void Initialize(PlantType type, float grownSpeed, IGrid placedGrid);
    void ReleasePlant();
    IGrid GetPlacedGrid();
    
    event Action GrownFinished;

    event Action GrownStarted;
    PlantType GetPlantType();
    float GetGrownSpeed(out float maxBorder);
}