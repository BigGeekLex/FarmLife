using System;
using UnityEngine;

public interface IGrid
{
    event Action<Vector2Int> Selected;

    event Action<Vector2Int> LandedRequest; 
    void OnSelected();

    Vector2Int GetPosition();
    void ReleaseGrid();
}