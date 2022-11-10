using UnityEngine;

public interface IWorldGridProvider
{  
    bool TrySelectGridByPosition(Vector2Int pos, out IGrid selected);
}