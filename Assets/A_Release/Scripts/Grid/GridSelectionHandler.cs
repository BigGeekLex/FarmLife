using MSFD;
using UnityEngine;

[RequireComponent(typeof(GridController))]
public class GridSelectionHandler : MonoBehaviour
{
    private IGrid _grid;
    private IPlantSelectableController PlantSelectableController => ServiceLocator.Get<IPlantSelectableController>();
    private void Start()
    {
        _grid = GetComponent<IGrid>();
        _grid.LandedRequest += OnLandedRequest;
    }
    private void OnDestroy()
    {
        _grid.LandedRequest -= OnLandedRequest;
    }
    private void OnLandedRequest(Vector2Int pos)
    {
        PlantSelectableController.OnRequest(pos);
    }
}