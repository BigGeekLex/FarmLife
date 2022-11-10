using System.Collections.Generic;
using UnityEngine;

public interface IHeroInitializer
{
    void SpawnHeroInWorldRandomPoint(Dictionary<Vector2Int, IGrid> worldData, HeroData heroData, float Yoffset);
}