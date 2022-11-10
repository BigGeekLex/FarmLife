using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WorldInitializer : IWorldInitializer
{
    private List<NavMeshSurface> _surfaces = new List<NavMeshSurface>();
    private void GenerateNavmesh()
    {
        foreach (var surface in _surfaces)
        {
            surface.BuildNavMesh();
        }
    }

    public Dictionary<Vector2Int, IGrid> GetGeneratedWorldBoard(WorldData data)
    {
        Dictionary<Vector2Int, IGrid> board = new Dictionary<Vector2Int, IGrid>();
        
        for (int i = 0; i < data.widht; i++)
        {
            for (int j = 0; j < data.lenght; j++)
            {
                GameObject nextBlock = PC.Spawn(data.spawnedBlock, new Vector3(i, data.yOffset, j), Quaternion.identity);
                
                IGrid grid = nextBlock.GetComponent<IGrid>();
                Vector2Int gridPos = new Vector2Int(i, j);
                
                _surfaces.Add(nextBlock.GetComponent<NavMeshSurface>());
                
                board.Add(gridPos, grid);
            }
        }
        GenerateNavmesh();

        return board;
    }
}
