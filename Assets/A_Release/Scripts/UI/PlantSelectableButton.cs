using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class PlantSelectableButton : MonoBehaviour, ISelectable<PlantType>
{
    [SerializeField] 
    private PlantType selectionType;
    
    private Button _button;
    
    public event Action<PlantType> OnSelected;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SendSelectionEvent);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SendSelectionEvent);
    }

    private void SendSelectionEvent()
    {
        OnSelected?.Invoke(selectionType);
    }
}