using System;
using UnityEngine;
using UnityEngine.Events;

public enum GridState
{
    Free,
    Busy
}
public class GridController : MonoBehaviour, IGrid
{
    [SerializeField] 
    private UnityEvent onSelected;
    private GridState _gridState;
    private Vector2Int _gridPosition;
    private void Start()
    {
        _gridPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        ChangeGridStatus(GridState.Free);
    }

    public event Action<Vector2Int> Selected;
    public event Action<Vector2Int> LandedRequest;
    public void OnSelected()
    {
        switch (_gridState)
        {
            case GridState.Free:
                LandedRequest?.Invoke(_gridPosition);
                ChangeGridStatus(GridState.Busy);
                break;
        }
        
        Selected?.Invoke(_gridPosition);
        onSelected?.Invoke();
    }

    public Vector2Int GetPosition()
    {
        return _gridPosition;
    }

    public void ReleaseGrid()
    {
        ChangeGridStatus(GridState.Free);
    }
    
    private void ChangeGridStatus(GridState state)
    {
        _gridState = state;
    }
}