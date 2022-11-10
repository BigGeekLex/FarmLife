using System;

public interface IPlantCuttable
{
    void Initialize(IPlant plant);
    void ShowCuttableView();

    event Action Cutted;
}