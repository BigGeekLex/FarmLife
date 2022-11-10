using System;
using System.Collections.Generic;
using UnityEngine;

public interface IWorldInitializer
{ 
    Dictionary<Vector2Int, IGrid> GetGeneratedWorldBoard(WorldData data);
}