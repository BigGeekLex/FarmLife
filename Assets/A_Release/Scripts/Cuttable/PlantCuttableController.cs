using System;
using UnityEngine;

public class PlantCuttableController : IPlantCuttableController
{
    public void SendCutRequest(Vector2Int pos)
    {
        CutRequest?.Invoke(pos);
    }

    public event Action<Vector2Int> CutRequest;
}