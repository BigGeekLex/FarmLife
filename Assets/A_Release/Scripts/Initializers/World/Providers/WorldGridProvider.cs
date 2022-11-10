using System.Collections.Generic;
using MSFD;
using UnityEngine;

public class WorldGridProvider : IWorldGridProvider
{
    private Dictionary<Vector2Int, IGrid> _board;
    public WorldGridProvider(Dictionary<Vector2Int, IGrid> boardData)
    {
        _board = boardData; 
    }
    
    public bool TrySelectGridByPosition(Vector2Int pos, out IGrid selected)
    {
        selected = null;

        if (!_board.ContainsKey(pos))
        {
            return false;
        }

        _board.TryGetValue(pos, out selected);
        return true;
    }
}
