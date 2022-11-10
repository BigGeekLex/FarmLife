using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HeroInitializer : IHeroInitializer
{
    public void SpawnHeroInWorldRandomPoint(Dictionary<Vector2Int, IGrid> worldData, HeroData heroData, float Yoffset)
    {
        List<Vector2Int> worldPossiblePositions = worldData.Select((x) => x.Key).ToList();
        
        int randomIndex = Random.Range(0, worldPossiblePositions.Count);

        float heroYOffset = 0.5f;
        
        Vector3 spawnPosition = new Vector3(worldPossiblePositions[randomIndex].x,Yoffset + heroYOffset, worldPossiblePositions[randomIndex].y);
        PC.Spawn(heroData.prefab, spawnPosition, Quaternion.identity);
    }
}
