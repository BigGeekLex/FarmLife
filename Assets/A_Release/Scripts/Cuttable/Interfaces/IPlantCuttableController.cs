using System;
using UnityEngine;

public interface IPlantCuttableController
{
    void SendCutRequest(Vector2Int pos);
    event Action<Vector2Int> CutRequest;
}