using System;
using MSFD;
using UnityEngine;


public class PlantSpawnController : IPlantSpawner
{
    private IPlantSelectableController Controller => ServiceLocator.Get<IPlantSelectableController>();
    private IWorldGridProvider WorldGridProvider => ServiceLocator.Get<IWorldGridProvider>();
    
    private PlantSpawnData _plantSpawnData;
    
    public event Action<Vector3, IPlant> Spawned;

    public PlantSpawnController(PlantSpawnData plantSpawnData)
    {
        _plantSpawnData = plantSpawnData;
        Controller.PlantSelected += OnPlantSelected;
    }
    
    ~PlantSpawnController()
    {
        Controller.PlantSelected -= OnPlantSelected;
    }
    private void OnPlantSelected(PlantType selectedType, Vector2Int selectedPos)
    {
        PlantData selectedData = _plantSpawnData.GetDataBySelectedType(selectedType);

        if (selectedData != null)
        {
            SpawnVegetable(selectedPos, selectedData);
        }
    }

    private void SpawnVegetable(Vector2Int pos, PlantData data)
    {
        IGrid placedGrid = null;
        
        if (WorldGridProvider.TrySelectGridByPosition(pos, out placedGrid))
        {
            Vector3 spawnPos = new Vector3(pos.x, 0, pos.y);
            GameObject nextGo = PC.Spawn(data.prefab, spawnPos);
            IPlant plant = nextGo.GetComponent<IPlant>();

            plant.Initialize(data.type, data.grownSpeed, placedGrid);
            
            Spawned?.Invoke(spawnPos, plant);
        }
    }
}