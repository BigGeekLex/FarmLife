using System;
using System.Collections.Generic;
using MSFD;
using UnityEngine;

public class GameInitializer : SingletoneBase<GameInitializer>
{
    [SerializeField] 
    private WorldData worldData;
    [SerializeField] 
    private HeroData heroData;
    [SerializeField] 
    private PlantSpawnData plantData;
    protected override void AwakeInitialization()
    {
        Dictionary<Vector2Int, IGrid> generatedBoard;
        
        WorldInitializer worldInitializer = new WorldInitializer();
        generatedBoard = worldInitializer.GetGeneratedWorldBoard(worldData);

        ServiceLocator.Register<IWorldGridProvider>(new WorldGridProvider(generatedBoard));

        HeroInitializer heroInitializer = new HeroInitializer();
        heroInitializer.SpawnHeroInWorldRandomPoint(generatedBoard, heroData, worldData.yOffset);
        
        ServiceLocator.Register<IScoreProvidable>(new ScoreController());
        
        ServiceLocator.Register<IPlantCollectableController>(new PlantCollectableController());
    }

    private void Start()
    {
        ServiceLocator.Register<IPlantSelectableController>(new PlantSelectableController());
        ServiceLocator.Register<IPlantSpawner>( new PlantSpawnController(plantData));
        ServiceLocator.Register<IPlantCuttableController>(new PlantCuttableController());
    }

    private void OnDestroy()
    {
        ServiceLocator.Clear();
    }
}