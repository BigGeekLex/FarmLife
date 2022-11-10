using System;
using MSFD;
using UnityEngine;

public class PlantSelectableController : IPlantSelectableController
{
    private ISelectable<PlantType> _plantSelectable;
    private IPlantSelectableWindow _plantSelectableWindow;
    
    private Vector2Int _currentPos;
    private bool _isAlreadyOpened;
    
    public PlantSelectableController()
    {
        _plantSelectable = ServiceLocator.Get<ISelectable<PlantType>>();
        _plantSelectableWindow = ServiceLocator.Get<IPlantSelectableWindow>();
        
        _plantSelectable.OnSelected += OnPlantSelected;
    }
    
    ~PlantSelectableController()
    {
        _plantSelectable.OnSelected -= OnPlantSelected;
    }
    
    public void OnRequest(Vector2Int pos)
    {
        OnPlantSelectableWindowRequest(pos);
    }
    
    public event Action<PlantType, Vector2Int> PlantSelected;
    private void OnPlantSelectableWindowRequest(Vector2Int pos)
    {
        if (IsCanShow())
        {
            _isAlreadyOpened = true;
            InitializeWindow(pos);
        }
    }
    private bool IsCanShow()
    {
        return !_isAlreadyOpened;
    }
    private void InitializeWindow(Vector2Int pos)
    {
        _currentPos = pos;
        _plantSelectableWindow.Open();
    }
    private void OnPlantSelected(PlantType selectedType)
    {
        _plantSelectableWindow.Close();
        _isAlreadyOpened = false;
        
        PlantSelected?.Invoke(selectedType, _currentPos);
    }
}